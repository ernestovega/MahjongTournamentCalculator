using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
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
        private int currentRound, currentTable, currentTablePlayer;
        private Random random = new Random();
        private int countTries = 0;

        #endregion

        #region Public methods

        public MainForm()
        {
            InitializeComponent();

            DataGridViewUtils.updateDataGridView(dataGridView, new List<Player>() {
                new Player("1", "Example name", "Example Country", "Example Team")});
        }

        #endregion

        #region Events

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            string path = string.Empty;
            if (!RequestFile(ref path))
            {
                Cursor.Current = Cursors.Default;
                return;
            }

            btnImportExcel.Enabled = false;
            btnCalculate.Enabled = false;
            btnExportar.Enabled = false;
            numUpDownRounds.Enabled = false;
            numUpDownTriesMax.Enabled = false;
            btnShowPlayers.Enabled = false;
            btnShowNames.Enabled = false;
            btnShowTeams.Enabled = false;
            btnShowCountries.Enabled = false;
            btnShowAll.Enabled = false;

            players.Clear();
            lblPlayers.Text = string.Empty;
            lblTables.Text = string.Empty;

            ImportExcel(path);

            lblPlayers.Text = "Players: " + players.Count;
            if (players.Count % 4 != 0)
                MessageBox.Show("The number of players must be a multiple of 4.\nCheck the Excel.");
            else
            {
                lblTables.Text = "Tables: " + players.Count / 4;
                btnCalculate.Enabled = true;
                numUpDownRounds.Enabled = true;
                numUpDownTriesMax.Enabled = true;
            }

            btnShowPlayers.PerformClick();

            btnImportExcel.Enabled = true;
            btnShowPlayers.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            btnImportExcel.Enabled = false;
            btnCalculate.Enabled = false;
            btnExportar.Enabled = false;
            numUpDownRounds.Enabled = false;
            numUpDownTriesMax.Enabled = false;
            btnShowPlayers.Enabled = false;
            btnShowNames.Enabled = false;
            btnShowTeams.Enabled = false;
            btnShowCountries.Enabled = false;
            btnShowAll.Enabled = false;
            lblTriesNeeded.Text = "Tries needed:";
            Application.DoEvents();

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
                numUpDownRounds.Enabled = true;
                numUpDownTriesMax.Enabled = true;
                btnCalculate.Enabled = true;
                btnShowPlayers.PerformClick();
                btnImportExcel.Enabled = true;
                numUpDownRounds.Enabled = true;
                numUpDownTriesMax.Enabled = true;
                btnCalculate.Enabled = true;
                MessageBox.Show("Can't calculate tournament after " + numTriesMax + " tries.");
                Cursor.Current = Cursors.Default;
                return;
            }

            //Si llegamos aqui es que todo ha ido bien, generamos todas las vistas y se muestramos las mesas
            generateTablesWhitAll(numRounds);
            generateTablesWhitNames(numRounds);
            generateTablesWhitTeams(numRounds);
            generateTablesWhitCountries(numRounds);

            btnShowNames.Enabled = true;
            btnShowNames.PerformClick();

            btnImportExcel.Enabled = true;
            numUpDownRounds.Enabled = true;
            numUpDownTriesMax.Enabled = true;
            btnCalculate.Enabled = true;
            btnExportar.Enabled = true;
            btnShowPlayers.Enabled = true;
            btnShowTeams.Enabled = true;
            btnShowCountries.Enabled = true;
            btnShowAll.Enabled = true;

            Cursor.Current = Cursors.Default;
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            btnShowAll.PerformClick();

            ExportToExcel();
            
            Cursor.Current = Cursors.Default;
        }

        private void btnShowPlayers_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            btnShowPlayers.Enabled = false;

            DataGridViewUtils.updateDataGridView(dataGridView, players);

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

        private void ImportExcel(string ruta)
        {
            DataTable dataTable = new DataTable();
            string strConnXlsx = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ruta
                + ";Extended Properties=" + '"' + "Excel 12.0 Xml;HDR=YES;IMEX=1" + '"';
            string strConnXls = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ruta
                + ";Extended Properties=" + '"' + "Excel 8.0;HDR=YES;IMEX=1" + '"';
            string sqlExcel;
            string strConn = ruta.Substring(ruta.Length - 4).ToLower().Equals("xlsx")
                ? strConnXlsx : strConnXls;
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();
                var dtSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                var Sheet1 = dtSchema.Rows[0].Field<string>("TABLE_NAME");
                sqlExcel = "SELECT * FROM [" + Sheet1 + "]";
                OleDbDataAdapter oleDbdataAdapter = new OleDbDataAdapter(sqlExcel, conn);
                oleDbdataAdapter.Fill(dataTable);
                foreach (DataRow row in dataTable.Rows)
                {
                    players.Add(
                        new Player(
                            row[0].ToString(),
                            row[1].ToString(),
                            row[2].ToString(),
                            row[3].ToString()));
                }
                btnShowPlayers.PerformClick();
            }
        }

        public void ExportToExcel()
        {
            NsExcel.Application excel;
            NsExcel.Workbook excelworkBook;
            NsExcel.Worksheet excelSheet;

            //start excel
            excel = new NsExcel.Application();

            // for making Excel visible
            excel.Visible = true;
            excel.DisplayAlerts = false;

            // Creation a new Workbook
            excelworkBook = excel.Workbooks.Add(Type.Missing);

            // Work sheet
            excelSheet = (NsExcel.Worksheet)excelworkBook.ActiveSheet;
            excelSheet.Name = "WorkSheet";

            //Write sheet

            excelSheet.Cells[1, 1] = "Round";
            excelSheet.Cells[1, 2] = "Table";
            excelSheet.Cells[1, 3] = "Player1 id";
            excelSheet.Cells[1, 4] = "Player2 id";
            excelSheet.Cells[1, 5] = "Player3 id";
            excelSheet.Cells[1, 6] = "Player4 id";
            excelSheet.Cells[1, 7] = "Player1 name";
            excelSheet.Cells[1, 8] = "Player2 name";
            excelSheet.Cells[1, 9] = "Player3 name";
            excelSheet.Cells[1, 10] = "Player4 name";
            excelSheet.Cells[1, 11] = "Player1 team";
            excelSheet.Cells[1, 12] = "Player2 team";
            excelSheet.Cells[1, 13] = "Player3 team";
            excelSheet.Cells[1, 14] = "Player4 team";
            excelSheet.Cells[1, 15] = "Player1 country";
            excelSheet.Cells[1, 16] = "Player2 country";
            excelSheet.Cells[1, 17] = "Player3 country";
            excelSheet.Cells[1, 18] = "Player4 country";
            for (int i = 1; i <= tablesWithAll.Count; i++)
            {
                excelSheet.Cells[i + 1, 1 ] = tablesWithAll[i - 1].roundId;
                excelSheet.Cells[i + 1, 2 ] = tablesWithAll[i - 1].tableId;
                excelSheet.Cells[i + 1, 3 ] = tablesWithAll[i - 1].player1Id;
                excelSheet.Cells[i + 1, 4 ] = tablesWithAll[i - 1].player2Id;
                excelSheet.Cells[i + 1, 5 ] = tablesWithAll[i - 1].player3Id;
                excelSheet.Cells[i + 1, 6 ] = tablesWithAll[i - 1].player4Id;
                excelSheet.Cells[i + 1, 7 ] = tablesWithAll[i - 1].player1Name;
                excelSheet.Cells[i + 1, 8 ] = tablesWithAll[i - 1].player2Name;
                excelSheet.Cells[i + 1, 9 ] = tablesWithAll[i - 1].player3Name;
                excelSheet.Cells[i + 1, 10] = tablesWithAll[i - 1].player4Name;
                excelSheet.Cells[i + 1, 11] = tablesWithAll[i - 1].player1Team;
                excelSheet.Cells[i + 1, 12] = tablesWithAll[i - 1].player2Team;
                excelSheet.Cells[i + 1, 13] = tablesWithAll[i - 1].player3Team;
                excelSheet.Cells[i + 1, 14] = tablesWithAll[i - 1].player4Team;
                excelSheet.Cells[i + 1, 15] = tablesWithAll[i - 1].player1Country;
                excelSheet.Cells[i + 1, 16] = tablesWithAll[i - 1].player2Country;
                excelSheet.Cells[i + 1, 17] = tablesWithAll[i - 1].player3Country;
                excelSheet.Cells[i + 1, 18] = tablesWithAll[i - 1].player4Country;
            }

            // now we resize the columns
            excelSheet.UsedRange.EntireColumn.AutoFit();
        }

        public void FormattingExcelCells(NsExcel.Range range, string HTMLcolorCode, System.Drawing.Color fontColor, bool IsFontbold)
        {
            range.Interior.Color = System.Drawing.ColorTranslator.FromHtml(HTMLcolorCode);
            range.Font.Color = System.Drawing.ColorTranslator.ToOle(fontColor);
            range.Font.Bold = IsFontbold;
        }

        #endregion

        #region Private methods

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

        private void generateTablesWhitAll(int numRounds)
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

        private void generateTablesWhitNames(int numRounds)
        {
            for (currentRound = 1; currentRound <= numRounds; currentRound++)
            {
                for (currentTable = 1; currentTable <= players.Count / 4; currentTable++)
                {
                    TableWithNames tableWithNames = new TableWithNames();
                    tableWithNames.roundId = currentRound;
                    tableWithNames.tableId = currentTable;
                    for (currentTablePlayer = 1; currentTablePlayer <= 4; currentTablePlayer++)
                    {
                        switch(currentTablePlayer)
                        {
                            case 1:
                                int player1Id = tablePlayers.Find(x => x.round == currentRound &&
                                x.table == currentTable && x.player == currentTablePlayer).playerId;
                                tableWithNames.player1Name = players.Find(x => x.id == player1Id).name;
                                break;
                            case 2:
                                int player2Id = tablePlayers.Find(x => x.round == currentRound &&
                                x.table == currentTable && x.player == currentTablePlayer).playerId;
                                tableWithNames.player2Name = players.Find(x => x.id == player2Id).name;
                                break;
                            case 3:
                                int player3Id = tablePlayers.Find(x => x.round == currentRound &&
                                x.table == currentTable && x.player == currentTablePlayer).playerId;
                                tableWithNames.player3Name = players.Find(x => x.id == player3Id).name;
                                break;
                            case 4:
                                int player4Id = tablePlayers.Find(x => x.round == currentRound &&
                                x.table == currentTable && x.player == currentTablePlayer).playerId;
                                tableWithNames.player4Name = players.Find(x => x.id == player4Id).name;
                                break;
                        }
                    }
                    tablesWithNames.Add(tableWithNames);
                }
            }
        }

        private void generateTablesWhitTeams(int numRounds)
        {
            for (currentRound = 1; currentRound <= numRounds; currentRound++)
            {
                for (currentTable = 1; currentTable <= players.Count / 4; currentTable++)
                {
                    TableWithTeams tableWithTeams = new TableWithTeams();
                    tableWithTeams.roundId = currentRound;
                    tableWithTeams.tableId = currentTable;
                    for (currentTablePlayer = 1; currentTablePlayer <= 4; currentTablePlayer++)
                    {
                        switch (currentTablePlayer)
                        {
                            case 1:
                                int player1Id = tablePlayers.Find(x => x.round == currentRound &&
                                x.table == currentTable && x.player == currentTablePlayer).playerId;
                                tableWithTeams.player1Team = players.Find(x => x.id == player1Id).team;
                                break;
                            case 2:
                                int player2Id = tablePlayers.Find(x => x.round == currentRound &&
                                x.table == currentTable && x.player == currentTablePlayer).playerId;
                                tableWithTeams.player2Team = players.Find(x => x.id == player2Id).team;
                                break;
                            case 3:
                                int player3Id = tablePlayers.Find(x => x.round == currentRound &&
                                x.table == currentTable && x.player == currentTablePlayer).playerId;
                                tableWithTeams.player3Team = players.Find(x => x.id == player3Id).team;
                                break;
                            case 4:
                                int player4Id = tablePlayers.Find(x => x.round == currentRound &&
                                x.table == currentTable && x.player == currentTablePlayer).playerId;
                                tableWithTeams.player4Team = players.Find(x => x.id == player4Id).team;
                                break;
                        }
                    }
                    tablesWithTeams.Add(tableWithTeams);
                }
            }
        }

        private void generateTablesWhitCountries(int numRounds)
        {
            for (currentRound = 1; currentRound <= numRounds; currentRound++)
            {
                for (currentTable = 1; currentTable <= players.Count / 4; currentTable++)
                {
                    TableWithCountries tableWithCountrys = new TableWithCountries();
                    tableWithCountrys.roundId = currentRound;
                    tableWithCountrys.tableId = currentTable;
                    for (currentTablePlayer = 1; currentTablePlayer <= 4; currentTablePlayer++)
                    {
                        switch (currentTablePlayer)
                        {
                            case 1:
                                int player1Id = tablePlayers.Find(x => x.round == currentRound &&
                                x.table == currentTable && x.player == currentTablePlayer).playerId;
                                tableWithCountrys.player1Country = players.Find(x => x.id == player1Id).country;
                                break;
                            case 2:
                                int player2Id = tablePlayers.Find(x => x.round == currentRound &&
                                x.table == currentTable && x.player == currentTablePlayer).playerId;
                                tableWithCountrys.player2Country = players.Find(x => x.id == player2Id).country;
                                break;
                            case 3:
                                int player3Id = tablePlayers.Find(x => x.round == currentRound &&
                                x.table == currentTable && x.player == currentTablePlayer).playerId;
                                tableWithCountrys.player3Country = players.Find(x => x.id == player3Id).country;
                                break;
                            case 4:
                                int player4Id = tablePlayers.Find(x => x.round == currentRound &&
                                x.table == currentTable && x.player == currentTablePlayer).playerId;
                                tableWithCountrys.player4Country = players.Find(x => x.id == player4Id).country;
                                break;
                        }
                    }
                    tablesWithCountries.Add(tableWithCountrys);
                }
            }
        }

        #endregion
    }
}