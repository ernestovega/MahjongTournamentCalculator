using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
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
        private List<string[]> sPlayers = new List<string[]>();
        private List<string[]> sTablesNames = new List<string[]>();
        private List<string[]> sTablesTeams = new List<string[]>();
        private List<string[]> sTablesCountries = new List<string[]>();
        private List<string[]> sTablesIds = new List<string[]>();
        private List<TableWithAll> tablesByPlayer = new List<TableWithAll>();
        private List<Rivals> rivalsByPlayer = new List<Rivals>();
        private int currentRound, currentTable, currentTablePlayer;
        private Random random = new Random();
        private int countTries = 0;
        private string errorMessage;
        private int numRounds;
        private string makingDate;

        #endregion

        #region Public methods

        public MainForm()
        {
            Thread t = new Thread(new ThreadStart(openSplash));
            t.Start();
            
            InitializeComponent();
            Thread.Sleep(1000);

            if(isExcelInstalled())
                t.Abort();
            else
            {
                MessageBox.Show("Excel not present on your computer.\nPlease get it.");
                Application.Exit();
            }
        }

        public void openSplash()
        {
            Application.Run(new SplashForm());
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            Activate();
        }

        #endregion

        #region Steps buttons

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
            sPlayers.Clear();
            tablePlayers.Clear();
            tablesWithAll.Clear();
            sTablesNames.Clear();
            sTablesTeams.Clear();
            sTablesCountries.Clear();
            sTablesIds.Clear();
            tablesByPlayer.Clear();
            rivalsByPlayer.Clear();
            lblPlayers.Text = string.Empty;
            lblTables.Text = string.Empty;
            errorMessage = "Something is wrong in the excel:\n";

            int excelState = ImportPlayer(path);
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
                    {//TODO OK
                        players = players.OrderBy(x => x.id).ToList();
                        GenerateSPlayers();
                        lblTables.Text = "Tables: " + numPlayers / 4;
                        DataGridViewUtils.updateDataGridViewPlayer(dataGridView, sPlayers);

                        btnGetExcelTemplate.Enabled = true;
                        btnImportExcel.Enabled = true;
                        btnCalculate.Enabled = true;
                        numUpDownRounds.Enabled = true;
                        numUpDownTriesMax.Enabled = true;

                        btnGetExcelTemplate.BackColor = Color.FromArgb(0, 177, 106);
                        btnImportExcel.BackColor = Color.FromArgb(0, 177, 106);
                        btnCalculate.BackColor = Color.FromArgb(0, 177, 106);
                        btnShowPlayers.BackColor = Color.FromArgb(224, 224, 224);

                        btnGetExcelTemplate.ForeColor = Color.White;
                        btnImportExcel.ForeColor = Color.White;
                        btnCalculate.ForeColor = Color.White;
                        btnShowPlayers.ForeColor = Color.White;

                        Cursor.Current = Cursors.Default;
                        return;
                    }
                }
            }
            else if(errorMessage.Equals("Something is wrong in the excel:\n"))
                errorMessage += "\n\tExcel malformed.";

            if (!errorMessage.Equals("Something is wrong in the excel:\n"))
                MessageBox.Show(errorMessage);
            
            btnGetExcelTemplate.Enabled = true;
            btnImportExcel.Enabled = true;
            btnShowPlayers.Enabled = false;
            btnShowNames.Enabled = true;
            btnShowTeams.Enabled = true;
            btnShowCountries.Enabled = true;
            btnShowIds.Enabled = true;

            btnGetExcelTemplate.BackColor = Color.FromArgb(0, 177, 106);
            btnImportExcel.BackColor = Color.FromArgb(0, 177, 106);
            btnShowPlayers.BackColor = Color.FromArgb(224, 224, 224);
            btnShowNames.BackColor = Color.FromArgb(0, 177, 106);
            btnShowTeams.BackColor = Color.FromArgb(0, 177, 106);
            btnShowCountries.BackColor = Color.FromArgb(0, 177, 106);
            btnShowIds.BackColor = Color.FromArgb(0, 177, 106);

            btnGetExcelTemplate.ForeColor = Color.White;
            btnImportExcel.ForeColor = Color.White;
            btnShowPlayers.ForeColor = Color.FromArgb(224, 224, 224);
            btnShowNames.ForeColor = Color.White;
            btnShowTeams.ForeColor = Color.White;
            btnShowCountries.ForeColor = Color.White;
            btnShowIds.ForeColor = Color.White;

            Cursor.Current = Cursors.Default;
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            DisableAll();
            lblTriesNeeded.Text = "Tries needed:";
            makingDate = string.Format("{0}{1}{2}_{3}{4}{5}", DateTime.Now.Year, DateTime.Now.Month, 
                DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            numRounds = decimal.ToInt32(numUpDownRounds.Value);
            int numTriesMax = decimal.ToInt32(numUpDownTriesMax.Value);
            customProgressBar.Maximum = numTriesMax;
            int result = -1;
            countTries = 0;

            customProgressBar.Visible = true;
            customProgressBar.Show();

            //Cada vez que un cálculo es imposible, se reintenta desde cero tantas veces como se hayan indicado.
            while (result < 0 && countTries < numTriesMax)
            {
                customProgressBar.Value = countTries++;
                lblTriesNeeded.Text = countTries.ToString();
                result = GenerateTournament(numRounds);
                lblTriesNeeded.Text = "Tries needed: " + countTries.ToString();
                Application.DoEvents();
            }

            customProgressBar.Hide();
            customProgressBar.Visible = false;
            
            /*Si no se ha podido calcular en los intentos indicados, se notifica,
              se muestra la lista de jugadores y se termina*/
            if (countTries >= numTriesMax)
            {
                DataGridViewUtils.updateDataGridViewPlayer(dataGridView, sPlayers);
                
                numUpDownRounds.Enabled = true;
                btnCalculate.Enabled = true;
                numUpDownTriesMax.Enabled = true;
                btnImportExcel.Enabled = true;
                btnCalculate.BackColor = Color.FromArgb(0, 177, 106);
                btnImportExcel.BackColor = Color.FromArgb(0, 177, 106);
                btnCalculate.BackColor = Color.FromArgb(0, 177, 106);
                btnCalculate.ForeColor = Color.White;
                btnImportExcel.ForeColor = Color.White;
                btnCalculate.ForeColor = Color.White;
                MessageBox.Show("Can't calculate tournament after " + numTriesMax + " tries.");
                Cursor.Current = Cursors.Default;
                return;
            }

            //Si llegamos aqui es que todo ha ido bien, generamos todas las vistas y se muestramos las mesas
            GenerateTablesWhitAll(numRounds);
            GenerateSTablesWithNames();
            GenerateSTablesWithTeams();
            GenerateSTablesWithCountries();
            GenerateSTablesWithIds();
            GenerateTablesByPlayer();
            GenerateRivalsByPlayer();

            EnableAll();
            DataGridViewUtils.updateDataGridViewTable(dataGridView, sTablesNames, "Name");
            btnShowNames.Enabled = false;
            btnShowNames.BackColor = Color.FromArgb(224, 224, 224);
            btnShowNames.ForeColor = Color.FromArgb(224, 224, 224);
            Cursor.Current = Cursors.Default;
        }

        private void btnCheckDuplicateRivals_Click(object sender, EventArgs e)
        {
            foreach (Player p in players)
            {
                string[] pRivals = rivalsByPlayer.Find(x => x.playerName.Equals(p.name)).rivalsNames;
                var dict = new Dictionary<string, int>();
                if (pRivals.Distinct().Count() != numRounds * 3)
                {
                    MessageBox.Show(p.name + " have duplicated rivals.");
                    return;
                }
            }
            MessageBox.Show("No duplicated rivals found.");
        }

        private void btnExportTournament_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            DisableAll();

            MessageBox.Show("Please wait until Excel creation finishes.\nA sound will indicate.");
            ExportTournament();
            SystemSounds.Exclamation.Play();

            DataGridViewUtils.updateDataGridViewTable(dataGridView, sTablesNames, "Name");
            EnableAll();
            btnShowNames.Enabled = false;
            btnShowNames.BackColor = Color.FromArgb(224, 224, 224);
            btnShowNames.ForeColor = Color.FromArgb(224, 224, 224);
            Cursor.Current = Cursors.Default;
        }

        private void btnExportScoringTables_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            DisableAll();

            MessageBox.Show("Please wait until Excel creation finishes.\nA sound will indicate.");
            ExportScoringTables();
            SystemSounds.Exclamation.Play();

            DataGridViewUtils.updateDataGridViewTable(dataGridView, sTablesNames, "Name");
            EnableAll();
            btnShowNames.Enabled = false;
            btnShowNames.BackColor = Color.FromArgb(224, 224, 224);
            btnShowNames.ForeColor = Color.FromArgb(224, 224, 224);
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

        #endregion

        #region Filters buttons

        private void btnShowPlayers_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            btnShowPlayers.Enabled = false;
            btnShowPlayers.BackColor = Color.FromArgb(224, 224, 224);
            btnShowPlayers.ForeColor = Color.FromArgb(224, 224, 224);

            DataGridViewUtils.updateDataGridViewPlayer(dataGridView, sPlayers);
            
            btnShowNames.Enabled = true;
            btnShowTeams.Enabled = true;
            btnShowCountries.Enabled = true;
            btnShowIds.Enabled = true;
            btnShowNames.BackColor = Color.FromArgb(0, 177, 106);
            btnShowTeams.BackColor = Color.FromArgb(0, 177, 106);
            btnShowCountries.BackColor = Color.FromArgb(0, 177, 106);
            btnShowIds.BackColor = Color.FromArgb(0, 177, 106);
            btnShowNames.ForeColor = Color.White;
            btnShowTeams.ForeColor = Color.White;
            btnShowCountries.ForeColor = Color.White;
            btnShowIds.ForeColor = Color.White;
            Cursor.Current = Cursors.Default;
        }

        private void btnShowNames_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            btnShowNames.Enabled = false;
            btnShowNames.BackColor = Color.FromArgb(224, 224, 224);
            btnShowNames.ForeColor = Color.FromArgb(224, 224, 224);


            DataGridViewUtils.updateDataGridViewTable(dataGridView, sTablesNames, "Name");

            btnShowPlayers.Enabled = true;
            btnShowTeams.Enabled = true;
            btnShowCountries.Enabled = true;
            btnShowIds.Enabled = true;
            btnShowPlayers.BackColor = Color.FromArgb(0, 177, 106);
            btnShowTeams.BackColor = Color.FromArgb(0, 177, 106);
            btnShowCountries.BackColor = Color.FromArgb(0, 177, 106);
            btnShowIds.BackColor = Color.FromArgb(0, 177, 106);
            btnShowPlayers.ForeColor = Color.White;
            btnShowTeams.ForeColor = Color.White;
            btnShowCountries.ForeColor = Color.White;
            btnShowIds.ForeColor = Color.White;
            Cursor.Current = Cursors.Default;
        }

        private void btnShowTeams_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            btnShowTeams.Enabled = false;
            btnShowTeams.BackColor = Color.FromArgb(224, 224, 224);
            btnShowTeams.ForeColor = Color.FromArgb(224, 224, 224);

            DataGridViewUtils.updateDataGridViewTable(dataGridView, sTablesTeams, "Team");

            btnShowPlayers.Enabled = true;
            btnShowNames.Enabled = true;
            btnShowCountries.Enabled = true;
            btnShowIds.Enabled = true;
            btnShowPlayers.BackColor = Color.FromArgb(0, 177, 106);
            btnShowNames.BackColor = Color.FromArgb(0, 177, 106);
            btnShowCountries.BackColor = Color.FromArgb(0, 177, 106);
            btnShowIds.BackColor = Color.FromArgb(0, 177, 106);
            btnShowPlayers.ForeColor = Color.White;
            btnShowNames.ForeColor = Color.White;
            btnShowCountries.ForeColor = Color.White;
            btnShowIds.ForeColor = Color.White;
            Cursor.Current = Cursors.Default;
        }

        private void btnShowCountries_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            btnShowCountries.Enabled = false;
            btnShowCountries.BackColor = Color.FromArgb(224, 224, 224);
            btnShowCountries.ForeColor = Color.FromArgb(224, 224, 224);

            DataGridViewUtils.updateDataGridViewTable(dataGridView, sTablesCountries, "Country");

            btnShowPlayers.Enabled = true;
            btnShowNames.Enabled = true;
            btnShowTeams.Enabled = true;
            btnShowIds.Enabled = true;
            btnShowPlayers.BackColor = Color.FromArgb(0, 177, 106);
            btnShowNames.BackColor = Color.FromArgb(0, 177, 106);
            btnShowTeams.BackColor = Color.FromArgb(0, 177, 106);
            btnShowIds.BackColor = Color.FromArgb(0, 177, 106);
            btnShowPlayers.ForeColor = Color.White;
            btnShowNames.ForeColor = Color.White;
            btnShowTeams.ForeColor = Color.White;
            btnShowIds.ForeColor = Color.White;
            Cursor.Current = Cursors.Default;
        }
        
        private void btnShowIds_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            btnShowIds.Enabled = false;
            btnShowIds.BackColor = Color.FromArgb(224, 224, 224);
            btnShowIds.ForeColor = Color.FromArgb(224, 224, 224);

            DataGridViewUtils.updateDataGridViewTable(dataGridView, sTablesIds, "Id");

            btnShowPlayers.Enabled = true;
            btnShowNames.Enabled = true;
            btnShowTeams.Enabled = true;
            btnShowCountries.Enabled = true;
            btnShowPlayers.BackColor = Color.FromArgb(0, 177, 106);
            btnShowNames.BackColor = Color.FromArgb(0, 177, 106);
            btnShowTeams.BackColor = Color.FromArgb(0, 177, 106);
            btnShowCountries.BackColor = Color.FromArgb(0, 177, 106);
            btnShowPlayers.ForeColor = Color.White;
            btnShowNames.ForeColor = Color.White;
            btnShowTeams.ForeColor = Color.White;
            btnShowCountries.ForeColor = Color.White;
            Cursor.Current = Cursors.Default;
        }

        #endregion

        #region Calculate tournament methods

        private int GenerateTournament(int numRounds)
        {
            //Limpiamos las tablas
            tablePlayers.Clear();
            tablesWithAll.Clear();
            sTablesNames.Clear();
            sTablesTeams.Clear();
            sTablesCountries.Clear();
            sTablesIds.Clear();
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

        private int ImportPlayer(string ruta)
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

        private void AddNewPlayerFromExcel(DataRow row)
        {
            players.Add(new Player(
                row.IsNull(0) || string.IsNullOrWhiteSpace(row[0].ToString()) ? "" : row[0].ToString(),
                row.IsNull(1) || string.IsNullOrWhiteSpace(row[1].ToString()) ? "" : row[1].ToString(),
                row.IsNull(2) || string.IsNullOrWhiteSpace(row[2].ToString()) ? "" : row[2].ToString(),
                row.IsNull(3) || string.IsNullOrWhiteSpace(row[3].ToString()) ? "" : row[3].ToString()
                ));
        }

        private void ExportTournament()
        {
            try
            { 
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
                    newSheet.Name = string.Format("Round{0}", currentRound);
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
                        newSheet.Cells[1, 3] = "Player1Name";
                        newSheet.Cells[1, 4] = "Player2Name";
                        newSheet.Cells[1, 5] = "Player3Name";
                        newSheet.Cells[1, 6] = "Player4Name";
                    }
                    if (chckBxTeams.Checked)
                    {
                        newSheet.Cells[1, 7] = "Player1Team";
                        newSheet.Cells[1, 8] = "Player2Team";
                        newSheet.Cells[1, 9] = "Player3Team";
                        newSheet.Cells[1, 10] = "Player4Team";
                    }
                    if (chckBxCountries.Checked)
                    {
                        newSheet.Cells[1, 11] = "Player1Country";
                        newSheet.Cells[1, 12] = "Player2Country";
                        newSheet.Cells[1, 13] = "Player3Country";
                        newSheet.Cells[1, 14] = "Player4Country";
                    }
                    if (chckBxIds.Checked)
                    {
                        newSheet.Cells[1, 15] = "Player1Id";
                        newSheet.Cells[1, 16] = "Player2Id";
                        newSheet.Cells[1, 17] = "Player3Id";
                        newSheet.Cells[1, 18] = "Player4Id";
                    }

                    //Write data
                    var currentRoundTables = tablesWithAll.FindAll(x => x.roundId == currentRound).ToList();

                    for (currentTable = 1; currentTable <= tablesWithAll.Select(x => x.tableId).Distinct().Count(); currentTable++)
                    {
                        newSheet.Cells[currentTable + 1, 1] = currentRoundTables[currentTable - 1].roundId;
                        newSheet.Cells[currentTable + 1, 2] = currentRoundTables[currentTable - 1].tableId;
                        if (chckBxNames.Checked)
                        {
                            newSheet.Cells[currentTable + 1, 3] = currentRoundTables[currentTable - 1].player1Name;
                            newSheet.Cells[currentTable + 1, 4] = currentRoundTables[currentTable - 1].player2Name;
                            newSheet.Cells[currentTable + 1, 5] = currentRoundTables[currentTable - 1].player3Name;
                            newSheet.Cells[currentTable + 1, 6] = currentRoundTables[currentTable - 1].player4Name;
                        }
                        if (chckBxTeams.Checked)
                        {
                            newSheet.Cells[currentTable + 1, 7] = currentRoundTables[currentTable - 1].player1Team;
                            newSheet.Cells[currentTable + 1, 8] = currentRoundTables[currentTable - 1].player2Team;
                            newSheet.Cells[currentTable + 1, 9] = currentRoundTables[currentTable - 1].player3Team;
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

                    //Paint odd lines
                    for (int i = 1; i <= newSheet.UsedRange.Rows.Count; i++)
                    {
                        if (i > 1 && i % 2 != 0)
                            newSheet.UsedRange.Rows[i].Cells.Interior.Color = ColorTranslator.ToOle(Color.FromArgb(224, 224, 224));
                    }
                }                
            
                //Write Tournament data by players
                WriteToExcelTablesByPlayers(excelSheets);
                WriteToExcelRivals(excelSheets);

                //Now save the excel
                string excelName = "Tournament_" + makingDate;
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
                    MessageBox.Show("Excel file couldn't be saved.");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Something was wrong, please try again.");
                return;
            }
        }

        private void ExportScoringTables()
        {
            try
            {
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
                for (currentRound = 1;
                    currentRound <= tablesWithAll.Select(x => x.roundId).Distinct().Count();
                    currentRound++)
                {
                    //Adding new Worksheet and deleting existing
                    var newSheet = (NsExcel.Worksheet)excelSheets.Add(Type.Missing,
                        excelSheets[excelSheets.Count], Type.Missing, Type.Missing);
                    newSheet.Name = string.Format("Round{0}", currentRound);
                    if (currentRound == 1)
                    {
                        while (excelSheets.Count > 1)
                        {
                            excelSheets[excelSheets.Count - 1].Delete();
                        }
                    }

                    //Write headers
                    newSheet.Cells[1, 1] = "Round";
                    newSheet.Cells[1, 2] = "Id";
                    newSheet.Cells[1, 3] = "Name";
                    newSheet.Cells[1, 4] = "Points";
                    newSheet.Cells[1, 5] = "Score";

                    //Write data
                    var currentRoundTables = tablesWithAll.FindAll(x => x.roundId == currentRound).ToList();

                    foreach (Player p in players)
                    {
                        newSheet.Cells[p.id + 1, 1] = currentRound;
                        newSheet.Cells[p.id + 1, 2] = p.id;
                        newSheet.Cells[p.id + 1, 3] = p.name;
                        newSheet.Cells[p.id + 1, 4] = p.id;
                        newSheet.Cells[p.id + 1, 5] = p.id * 5;
                    }

                    //Resize columns
                    newSheet.Cells[1, 1].ColumnWidth = 6;
                    newSheet.Cells[1, 2].ColumnWidth = 6;
                    newSheet.Cells[1, 3].ColumnWidth = 32;
                    newSheet.Cells[1, 4].ColumnWidth = 9;
                    newSheet.Cells[1, 5].ColumnWidth = 9;

                    //Paint headers
                    newSheet.UsedRange.Rows[1].Cells.Interior.Color = ColorTranslator.ToOle(Color.FromArgb(0, 177, 106));
                    newSheet.UsedRange.Rows[1].Cells.Font.Color = ColorTranslator.ToOle(Color.White);
                    newSheet.UsedRange.Rows[1].Cells.Font.Bold = true;

                    //Paint odd lines
                    for (int i = 1; i <= newSheet.UsedRange.Rows.Count; i++)
                    {
                        if (i > 1 && i % 2 != 0)
                            newSheet.UsedRange.Rows[i].Cells.Interior.Color = ColorTranslator.ToOle(Color.FromArgb(224, 224, 224));
                    }
                }

                GeneratePlayersTotalsSheet(excelSheets);
                GenerateTeamTotalsSheet(excelSheets);

                //Saving file
                string excelName = "Score_Tables" + makingDate;
                excelWorkBook.SaveAs(excelName,
                    NsExcel.XlFileFormat.xlWorkbookNormal);
                try
                {
                    excelWorkBook.SaveCopyAs(
                        Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                        + "\\" + excelName + ".xls");
                }
                catch(Exception e)
                {
                    MessageBox.Show("Excel file couldn't be saved.");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Something was wrong, please try again.");
                return;
            }
        }

        private void GeneratePlayersTotalsSheet(NsExcel.Sheets excelSheets)
        {
            //Create the Players total points sheet
            var playersTotalScoreSheet = (NsExcel.Worksheet)excelSheets.Add(Type.Missing,
                    excelSheets[excelSheets.Count], Type.Missing, Type.Missing);
            playersTotalScoreSheet.Name = "PlayersTotal";

            //Write headers
            playersTotalScoreSheet.Cells[1, 1] = "Id";
            playersTotalScoreSheet.Cells[1, 2] = "Name";
            playersTotalScoreSheet.Cells[1, 3] = "Points";
            playersTotalScoreSheet.Cells[1, 4] = "Score";
            playersTotalScoreSheet.Cells[1, 5] = "Team";
            playersTotalScoreSheet.Cells[1, 6] = "Country";

            //Write data
            foreach (Player p in players)
            {
                string cellId = (p.id + 1).ToString();
                playersTotalScoreSheet.Cells[cellId, 1] = p.id;
                playersTotalScoreSheet.Cells[cellId, 2] = p.name;                
                NsExcel.Range selectedRange = null;
                try
                {
                    selectedRange = (NsExcel.Range)playersTotalScoreSheet.Cells[cellId, 3];
                    selectedRange.Formula = string.Format("=Sum(Round1:Round{0}!D{1}", numRounds, cellId);
                    selectedRange = (NsExcel.Range)playersTotalScoreSheet.Cells[cellId, 4];
                    selectedRange.Formula = string.Format("=Sum(Round1:Round{0}!E{1}", numRounds, cellId);
                }
                catch (Exception e)
                {
                    string stacktrace = e.StackTrace;
                }
                finally
                {
                    if (selectedRange != null) Marshal.ReleaseComObject(selectedRange);
                }
                playersTotalScoreSheet.Cells[cellId, 5] = p.team;
                playersTotalScoreSheet.Cells[cellId, 6] = p.country;
            }

            //Resize columns
            playersTotalScoreSheet.Cells[1, 1].ColumnWidth = 6;
            playersTotalScoreSheet.Cells[1, 2].ColumnWidth = 32;
            playersTotalScoreSheet.Cells[1, 3].ColumnWidth = 9;
            playersTotalScoreSheet.Cells[1, 4].ColumnWidth = 9;
            playersTotalScoreSheet.Cells[1, 5].ColumnWidth = 24;
            playersTotalScoreSheet.Cells[1, 6].ColumnWidth = 12;

            //Paint headers
            playersTotalScoreSheet.UsedRange.Rows[1].Cells.Interior.Color = ColorTranslator.ToOle(Color.FromArgb(0, 177, 106));
            playersTotalScoreSheet.UsedRange.Rows[1].Cells.Font.Color = ColorTranslator.ToOle(Color.White);
            playersTotalScoreSheet.UsedRange.Rows[1].Cells.Font.Bold = true;

            //Paint odd lines
            for (int i = 1; i <= playersTotalScoreSheet.UsedRange.Rows.Count; i++)
            {
                if (i > 1 && i % 2 != 0)
                    playersTotalScoreSheet.UsedRange.Rows[i].Cells.Interior.Color = ColorTranslator.ToOle(Color.FromArgb(224, 224, 224));
            }

            //Align content to center
            playersTotalScoreSheet.Cells[1, 3] = NsExcel.XlHAlign.xlHAlignCenter;
            playersTotalScoreSheet.Cells[1, 4] = NsExcel.XlHAlign.xlHAlignCenter;
        }

        private void GenerateTeamTotalsSheet(NsExcel.Sheets excelSheets)
        {
            //Create the Teams total score sheet
            var TeamsTotalScoreSheet = (NsExcel.Worksheet)excelSheets.Add(Type.Missing,
                    excelSheets[excelSheets.Count], Type.Missing, Type.Missing);
            TeamsTotalScoreSheet.Name = "TeamsTotal";

            //Write headers
            TeamsTotalScoreSheet.Cells[1, 1] = "Team";
            TeamsTotalScoreSheet.Cells[1, 2] = "Points";
            TeamsTotalScoreSheet.Cells[1, 3] = "Score";
            

            //Write data
            string[] teams = players.Select(x => x.team).Distinct().ToArray();
            for (int i = 1; i <= teams.Length; i++)
            {
                TeamsTotalScoreSheet.Cells[i + 1, 1] = teams[i - 1];
                NsExcel.Range selectedRange = null;
                try
                {
                    selectedRange = (NsExcel.Range)TeamsTotalScoreSheet.Cells[i + 1, 2];
                    selectedRange.Formula = string.Format("=SUMIF(PlayersTotal!E2:E61, A{0}, PlayersTotal!C2:C61)", i + 1);
                    selectedRange = (NsExcel.Range)TeamsTotalScoreSheet.Cells[i + 1, 3];
                    selectedRange.Formula = string.Format("=SUMIF(PlayersTotal!E2:E61, A{0}, PlayersTotal!D2:D61)", i + 1);
                }
                catch (Exception e)
                {
                    string stacktrace = e.StackTrace;
                }
                finally
                {
                    if (selectedRange != null) Marshal.ReleaseComObject(selectedRange);
                }
            }

            //Resize columns
            TeamsTotalScoreSheet.Cells[1, 1].ColumnWidth = 32;
            TeamsTotalScoreSheet.Cells[1, 2].ColumnWidth = 9;
            TeamsTotalScoreSheet.Cells[1, 3].ColumnWidth = 9;

            //Paint headers
            TeamsTotalScoreSheet.UsedRange.Rows[1].Cells.Interior.Color = ColorTranslator.ToOle(Color.FromArgb(0, 177, 106));
            TeamsTotalScoreSheet.UsedRange.Rows[1].Cells.Font.Color = ColorTranslator.ToOle(Color.White);
            TeamsTotalScoreSheet.UsedRange.Rows[1].Cells.Font.Bold = true;

            //Align content to center
            TeamsTotalScoreSheet.Cells[1, 2] = NsExcel.XlHAlign.xlHAlignCenter;
            TeamsTotalScoreSheet.Cells[1, 3] = NsExcel.XlHAlign.xlHAlignCenter;

            //Paint odd lines
            for (int i = 1; i <= TeamsTotalScoreSheet.UsedRange.Rows.Count; i++)
            {
                if (i > 1 && i % 2 != 0)
                    TeamsTotalScoreSheet.UsedRange.Rows[i].Cells.Interior.Color = ColorTranslator.ToOle(Color.FromArgb(224, 224, 224));
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

            //Write data

            for (int i = 0; i < tablesByPlayer.Count; i++)
            {
                TableWithAll twa = tablesByPlayer[i];

                newSheet.Cells[i + 2, 1] = twa.roundId;
                newSheet.Cells[i + 2, 2] = twa.tableId;
                newSheet.Cells[i + 2, 3] = twa.player1Name;
                newSheet.Cells[i + 2, 4] = twa.player2Name;
                newSheet.Cells[i + 2, 5] = twa.player3Name;
                newSheet.Cells[i + 2, 6] = twa.player4Name;
                newSheet.Cells[i + 2, 7] = twa.player1Team;
                newSheet.Cells[i + 2, 8] = twa.player2Team;
                newSheet.Cells[i + 2, 9] = twa.player3Team;
                newSheet.Cells[i + 2, 10] = twa.player4Team;
                newSheet.Cells[i + 2, 11] = twa.player1Country;
                newSheet.Cells[i + 2, 12] = twa.player2Country;
                newSheet.Cells[i + 2, 13] = twa.player3Country;
                newSheet.Cells[i + 2, 14] = twa.player4Country;
                newSheet.Cells[i + 2, 15] = twa.player1Id;
                newSheet.Cells[i + 2, 16] = twa.player2Id;
                newSheet.Cells[i + 2, 17] = twa.player3Id;
                newSheet.Cells[i + 2, 18] = twa.player4Id;
            }

            //Paint headers
            newSheet.UsedRange.Rows[1].Cells.Interior.Color = ColorTranslator.ToOle(Color.FromArgb(0, 177, 106));
            newSheet.UsedRange.Rows[1].Cells.Font.Color = ColorTranslator.ToOle(Color.White);
            newSheet.UsedRange.Rows[1].Cells.Font.Bold = true;

            //Paint odd lines
            for (int i = 1; i <= newSheet.UsedRange.Rows.Count; i++)
            {
                if (i > 1 && i % 2 != 0)
                    newSheet.UsedRange.Rows[i].Cells.Interior.Color = ColorTranslator.ToOle(Color.FromArgb(224, 224, 224));
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

            //Paint headers
            newSheet.UsedRange.Rows[1].Cells.Interior.Color = ColorTranslator.ToOle(Color.FromArgb(0, 177, 106));
            newSheet.UsedRange.Rows[1].Cells.Font.Color = ColorTranslator.ToOle(Color.White);
            newSheet.UsedRange.Rows[1].Cells.Font.Bold = true;

            //Paint odd lines
            for (int i = 1; i <= newSheet.UsedRange.Rows.Count; i++)
            {
                if (i > 1 && i % 2 != 0)
                    newSheet.UsedRange.Rows[i].Cells.Interior.Color = ColorTranslator.ToOle(Color.FromArgb(224, 224, 224));
            }

            //Bold first column
            newSheet.UsedRange.Columns[1].Cells.Font.Bold = true;

            //Resize columns
            newSheet.UsedRange.EntireColumn.AutoFit();
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
            btnCheckDuplicateRivals.Enabled = true;
            btnExportTournament.Enabled = true;
            btnExportScoringTables.Enabled = true;
            numUpDownRounds.Enabled = true;
            numUpDownTriesMax.Enabled = true;
            chckBxNames.Enabled = true;
            chckBxTeams.Enabled = true;
            chckBxCountries.Enabled = true;
            chckBxIds.Enabled = true;
            btnShowPlayers.Enabled = true;
            btnShowNames.Enabled = true;
            btnShowTeams.Enabled = true;
            btnShowCountries.Enabled = true;
            btnShowIds.Enabled = true;

            btnGetExcelTemplate.BackColor = Color.FromArgb(0, 177, 106);
            btnImportExcel.BackColor = Color.FromArgb(0, 177, 106);
            btnCalculate.BackColor = Color.FromArgb(0, 177, 106);
            btnCheckDuplicateRivals.BackColor = Color.FromArgb(0, 177, 106);
            btnImportExcel.BackColor = Color.FromArgb(0, 177, 106);
            btnExportTournament.BackColor = Color.FromArgb(0, 177, 106);
            btnExportScoringTables.BackColor = Color.FromArgb(0, 177, 106);
            btnShowPlayers.BackColor = Color.FromArgb(0, 177, 106);
            btnShowNames.BackColor = Color.FromArgb(0, 177, 106);
            btnShowTeams.BackColor = Color.FromArgb(0, 177, 106);
            btnShowCountries.BackColor = Color.FromArgb(0, 177, 106);
            btnShowIds.BackColor = Color.FromArgb(0, 177, 106);
            btnGetExcelTemplate.ForeColor = Color.White;
            btnImportExcel.ForeColor = Color.White;
            btnCalculate.ForeColor = Color.White;
            btnCheckDuplicateRivals.ForeColor = Color.White;
            btnExportTournament.ForeColor = Color.White;
            btnExportScoringTables.ForeColor = Color.White;
            btnShowPlayers.ForeColor = Color.White;
            btnShowNames.ForeColor = Color.White;
            btnShowTeams.ForeColor = Color.White;
            btnShowCountries.ForeColor = Color.White;
            btnShowIds.ForeColor = Color.White;
        }

        private void DisableAll()
        {
            btnGetExcelTemplate.Enabled = false;
            btnImportExcel.Enabled = false;
            btnCalculate.Enabled = false;
            btnCheckDuplicateRivals.Enabled = false;
            btnExportTournament.Enabled = false;
            btnExportScoringTables.Enabled = false;
            chckBxNames.Enabled = false;
            chckBxTeams.Enabled = false;
            chckBxCountries.Enabled = false;
            chckBxIds.Enabled = false;
            btnShowPlayers.Enabled = false;
            btnShowNames.Enabled = false;
            btnShowTeams.Enabled = false;
            btnShowCountries.Enabled = false;
            btnShowIds.Enabled = false;

            btnGetExcelTemplate.BackColor = Color.FromArgb(224, 224, 224);
            btnImportExcel.BackColor = Color.FromArgb(224, 224, 224);
            btnCalculate.BackColor = Color.FromArgb(224, 224, 224);
            btnCheckDuplicateRivals.BackColor = Color.FromArgb(224, 224, 224);
            btnExportTournament.BackColor = Color.FromArgb(224, 224, 224);
            btnExportScoringTables.BackColor = Color.FromArgb(224, 224, 224);
            btnShowPlayers.BackColor = Color.FromArgb(224, 224, 224);
            btnShowNames.BackColor = Color.FromArgb(224, 224, 224);
            btnShowTeams.BackColor = Color.FromArgb(224, 224, 224);
            btnShowCountries.BackColor = Color.FromArgb(224, 224, 224);
            btnShowIds.BackColor = Color.FromArgb(224, 224, 224);

            btnGetExcelTemplate.ForeColor = Color.FromArgb(224, 224, 224);
            btnImportExcel.ForeColor = Color.FromArgb(224, 224, 224);
            btnCalculate.ForeColor = Color.FromArgb(224, 224, 224);
            btnCheckDuplicateRivals.ForeColor = Color.FromArgb(224, 224, 224);
            btnExportTournament.ForeColor = Color.FromArgb(224, 224, 224);
            btnExportScoringTables.ForeColor = Color.FromArgb(224, 224, 224);
            btnShowPlayers.ForeColor = Color.FromArgb(224, 224, 224);
            btnShowNames.ForeColor = Color.FromArgb(224, 224, 224);
            btnShowTeams.ForeColor = Color.FromArgb(224, 224, 224);
            btnShowCountries.ForeColor = Color.FromArgb(224, 224, 224);
            btnShowIds.ForeColor = Color.FromArgb(224, 224, 224);
        }

        private void GenerateSPlayers()
        {
            foreach (Player p in players)
            {
                sPlayers.Add(new string[] { p.id.ToString(), p.name, p.team, p.country });
            }
        }

        private void GenerateSTablesWithNames()
        {
            foreach (TableWithAll t in tablesWithAll)
            {
                sTablesNames.Add(new string[] {
                    t.roundId.ToString(),
                    t.tableId.ToString(),
                    t.player1Name.ToString(),
                    t.player2Name.ToString(),
                    t.player3Name.ToString(),
                    t.player4Name.ToString(), });
            }
        }

        private void GenerateSTablesWithTeams()
        {
            foreach (TableWithAll t in tablesWithAll)
            {
                sTablesTeams.Add(new string[] {
                    t.roundId.ToString(),
                    t.tableId.ToString(),
                    t.player1Team.ToString(),
                    t.player2Team.ToString(),
                    t.player3Team.ToString(),
                    t.player4Team.ToString(), });
            }
        }

        private void GenerateSTablesWithCountries()
        {
            foreach (TableWithAll t in tablesWithAll)
            {
                sTablesCountries.Add(new string[] {
                    t.roundId.ToString(),
                    t.tableId.ToString(),
                    t.player1Country.ToString(),
                    t.player2Country.ToString(),
                    t.player3Country.ToString(),
                    t.player4Country.ToString(), });
            }
        }

        private void GenerateSTablesWithIds()
        {
            foreach (TableWithAll t in tablesWithAll)
            {
                sTablesIds.Add(new string[] {
                    t.roundId.ToString(),
                    t.tableId.ToString(),
                    t.player1Id.ToString(),
                    t.player2Id.ToString(),
                    t.player3Id.ToString(),
                    t.player4Id.ToString(), });
            }
        }

        #endregion
    }
}