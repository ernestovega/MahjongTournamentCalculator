using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using TournamentCalculator.Model;
using TournamentCalculator.Utils;
using NsExcel = Microsoft.Office.Interop.Excel;

namespace TournamentCalculator
{
    public partial class MainForm : Form
    {
        #region Fields

        private List<Player> players = new List<Player>();
        private List<TablePlayer> tablePlayers = new List<TablePlayer>();
        private List<TableWithAll> tablesWithAll = new List<TableWithAll>();
        private List<TableWithNames> tablesWithNames = new List<TableWithNames>();
        private List<TableWithTeams> tablesWithTeams = new List<TableWithTeams>();
        private List<TableWithCountries> tablesWithCountries = new List<TableWithCountries>();
        private List<TableWithAll> tablesByPlayer = new List<TableWithAll>();
        private List<Rivals> rivalsByPlayer = new List<Rivals>();
        private int currentRound, currentTable, currentTablePlayer;
        private Random random = new Random();
        private int countTries = 0;
        private string errorMessage;

        #endregion

        #region Public methods

        public MainForm()
        {
            Thread t = new Thread(new ThreadStart(openSplash));
            t.Start();
            Thread.Sleep(2000);
            InitializeComponent();
            t.Abort();

            DataGridViewUtils.updateDataGridView(dataGridView, new List<Player>() {
                new Player("1", "Example name", "Example Country", "Example Team")});
        }
        
        public void openSplash()
        {
            Application.Run(new SplashForm());
        }

        #endregion

        #region Events

        private void btnGetExcelTemplate_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            GenerateExcelTemplate();
            
            Cursor.Current = Cursors.Default;
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            string path = string.Empty;
            if (!RequestFile(ref path))
            {
                Cursor.Current = Cursors.Default;
                return;
            }

            DisableAll();

            players.Clear();
            tablePlayers.Clear();
            tablesWithAll.Clear();
            tablesWithNames.Clear();
            tablesWithTeams.Clear();
            tablesWithCountries.Clear();
            tablesByPlayer.Clear();
            rivalsByPlayer.Clear();
            lblPlayers.Text = string.Empty;
            lblTables.Text = string.Empty;
            errorMessage = "Something is wrong in the excel:\n";

            int excelState = ImportExcel(path);
            if (excelState > 0)
            {
                int numPlayers = players.Count;
                if (numPlayers <= 0)
                {
                    lblPlayers.Text = "Players: " + 0;
                    errorMessage += "\n\tThere aren't enought players";
                }
                else
                {
                    lblPlayers.Text = "Players: " + players.Count;
                    if (numPlayers % 4 != 0)
                        errorMessage += "\n\tThe number of players must be a multiple of 4.";
                    else if (players.Select(x => x.team).Distinct().Count() != numPlayers / 4)
                        errorMessage += "\n\tFor " + numPlayers + " players, the number of teams must be " + numPlayers / 4 + ".";
                    else if (ThereAre4PlayersByTeam())
                        errorMessage += "\n\tEach team must have 4 players.";
                    else
                    {
                        lblTables.Text = "Tables: " + numPlayers / 4;
                        btnGetExcelTemplate.Enabled = true;
                        btnImportExcel.Enabled = true;
                        btnCalculate.Enabled = true;
                        numUpDownRounds.Enabled = true;
                        numUpDownTriesMax.Enabled = true;
                        btnShowPlayers.Enabled = true;
                        
                        DataGridViewUtils.updateDataGridView(dataGridView, players);

                        btnShowPlayers.Enabled = false;
                        btnShowByPlayers.Enabled = true;
                        btnShowNames.Enabled = true;
                        btnShowTeams.Enabled = true;
                        btnShowCountries.Enabled = true;
                        btnShowAll.Enabled = true;
                        Cursor.Current = Cursors.Default;
                        return;
                    }
                }
            }
            else if(errorMessage.Equals("Something is wrong in the excel:\n"))
                errorMessage += "\n\tExcel malformed.";

            if (!errorMessage.Equals("Something is wrong in the excel:\n"))
                MessageBox.Show(errorMessage);

            DataGridViewUtils.updateDataGridView(dataGridView, players);

            btnGetExcelTemplate.Enabled = true;
            btnImportExcel.Enabled = true;
            btnShowPlayers.Enabled = false;
            btnShowByPlayers.Enabled = true;
            btnShowNames.Enabled = true;
            btnShowTeams.Enabled = true;
            btnShowCountries.Enabled = true;
            btnShowAll.Enabled = true;

            Cursor.Current = Cursors.Default;
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            DisableAll();
            lblTriesNeeded.Text = "Tries needed:";

            int numRounds = decimal.ToInt32(numUpDownRounds.Value);
            int numTriesMax = decimal.ToInt32(numUpDownTriesMax.Value);
            int result = -1;
            countTries = 0;

            //Cada vez que un cálculo es imposible, se reintenta desde cero tantas veces como se hayan indicado.
            while (result < 0 && countTries < numTriesMax)
            {
                countTries++;
                result = GenerateTournament(numRounds);
                lblTriesNeeded.Text = "Tries needed: " + countTries.ToString();
                Application.DoEvents();
            }
            
            /*Si no se ha podido calcular en los intentos indicados, se notifica,
              se muestra la lista de jugadores y se termina*/
            if (countTries >= numTriesMax)
            {
                DataGridViewUtils.updateDataGridView(dataGridView, players);
                
                numUpDownRounds.Enabled = true;
                btnCalculate.Enabled = true;
                numUpDownTriesMax.Enabled = true;
                btnImportExcel.Enabled = true;
                btnCalculate.Enabled = true;
                MessageBox.Show("Can't calculate tournament after " + numTriesMax + " tries.");
                Cursor.Current = Cursors.Default;
                return;
            }

            //Si llegamos aqui es que todo ha ido bien, generamos todas las vistas y se muestramos las mesas
            GenerateTablesWhitAll(numRounds);
            GenerateTablesWhitNames(numRounds);
            GenerateTablesWhitTeams(numRounds);
            GenerateTablesWhitCountries(numRounds);
            GenerateTablesByPlayer();
            GenerateRivalsByPlayer();

            EnableAll();
            DataGridViewUtils.updateDataGridView(dataGridView, tablesWithNames);
            btnShowNames.Enabled = false;
            Cursor.Current = Cursors.Default;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            DisableAll();

            ExportToExcel();
            
            DataGridViewUtils.updateDataGridView(dataGridView, tablesWithNames);
            EnableAll();
            btnShowNames.Enabled = false;
            Cursor.Current = Cursors.Default;
        }

        private void chckBxNames_CheckedChanged(object sender, EventArgs e)
        {
            CheckNamesIfAllUnchecked();
        }

        private void chckBxTeams_CheckedChanged(object sender, EventArgs e)
        {
            CheckNamesIfAllUnchecked();
        }

        private void chckBxCountries_CheckedChanged(object sender, EventArgs e)
        {
            CheckNamesIfAllUnchecked();
        }

        private void chckBxIds_CheckedChanged(object sender, EventArgs e)
        {
            CheckNamesIfAllUnchecked();
        }

        private void btnShowPlayers_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            btnShowPlayers.Enabled = false;

            DataGridViewUtils.updateDataGridView(dataGridView, players);

            btnShowByPlayers.Enabled = true;
            btnShowNames.Enabled = true;
            btnShowTeams.Enabled = true;
            btnShowCountries.Enabled = true;
            btnShowAll.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void btnShowByPlayers_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            btnShowByPlayers.Enabled = false;

            DataGridViewUtils.updateDataGridView(dataGridView, tablesByPlayer);

            btnShowPlayers.Enabled = true;
            btnShowNames.Enabled = true;
            btnShowTeams.Enabled = true;
            btnShowCountries.Enabled = true;
            btnShowAll.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            btnShowAll.Enabled = false;

            DataGridViewUtils.updateDataGridView(dataGridView, tablesWithAll);

            btnShowPlayers.Enabled = true;
            btnShowByPlayers.Enabled = true;
            btnShowNames.Enabled = true;
            btnShowTeams.Enabled = true;
            btnShowCountries.Enabled = true;
        }

        private void btnShowNames_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            btnShowNames.Enabled = false;


            DataGridViewUtils.updateDataGridView(dataGridView, tablesWithNames);

            btnShowPlayers.Enabled = true;
            btnShowByPlayers.Enabled = true;
            btnShowTeams.Enabled = true;
            btnShowCountries.Enabled = true;
            btnShowAll.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void btnShowTeams_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            btnShowTeams.Enabled = false;

            DataGridViewUtils.updateDataGridView(dataGridView, tablesWithTeams);

            btnShowPlayers.Enabled = true;
            btnShowByPlayers.Enabled = true;
            btnShowNames.Enabled = true;
            btnShowCountries.Enabled = true;
            btnShowAll.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void btnShowCountries_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            btnShowCountries.Enabled = false;

            DataGridViewUtils.updateDataGridView(dataGridView, tablesWithCountries);

            btnShowPlayers.Enabled = true;
            btnShowByPlayers.Enabled = true;
            btnShowNames.Enabled = true;
            btnShowTeams.Enabled = true;
            btnShowAll.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        #endregion

        #region Calculate tournament methods

        private int GenerateTournament(int numRounds)
        {
            //Limpiamos las tablas
            tablePlayers.Clear();
            tablesWithAll.Clear();
            tablesWithNames.Clear();
            tablesWithTeams.Clear();
            tablesWithCountries.Clear();
            tablesByPlayer.Clear();
            rivalsByPlayer.Clear();
            for (currentRound = 1; currentRound <= numRounds; currentRound++)
            {//Iteramos por rondas

                //Copiamos la lista de jugadores para ir borrando los que vayamos usando cada ronda
                List<int> playersNotUsedThisRound = players.Select(x => x.Clone()).ToList().Select(x => x.id).ToList();

                for (currentTable = 1; currentTable <= players.Count / 4; currentTable++)
                {//Iteramos por mesas en cada ronda

                    for (currentTablePlayer = 1; currentTablePlayer <= 4; currentTablePlayer++)
                    {//Iteramos por jugador en cada mesa

                        //Copiamos la lista de jugadores para ir borrando los que vayamos descartando
                        int[] arrayPlayersIdsNotDiscarded = new int[playersNotUsedThisRound.Count];
                        playersNotUsedThisRound.CopyTo(arrayPlayersIdsNotDiscarded);
                        List<int> playersIdsNotDiscarded = new List<int>(arrayPlayersIdsNotDiscarded);

                        bool playerFounded = false;
                        //Si no hay jugador elegido y no hemos recorrido todos los jugadores lo reintentamos.
                        while (!playerFounded && playersIdsNotDiscarded.Count > 0)
                        {
                            //Obtenemos la lista de jugadores de la actual mesa
                            List<TablePlayer> currentTableTablePlayers = tablePlayers.FindAll
                                (x => x.round == currentRound && x.table == currentTable).ToList();
                            List<Player> currentTablePlayers = new List<Player>();
                            foreach(TablePlayer tp in currentTableTablePlayers)
                                currentTablePlayers.Add(GetPlayerById(tp.playerId));

                            //Elegimos un jugador al azar y lo quitamos de la lista de no descartados
                            int r = random.Next(0, arrayPlayersIdsNotDiscarded.Count());
                            Player choosenOne = GetPlayerById(arrayPlayersIdsNotDiscarded[r]);
                            playersIdsNotDiscarded.Remove(choosenOne.id);

                            //Obtenemos la lista de jugadores que han jugado en anteriores rondas contra el elegido
                            List<int> playersWHPATCO = GetPlayersWhoHavePlayedAgainstTheChoosenOne(choosenOne);
                            bool anyoneHavePlayed = false;
                            foreach(int ctp in currentTablePlayers.Select(x => x.id))
                            {
                                if (playersWHPATCO.Contains(ctp))
                                    anyoneHavePlayed = true;
                            }

                            /*Si el elegido ya ha jugado contra alguno de los de la mesa actual
                              o es del mismo equipo que alguno de los de la mesa actual
                              hay que buscar un nuevo candidato para esta mesa*/
                            if (anyoneHavePlayed || currentTablePlayers.Select(x => x.team).Contains(choosenOne.team))
                                playerFounded = false;
                            else
                            {/*Si no ha jugado contra ninguno ni son de su mismo equipo, lo añadimos a la mesa
                               y lo quitamos de la lista de jugadores sin usar esta ronda*/

                                playerFounded = true;
                                tablePlayers.Add(new TablePlayer(currentRound, currentTable, currentTablePlayer, 
                                    choosenOne.id));
                                playersNotUsedThisRound.Remove(choosenOne.id);
                            }
                        }

                        //Si no se ha encontrado un posible jugador delvolvemos error para volver a empezar todo.
                        if(!playerFounded && playersIdsNotDiscarded.Count == 0)
                            return -1;
                    }
                }
            }
            //Si llegamos aqui es que todo ha ido bien y se ha terminado el cálculo
            return 1;
        }

        private List<int> GetPlayersWhoHavePlayedAgainstTheChoosenOne(Player choosenOne)
        {
            //Obtenemos una lista con las mesas de las anteriores rondas
            List<TablePlayer> anterioresRondas = tablePlayers.FindAll(x => x.round < currentRound).ToList();

            //Si hay anteriores rondas
            if (anterioresRondas.Count > 0)
            {
                //Obtenemos una lista con los ids de las anteriores rondas
                List<int> roundIdsWhichHavePlayed = anterioresRondas.Select(x => x.round).Distinct().ToList();

                //Obtenemos una lista de las mesas en las que ha jugado el elegido en cada ronda
                List<TablePlayer> tablePlayersWhichHavePlayedChoosenOne = new List<TablePlayer>();
                foreach (int roundPlayed in roundIdsWhichHavePlayed)
                {
                    tablePlayersWhichHavePlayedChoosenOne.AddRange(anterioresRondas.FindAll(
                        x => x.round == roundPlayed && x.playerId == choosenOne.id).ToList());
                }
                List<TablePlayer> completeTablePlayersWhichHavePlayedAll = new List<TablePlayer>();
                foreach (TablePlayer tp in tablePlayersWhichHavePlayedChoosenOne)
                {
                    completeTablePlayersWhichHavePlayedAll.AddRange(anterioresRondas.FindAll(
                        x => x.round == tp.round && x.table == tp.table).ToList());
                }

                //Obtenemos una lista con los jugadores que ya han jugado contra el elegido en cada mesa donde él jugó
                List<int> rivalsWhoHavePlayedAgainst = new List<int>();
                foreach (TablePlayer tp in completeTablePlayersWhichHavePlayedAll)
                {
                    if (tp.playerId != choosenOne.id)
                        rivalsWhoHavePlayedAgainst.Add(tp.playerId);
                }
                return rivalsWhoHavePlayedAgainst;
            }
            else
                return new List<int>();
        }

        #endregion

        #region Excel methods

        private bool isExcelInstalled()
        {
            Type officeType = Type.GetTypeFromProgID("Excel.Application");
            if (officeType == null)
            {
                MessageBox.Show("Excel is not present on your computer.");
                return false;
            }
            else
                return true;
        }

        private void GenerateExcelTemplate()
        {
            if (!isExcelInstalled())
                return;

            //Start excel
            NsExcel.Application excel;
            excel = new NsExcel.Application();

            //Make excel visible
            excel.Visible = true;
            excel.DisplayAlerts = false;

            //Create a new Workbook
            NsExcel.Workbook excelWorkBook;
            excelWorkBook = excel.Workbooks.Add();

            //Using default Worksheet
            var excelSheets = excelWorkBook.Sheets as NsExcel.Sheets;

            //Adding new Worksheet
            var newSheet = (NsExcel.Worksheet)excelSheets.Add(Type.Missing, excelSheets[excelSheets.Count], Type.Missing, Type.Missing);
            newSheet.Name = "Players";
            while (excelSheets.Count > 1)
            {
                excelSheets[excelSheets.Count - 1].Delete();
            }

            //Write headers
            newSheet.Cells[1, 1] = "Id";
            newSheet.Cells[1, 2] = "Name";
            newSheet.Cells[1, 3] = "Team";
            newSheet.Cells[1, 4] = "Country";

            //Paint headers
            newSheet.UsedRange.Rows[1].Cells.Interior.Color = ColorTranslator.ToOle(Color.FromArgb(0, 177, 106));
            newSheet.UsedRange.Rows[1].Cells.Font.Color = ColorTranslator.ToOle(Color.White);
            newSheet.UsedRange.Rows[1].Cells.Font.Bold = true;

            //Save the excel
            string excelName = "Players_Template";
            excelWorkBook.SaveAs(excelName,
                NsExcel.XlFileFormat.xlWorkbookNormal);
            try
            {
                excelWorkBook.SaveCopyAs(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + excelName + ".xls");
            }
            catch
            {
                MessageBox.Show("Excel template coldn't be saved.");
            }
        }

        private int ImportExcel(string ruta)
        {
            DataTable dataTable = new DataTable();
            bool flagWrongExcel = false;
            string strConnXlsx = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ruta
                + ";Extended Properties=" + '"' + "Excel 12.0 Xml;IMEX=1" + '"';
            string strConnXls = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ruta
                + ";Extended Properties=" + '"' + "Excel 8.0;IMEX=1" + '"';
            string sqlExcel;
            string strConn = ruta.Substring(ruta.Length - 4).ToLower().Equals("xlsx")
                ? strConnXlsx : strConnXls;
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                try
                {
                    conn.Open();
                    var dtSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                    var Sheet1 = dtSchema.Rows[0].Field<string>("TABLE_NAME");
                    sqlExcel = "SELECT * FROM [" + Sheet1 + "]";
                    OleDbDataAdapter oleDbdataAdapter = new OleDbDataAdapter(sqlExcel, conn);
                    oleDbdataAdapter.Fill(dataTable);
                }
                catch
                {
                    errorMessage += "\n\tWrong Excel file format.";
                    flagWrongExcel = true;
                }

                if (dataTable == null || dataTable.Rows == null || dataTable.Columns == null)
                {
                    flagWrongExcel = true;
                    errorMessage += "\n\tWrong Excel file format or empty.";
                }
                else if(dataTable.Columns.Count < 4)
                {
                    flagWrongExcel = true;
                    errorMessage += "\n\tThere aren´t enough columns.";
                }
                //else if (dataTable.Columns.Count > 4)
                //{
                //    flagWrongExcel = true;
                //    errorMessage += "\n\tThere are too much columns.";
                //}
                else if (!((DataColumn)dataTable.Columns[0]).ColumnName.ToString().ToLower().Equals("id") ||
                    !((DataColumn)dataTable.Columns[1]).ColumnName.ToString().ToLower().Equals("name") ||
                    !((DataColumn)dataTable.Columns[2]).ColumnName.ToString().ToLower().Equals("team") ||
                    !((DataColumn)dataTable.Columns[3]).ColumnName.ToString().ToLower().Equals("country"))
                {
                    flagWrongExcel = true;
                    errorMessage += "\n\tColumn headers doesn´t match.";
                }

                if (!flagWrongExcel)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        try
                        {
                            if (string.IsNullOrWhiteSpace(row[0].ToString()) || string.IsNullOrWhiteSpace(row[1].ToString()) ||
                                string.IsNullOrWhiteSpace(row[2].ToString()) || string.IsNullOrWhiteSpace(row[3].ToString()))
                            {//Nos aseguramos de que no hay ninguna casilla vacía
                                flagWrongExcel = true;
                                AddNewPlayerFromExcel(row);
                            }
                            else
                                AddNewPlayerFromExcel(row);
                        }
                        catch (Exception)
                        {
                            flagWrongExcel = true;
                            AddNewPlayerFromExcel(row);
                        }
                    }
                }
            }
            if (flagWrongExcel)
            {
                return -1;
            }
            else
                return 1;
        }

        private static bool RequestFile(ref string path)
        {
            OpenFileDialog fDialog = new OpenFileDialog();
            fDialog.Title = "Select Excel file";
            fDialog.Filter = "Excel Files|*.xlsx;*.xls;";
            fDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (fDialog.ShowDialog() == DialogResult.OK)
            {
                path = fDialog.FileName.ToString();
                return true;
            }

            path = "";
            return false;
        }

        private void AddNewPlayerFromExcel(DataRow row)
        {
            players.Add(new Player(
                row.IsNull(0) || string.IsNullOrWhiteSpace(row[0].ToString()) ? "" : row[0].ToString(),
                row.IsNull(1) || string.IsNullOrWhiteSpace(row[1].ToString()) ? "" : row[1].ToString(),
                row.IsNull(2) || string.IsNullOrWhiteSpace(row[2].ToString()) ? "" : row[2].ToString(),
                row.IsNull(3) || string.IsNullOrWhiteSpace(row[3].ToString()) ? "" : row[3].ToString()
                ));
        }

        private bool ThereAre4PlayersByTeam()
        {
            List<string> teams = players.Select(x => x.team).Distinct().ToList();
            bool flagWrongMembers = false;
            foreach(string team in teams)
            {
                if (players.FindAll(x => x.team.Equals(team)).Count != 4)
                    flagWrongMembers = true;
            }
            return flagWrongMembers;
        }

        private void ExportToExcel()
        {
            if (!isExcelInstalled())
                return;

            //Start excel
            NsExcel.Application excel;
            excel = new NsExcel.Application();

            //Make excel visible
            excel.Visible = true;
            excel.DisplayAlerts = false;

            //Create a new Workbook
            NsExcel.Workbook excelWorkBook;
            excelWorkBook = excel.Workbooks.Add();

            //Using default Worksheet
            NsExcel.Sheets excelSheets = excelWorkBook.Sheets as NsExcel.Sheets;

            //Write Tournament data by rounds         
            for (currentRound = 1; currentRound <= tablesWithAll.Select(x => x.roundId).Distinct().Count(); currentRound++)
            {
                //Adding new Worksheet
                var newSheet = (NsExcel.Worksheet)excelSheets.Add(Type.Missing, excelSheets[excelSheets.Count], Type.Missing, Type.Missing);
                newSheet.Name = "Round " + (currentRound);
                if (currentRound == 1)
                {
                    while (excelSheets.Count > 1)
                    {
                        excelSheets[excelSheets.Count - 1].Delete();
                    }
                }

                //Write headers
                newSheet.Cells[1, 1] = "Round";
                newSheet.Cells[1, 2] = "Table";
                if (chckBxNames.Checked)
                {
                    newSheet.Cells[1, 3] = "Player 1 Name";
                    newSheet.Cells[1, 4] = "Player 2 Name";
                    newSheet.Cells[1, 5] = "Player 3 Name";
                    newSheet.Cells[1, 6] = "Player 4 Name";
                }
                if (chckBxTeams.Checked)
                {
                    newSheet.Cells[1, 7] = "Player 1 Team";
                    newSheet.Cells[1, 8] = "Player 2 Team";
                    newSheet.Cells[1, 9] = "Player 3 Team";
                    newSheet.Cells[1, 10] = "Player 4 Team";
                }
                if (chckBxCountries.Checked)
                {
                    newSheet.Cells[1, 11] = "Player 1 Country";
                    newSheet.Cells[1, 12] = "Player 2 Country";
                    newSheet.Cells[1, 13] = "Player 3 Country";
                    newSheet.Cells[1, 14] = "Player 4 Country";
                }
                if (chckBxIds.Checked)
                {
                    newSheet.Cells[1, 15] = "Player 1 Id";
                    newSheet.Cells[1, 16] = "Player 2 Id";
                    newSheet.Cells[1, 17] = "Player 3 Id";
                    newSheet.Cells[1, 18] = "Player 4 Id";
                }

                var currentRoundTables = tablesWithAll.FindAll(x => x.roundId == currentRound).ToList();

                for (currentTable = 1; currentTable <= tablesWithAll.Select(x => x.tableId).Distinct().Count(); currentTable++)
                {
                    newSheet.Cells[currentTable + 1, 1 ] = currentRoundTables[currentTable - 1].roundId;
                    newSheet.Cells[currentTable + 1, 2 ] = currentRoundTables[currentTable - 1].tableId;
                    if (chckBxNames.Checked)
                    {
                        newSheet.Cells[currentTable + 1, 3 ] = currentRoundTables[currentTable - 1].player1Name;
                        newSheet.Cells[currentTable + 1, 4 ] = currentRoundTables[currentTable - 1].player2Name;
                        newSheet.Cells[currentTable + 1, 5 ] = currentRoundTables[currentTable - 1].player3Name;
                        newSheet.Cells[currentTable + 1, 6 ] = currentRoundTables[currentTable - 1].player4Name;
                    }
                    if (chckBxTeams.Checked)
                    {
                        newSheet.Cells[currentTable + 1, 7 ] = currentRoundTables[currentTable - 1].player1Team;
                        newSheet.Cells[currentTable + 1, 8 ] = currentRoundTables[currentTable - 1].player2Team;
                        newSheet.Cells[currentTable + 1, 9 ] = currentRoundTables[currentTable - 1].player3Team;
                        newSheet.Cells[currentTable + 1, 10] = currentRoundTables[currentTable - 1].player4Team;
                    }
                    if (chckBxCountries.Checked)
                    {
                        newSheet.Cells[currentTable + 1, 11] = currentRoundTables[currentTable - 1].player1Country;
                        newSheet.Cells[currentTable + 1, 12] = currentRoundTables[currentTable - 1].player2Country;
                        newSheet.Cells[currentTable + 1, 13] = currentRoundTables[currentTable - 1].player3Country;
                        newSheet.Cells[currentTable + 1, 14] = currentRoundTables[currentTable - 1].player4Country;
                    }
                    if (chckBxIds.Checked)
                    {
                        newSheet.Cells[currentTable + 1, 15] = currentRoundTables[currentTable - 1].player1Id;
                        newSheet.Cells[currentTable + 1, 16] = currentRoundTables[currentTable - 1].player2Id;
                        newSheet.Cells[currentTable + 1, 17] = currentRoundTables[currentTable - 1].player3Id;
                        newSheet.Cells[currentTable + 1, 18] = currentRoundTables[currentTable - 1].player4Id;
                    }
                }

                //Resize columns
                newSheet.UsedRange.EntireColumn.AutoFit();

                //Paint headers
                newSheet.UsedRange.Rows[1].Cells.Interior.Color = ColorTranslator.ToOle(Color.FromArgb(0, 177, 106));
                newSheet.UsedRange.Rows[1].Cells.Font.Color = ColorTranslator.ToOle(Color.White);
                newSheet.UsedRange.Rows[1].Cells.Font.Bold = true;
            }

            //Write Tournament data by players
            WriteToExcelTablesByPlayers(excelSheets);
            WriteToExcelRivals(excelSheets);

            //Now save the excel
            string excelName = "Tournament_"
                + DateTime.Now.Second + DateTime.Now.Minute + DateTime.Now.Hour
                + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year;
            excelWorkBook.SaveAs(excelName,
                NsExcel.XlFileFormat.xlWorkbookNormal);
            try
            {
                excelWorkBook.SaveCopyAs(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                    + "\\" + excelName + ".xls");
            }
            catch
            {
                MessageBox.Show("Excel template coldn't be saved.");
            }
        }

        private void WriteToExcelTablesByPlayers(NsExcel.Sheets excelSheets)
        {
            //Adding new Worksheet
            var newSheet = (NsExcel.Worksheet)excelSheets.Add(Type.Missing, excelSheets[excelSheets.Count], Type.Missing, Type.Missing);
            newSheet.Name = "Player's tables";
           
            //Write headers
            newSheet.Cells[1, 1] = "Round";
            newSheet.Cells[1, 2] = "Table";
            newSheet.Cells[1, 3] = "Player 1 Name";
            newSheet.Cells[1, 4] = "Player 2 Name";
            newSheet.Cells[1, 5] = "Player 3 Name";
            newSheet.Cells[1, 6] = "Player 4 Name";
            newSheet.Cells[1, 7] = "Player 1 Team";
            newSheet.Cells[1, 8] = "Player 2 Team";
            newSheet.Cells[1, 9] = "Player 3 Team";
            newSheet.Cells[1, 10] = "Player 4 Team";
            newSheet.Cells[1, 11] = "Player 1 Country";
            newSheet.Cells[1, 12] = "Player 2 Country";
            newSheet.Cells[1, 13] = "Player 3 Country";
            newSheet.Cells[1, 14] = "Player 4 Country";
            newSheet.Cells[1, 15] = "Player 1 Id";
            newSheet.Cells[1, 16] = "Player 2 Id";
            newSheet.Cells[1, 17] = "Player 3 Id";
            newSheet.Cells[1, 18] = "Player 4 Id";

            //Paint headers
            newSheet.UsedRange.Rows[1].Cells.Interior.Color = ColorTranslator.ToOle(Color.FromArgb(0, 177, 106));
            newSheet.UsedRange.Rows[1].Cells.Font.Color = ColorTranslator.ToOle(Color.White);
            newSheet.UsedRange.Rows[1].Cells.Font.Bold = true;

            //Write data

            for (int i = 0; i < tablesByPlayer.Count; i++)
            {
                TableWithAll twa = tablesByPlayer[i];

                newSheet.Cells[i + 1, 1] = twa.roundId;
                newSheet.Cells[i + 1, 2] = twa.tableId;
                newSheet.Cells[i + 1, 3] = twa.player1Name;
                newSheet.Cells[i + 1, 4] = twa.player2Name;
                newSheet.Cells[i + 1, 5] = twa.player3Name;
                newSheet.Cells[i + 1, 6] = twa.player4Name;
                newSheet.Cells[i + 1, 7] = twa.player1Team;
                newSheet.Cells[i + 1, 8] = twa.player2Team;
                newSheet.Cells[i + 1, 9] = twa.player3Team;
                newSheet.Cells[i + 1, 10] = twa.player4Team;
                newSheet.Cells[i + 1, 11] = twa.player1Country;
                newSheet.Cells[i + 1, 12] = twa.player2Country;
                newSheet.Cells[i + 1, 13] = twa.player3Country;
                newSheet.Cells[i + 1, 14] = twa.player4Country;
                newSheet.Cells[i + 1, 15] = twa.player1Id;
                newSheet.Cells[i + 1, 16] = twa.player2Id;
                newSheet.Cells[i + 1, 17] = twa.player3Id;
                newSheet.Cells[i + 1, 18] = twa.player4Id;
            }

            //Resize columns
            newSheet.UsedRange.EntireColumn.AutoFit();
        }

        private void WriteToExcelRivals(NsExcel.Sheets excelSheets)
        {
            //Adding new Worksheet
            var newSheet = (NsExcel.Worksheet)excelSheets.Add(Type.Missing, excelSheets[excelSheets.Count], Type.Missing, Type.Missing);
            newSheet.Name = "Player's rivals";

            //Write headers
            newSheet.Cells[1, 1] = "Player Name";
            int maxRivals = 0;
            foreach (Rivals r in rivalsByPlayer)
            {
                if (r.rivalsNames.Count() > maxRivals)
                    maxRivals = r.rivalsNames.Count();
            }
            for (int i = 1; i <= maxRivals; i++)
            {
                newSheet.Cells[1, i + 1] = "Rival " + i + " Name";
            }

            //Paint headers
            newSheet.UsedRange.Rows[1].Cells.Interior.Color = ColorTranslator.ToOle(Color.FromArgb(0, 177, 106));
            newSheet.UsedRange.Rows[1].Cells.Font.Color = ColorTranslator.ToOle(Color.White);
            newSheet.UsedRange.Rows[1].Cells.Font.Bold = true;

            //Write data
            for (int i = 0; i < rivalsByPlayer.Count; i++)
            {
                Rivals r = rivalsByPlayer[i];

                newSheet.Cells[i + 2, 1] = r.playerName;
                for (int j = 0; j < r.rivalsNames.Count(); j++)
                {
                    newSheet.Cells[i + 2, j + 2] = r.rivalsNames[j];
                }
            }

            //Bold first column
            newSheet.UsedRange.Columns[1].Cells.Font.Bold = true;

            //Resize columns
            newSheet.UsedRange.EntireColumn.AutoFit();
        }

        #endregion

        #region Private methods

        private Player GetPlayerById(int id)
        {
            foreach(Player p in players)
            {
                if(p.id == id)
                {
                    return p;
                }
            }
            return null;
        }

        private void GenerateTablesWhitAll(int numRounds)
        {
            for (currentRound = 1; currentRound <= numRounds; currentRound++)
            {
                for (currentTable = 1; currentTable <= players.Count / 4; currentTable++)
                {
                    TableWithAll tableWithAll = new TableWithAll();
                    tableWithAll.roundId = currentRound;
                    tableWithAll.tableId = currentTable;
                    for (currentTablePlayer = 1; currentTablePlayer <= 4; currentTablePlayer++)
                    {
                        switch (currentTablePlayer)
                        {
                            case 1:
                                int player1Id = tablePlayers.Find(x => x.round == currentRound &&
                                x.table == currentTable && x.player == currentTablePlayer).playerId;
                                Player player = players.Find(x => x.id == player1Id);
                                tableWithAll.player1Name = player.name;
                                tableWithAll.player1Team = player.team;
                                tableWithAll.player1Country = player.country;
                                tableWithAll.player1Id = player.id;
                                break;
                            case 2:
                                int player2Id = tablePlayers.Find(x => x.round == currentRound &&
                                x.table == currentTable && x.player == currentTablePlayer).playerId;
                                Player player2 = players.Find(x => x.id == player2Id);
                                tableWithAll.player2Name = player2.name;
                                tableWithAll.player2Team = player2.team;
                                tableWithAll.player2Country = player2.country;
                                tableWithAll.player2Id = player2.id;
                                break;
                            case 3:
                                int player3Id = tablePlayers.Find(x => x.round == currentRound &&
                                x.table == currentTable && x.player == currentTablePlayer).playerId;
                                Player player3 = players.Find(x => x.id == player3Id);
                                tableWithAll.player3Name = player3.name;
                                tableWithAll.player3Team = player3.team;
                                tableWithAll.player3Country = player3.country;
                                tableWithAll.player3Id = player3.id;
                                break;
                            case 4:
                                int player4Id = tablePlayers.Find(x => x.round == currentRound &&
                                x.table == currentTable && x.player == currentTablePlayer).playerId;
                                Player player4 = players.Find(x => x.id == player4Id);
                                tableWithAll.player4Name = player4.name;
                                tableWithAll.player4Team = player4.team;
                                tableWithAll.player4Country = player4.country;
                                tableWithAll.player4Id = player4.id;
                                break;
                        }
                    }
                    tablesWithAll.Add(tableWithAll);
                }
            }
        }

        private void GenerateTablesWhitNames(int numRounds)
        {
            tablesWithNames.AddRange(
                tablesWithAll.Select(x => new TableWithNames(
                    x.roundId, x.tableId, x.player1Name, x.player2Name, x.player3Name, x.player4Name))
                    .ToList());            
        }

        private void GenerateTablesWhitTeams(int numRounds)
        {
            tablesWithTeams.AddRange(
                tablesWithAll.Select(x => new TableWithTeams(
                    x.roundId, x.tableId, x.player1Team, x.player2Team, x.player3Team, x.player4Team))
                    .ToList());
        }

        private void GenerateTablesWhitCountries(int numRounds)
        {
            tablesWithCountries.AddRange(
                tablesWithAll.Select(x => new TableWithCountries(
                    x.roundId, x.tableId, x.player1Country, x.player2Country, x.player3Country, x.player4Country))
                    .ToList());
        }

        private void GenerateTablesByPlayer()
        {
            foreach(Player p in players)
            {
                tablesByPlayer.AddRange(
                    tablesWithAll.FindAll(x => 
                        x.player1Name.Equals(p.name) ||
                        x.player2Name.Equals(p.name) ||
                        x.player3Name.Equals(p.name) ||
                        x.player4Name.Equals(p.name)));
            }
        }

        private void GenerateRivalsByPlayer()
        {
            foreach (Player p in players)
            {
                List<TableWithAll> thisPlayerTables = tablesWithAll.FindAll(x =>
                        x.player1Name.Equals(p.name) ||
                        x.player2Name.Equals(p.name) ||
                        x.player3Name.Equals(p.name) ||
                        x.player4Name.Equals(p.name));
                List<string> thisPlayerRivals = new List<string>();
                foreach (TableWithAll twa in thisPlayerTables)
                {
                    if (!twa.player1Name.Equals(p.name))
                        thisPlayerRivals.Add(twa.player1Name);
                    if (!twa.player2Name.Equals(p.name))
                        thisPlayerRivals.Add(twa.player2Name);
                    if (!twa.player3Name.Equals(p.name))
                        thisPlayerRivals.Add(twa.player3Name);
                    if (!twa.player4Name.Equals(p.name))
                        thisPlayerRivals.Add(twa.player4Name);
                }
                rivalsByPlayer.Add(new Rivals(p.name, thisPlayerRivals.ToArray()));
            }
        }

        private void CheckNamesIfAllUnchecked()
        {
            if (!chckBxNames.Checked && !chckBxTeams.Checked &&
                            !chckBxCountries.Checked && !chckBxIds.Checked)
            {
                chckBxNames.Checked = true;
            }
        }

        private void EnableAll()
        {
            btnGetExcelTemplate.Enabled = true;
            btnImportExcel.Enabled = true;
            btnCalculate.Enabled = true;
            btnExport.Enabled = true;
            numUpDownRounds.Enabled = true;
            numUpDownTriesMax.Enabled = true;
            chckBxNames.Enabled = true;
            chckBxTeams.Enabled = true;
            chckBxCountries.Enabled = true;
            chckBxIds.Enabled = true;
            btnShowPlayers.Enabled = true;
            btnShowByPlayers.Enabled = true;
            btnShowNames.Enabled = true;
            btnShowTeams.Enabled = true;
            btnShowCountries.Enabled = true;
            btnShowAll.Enabled = true;
        }

        private void DisableAll()
        {
            btnGetExcelTemplate.Enabled = false;
            btnImportExcel.Enabled = false;
            btnCalculate.Enabled = false;
            btnExport.Enabled = false;
            numUpDownRounds.Enabled = false;
            numUpDownTriesMax.Enabled = false;
            chckBxNames.Enabled = false;
            chckBxTeams.Enabled = false;
            chckBxCountries.Enabled = false;
            chckBxIds.Enabled = false;
            btnShowPlayers.Enabled = false;
            btnShowByPlayers.Enabled = false;
            btnShowNames.Enabled = false;
            btnShowTeams.Enabled = false;
            btnShowCountries.Enabled = false;
            btnShowAll.Enabled = false;
        }

        #endregion
    }
}