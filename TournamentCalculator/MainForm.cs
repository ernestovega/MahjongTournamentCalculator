using FastMember;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
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
        private List<Player> playersNoUsadosEstaRonda;
        private List<Table> tables = new List<Table>(); 
        private List<TableWithNamesOnly> tablesWithNamesOnly;
        private List<TableWithAll> tablesWithAll;       
        private int currentRound;
        private int currentTable;
        private Random random = new Random();
        private int triesCounter2;
        private int triesCounter3;
        private int triesCounter4;
        private List<TableWithTeamsOnly> tablesWithTeamsOnly;
        private List<TableWithCountriesOnly> tablesWithCountriesOnly;
        private string path;
        private int countTries = 0;

        #endregion

        #region Public methods

        public MainForm()
        {
            InitializeComponent();

            players = new List<Player>();
            tables = new List<Table>();
            playersNoUsadosEstaRonda = new List<Player>();
            tablesWithNamesOnly = new List<TableWithNamesOnly>();
            tablesWithAll = new List<TableWithAll>();

            DataGridViewUtils.updateDataGridView(dataGridView, new List<Player>() {
                new Player("1", "Example name", "Example Country", "Example Team")});
        }

        #endregion

        #region Events

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (!RequestFile(ref path))
            {
                Cursor.Current = Cursors.Default;
                return;
            }

            btnImportExcel.Enabled = false;
            btnCalculate.Enabled = false;
            btnFindDuplicates.Enabled = false;
            btnPlayerRivals.Enabled = false;
            btnExportar.Enabled = false;
            btnShowNames.Enabled = false;
            btnShowTeams.Enabled = false;
            btnShowCountries.Enabled = false;
            btnShowAll.Enabled = false;

            players.Clear();
            playersNoUsadosEstaRonda.Clear();
            lblPlayers.Text = string.Empty;
            lblTables.Text = string.Empty;
            countTries = 0;

            ImportExcel(path);

            lblPlayers.Text = "Players: " + players.Count;
            if (players.Count % 4 != 0)
            {
                MessageBox.Show("The number of players must be a multiple of 4.\nCheck the Excel.");
            }
            else
            {
                lblTables.Text = "Tables: " + players.Count / 4;
                btnCalculate.Enabled = true;
                numUpDownRounds.Enabled = true;
                numUpDownTriesMax.Enabled = true;
            }

            btnImportExcel.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            numUpDownRounds.Enabled = false;
            numUpDownTriesMax.Enabled = false;
            btnCalculate.Enabled = false;
            btnFindDuplicates.Enabled = false;
            btnPlayerRivals.Enabled = false;
            btnExportar.Enabled = false;

            countTries = 0;
            int numTriesMax = decimal.ToInt32(numUpDownTriesMax.Value);
            int numRounds = decimal.ToInt32(numUpDownRounds.Value);
            int result = -1;
            while (result < 0 && countTries < numTriesMax)
            {
                countTries++;
                result = generateTournament(numRounds);
            }
            if (countTries >= numTriesMax)
            {
                MessageBox.Show("Can't calculate tournament after " + numTriesMax + " tries.");
                lblTriesNeeded.Text = "Tries needed: " + countTries.ToString();
                numUpDownRounds.Enabled = true;
                numUpDownTriesMax.Enabled = true;
                btnCalculate.Enabled = true;
                DataGridViewUtils.updateDataGridView(dataGridView, players);
                return;
            }
            lblTriesNeeded.Text = "Tries needed: " + countTries.ToString();

            updateTablesWithAll();
            updateTablesWithNamesOnly();
            updateTablesWithTeamsOnly();
            updateTablesWithCountriesOnly();

            numUpDownRounds.Enabled = true;
            numUpDownTriesMax.Enabled = true;
            btnCalculate.Enabled = true;
            btnExportar.Enabled = true;
            btnPlayerRivals.Enabled = true;
            btnFindDuplicates.Enabled = true;
            btnShowNames.Enabled = true;
            btnShowTeams.Enabled = true;
            btnShowCountries.Enabled = true;
            btnShowAll.Enabled = true;

            btnShowNames.PerformClick();
            Cursor.Current = Cursors.Default;
        }

        private void btnFindDuplicates_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            btnFindDuplicates.Enabled = false;

            List<string> duplicados = new List<string>();
            int numTablesPerRound = players.Count / 4;
            int numRounds = tables.Count / numTablesPerRound;
            for (int i = 1; i <= numRounds; i++)
            {
                string dups = "";
                List<int> readedPlayersInThisRound = new List<int>();
                List<Table> roundTables = tables.FindAll(x => x.roundId == i);

                foreach (Table table in roundTables)
                {
                    if (readedPlayersInThisRound.Contains(table.player1Id))
                        dups += players.Find(x => x.id == table.player1Id).name + ", ";
                    else
                        readedPlayersInThisRound.Add(table.player1Id);

                    if (readedPlayersInThisRound.Contains(table.player2Id))
                        dups += players.Find(x => x.id == table.player2Id).name + ", ";
                    else
                        readedPlayersInThisRound.Add(table.player2Id);

                    if (readedPlayersInThisRound.Contains(table.player3Id))
                        dups += players.Find(x => x.id == table.player3Id).name + ", ";
                    else
                        readedPlayersInThisRound.Add(table.player3Id);

                    if (readedPlayersInThisRound.Contains(table.player4Id))
                        dups += players.Find(x => x.id == table.player4Id).name + ", ";
                    else
                        readedPlayersInThisRound.Add(table.player4Id);
                }
                duplicados.Add(dups);
            }
            string message = "";
            for(int i = 1; i <= duplicados.Count; i++)
            {
                message += "Round " + i + ": ";

                if (string.IsNullOrEmpty(duplicados[i - 1]))
                    message += "0";
                else
                    message += duplicados[i - 1];

                message += "\n";
            }
            MessageBox.Show(message);

            btnFindDuplicates.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void btnPlayerRivals_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            btnPlayerRivals.Enabled = false;

            var listRivals = new List<TableWithNamesOnly>();
            foreach (var p in players)
            {
                var lista = tablesWithNamesOnly.FindAll(x => p.name == x.player1Name ||
                p.name == x.player2Name || p.name == x.player3Name || p.name == x.player4Name);                
                listRivals.AddRange(lista);
            }
            DataGridViewUtils.updateDataGridView(dataGridView, listRivals);

            btnPlayerRivals.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            btnFindDuplicates.Enabled = false;

            DataGridViewUtils.updateDataGridView(dataGridView, tablesWithAll);
            ExportToExcel();

            btnFindDuplicates.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void btnShowNames_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            btnShowNames.Enabled = false;


            DataGridViewUtils.updateDataGridView(dataGridView, tablesWithNamesOnly);

            btnShowTeams.Enabled = true;
            btnShowCountries.Enabled = true;
            btnShowAll.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void btnShowTeams_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            btnShowTeams.Enabled = false;

            DataGridViewUtils.updateDataGridView(dataGridView, tablesWithTeamsOnly);

            btnShowNames.Enabled = true;
            btnShowCountries.Enabled = true;
            btnShowAll.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void btnShowCountries_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            btnShowCountries.Enabled = false;

            DataGridViewUtils.updateDataGridView(dataGridView, tablesWithCountriesOnly);

            btnShowNames.Enabled = true;
            btnShowTeams.Enabled = true;
            btnShowAll.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            btnShowAll.Enabled = false;

            DataGridViewUtils.updateDataGridView(dataGridView, tablesWithAll);

            btnShowNames.Enabled = true;
            btnShowTeams.Enabled = true;
            btnShowCountries.Enabled = true;
        }

        #endregion

        #region Player methods

        private int getRandomPlayer()
        {
            int r = random.Next(playersNoUsadosEstaRonda.Count);
            Player playerNoUsado = playersNoUsadosEstaRonda[r];
            playersNoUsadosEstaRonda.RemoveAt(r);
            return playerNoUsado.id;
        }

        private int getRandomPlayer(int p1)
        {
            List<Player> pNoUsadosYQueNoHanJugadoYaConEste = getPlayersNoUsadosYQueNoHanJugadoYaConEste(p1);
            if (pNoUsadosYQueNoHanJugadoYaConEste.Count == 0)
            {
                return -1;
            }
            int r = random.Next(pNoUsadosYQueNoHanJugadoYaConEste.Count);
            Player playerNoUsado = pNoUsadosYQueNoHanJugadoYaConEste[r];
            int counter = 0;
            while (counter < playersNoUsadosEstaRonda.Count &&
                playerNoUsado.team.ToLower().Equals(players[p1 - 1].team.ToLower()))
            {
                r = random.Next(playersNoUsadosEstaRonda.Count);
                playerNoUsado = playersNoUsadosEstaRonda[r];
                counter++;
            }
            if(counter == playersNoUsadosEstaRonda.Count)
            {
                return -2;
            }
            playersNoUsadosEstaRonda.RemoveAt(r);
            return playerNoUsado.id;
        }

        private int getRandomPlayer(int p1, int p2)
        {
            List<Player> pNoUsadosYQueNoHanJugadoYaConEste = getPlayersNoUsadosYQueNoHanJugadoYaConEste(p1);
            pNoUsadosYQueNoHanJugadoYaConEste = getPlayersNoUsadosYQueNoHanJugadoYaConEste(p2);
            if(pNoUsadosYQueNoHanJugadoYaConEste.Count == 0)
            {
                return -3;
            }
            int r = random.Next(pNoUsadosYQueNoHanJugadoYaConEste.Count);
            Player playerNoUsado = pNoUsadosYQueNoHanJugadoYaConEste[r];
            int counter = 0;
            while (counter < playersNoUsadosEstaRonda.Count &&
                playerNoUsado.team.Equals(players[p1 - 1].team) &&
                playerNoUsado.team.Equals(players[p2 - 1].team))
            {
                r = random.Next(playersNoUsadosEstaRonda.Count);
                playerNoUsado = playersNoUsadosEstaRonda[r];
                counter++;
            }
            if (counter == playersNoUsadosEstaRonda.Count)
            {
                return -4;
            }
            playersNoUsadosEstaRonda.RemoveAt(r);
            return playerNoUsado.id;
        }

        private int getRandomPlayer(int p1, int p2, int p3)
        {
            List<Player> pNoUsadosYQueNoHanJugadoYaConEste = getPlayersNoUsadosYQueNoHanJugadoYaConEste(p1);
            pNoUsadosYQueNoHanJugadoYaConEste = getPlayersNoUsadosYQueNoHanJugadoYaConEste(p2);
            pNoUsadosYQueNoHanJugadoYaConEste = getPlayersNoUsadosYQueNoHanJugadoYaConEste(p3);
            if (pNoUsadosYQueNoHanJugadoYaConEste.Count == 0)
            {
                return -5;
            }
            int r = random.Next(pNoUsadosYQueNoHanJugadoYaConEste.Count);
            Player playerNoUsado = pNoUsadosYQueNoHanJugadoYaConEste[r];
            int counter = 0;
            while (counter < playersNoUsadosEstaRonda.Count &&
                playerNoUsado.team.Equals(players[p1 - 1].team) &&
                playerNoUsado.team.Equals(players[p2 - 1].team) &&
                playerNoUsado.team.Equals(players[p3 - 1].team))
            {
                r = random.Next(playersNoUsadosEstaRonda.Count);
                playerNoUsado = playersNoUsadosEstaRonda[r];
                counter++;
            }
            if (counter == playersNoUsadosEstaRonda.Count)
            {
                return -6;
            }
            playersNoUsadosEstaRonda.RemoveAt(r);
            return playerNoUsado.id;
        }

        private List<Player> getPlayersNoUsadosYQueNoHanJugadoYaConEste(int pId)
        {
            List<Player> playersNoUsadosYQueNoHanJugadoYaConEstos = playersNoUsadosEstaRonda.Select(x => x.Clone()).ToList();
            List<Table> mesasDondeJugo = tables.FindAll(x => x.roundId < currentRound  && (
                pId == x.player1Id || pId == x.player2Id || pId == x.player3Id || pId == x.player4Id));
            List<int> pIdsConQuienJugo = new List<int>();

            foreach(Table mesa in mesasDondeJugo)
            {
                if (mesa.player1Id == pId)
                {
                    pIdsConQuienJugo.Add(mesa.player2Id);
                    pIdsConQuienJugo.Add(mesa.player3Id);
                    pIdsConQuienJugo.Add(mesa.player4Id);
                }
                else if (mesa.player2Id == pId)
                {
                    pIdsConQuienJugo.Add(mesa.player1Id);
                    pIdsConQuienJugo.Add(mesa.player3Id);
                    pIdsConQuienJugo.Add(mesa.player4Id);
                }
                else if (mesa.player3Id == pId)
                {
                    pIdsConQuienJugo.Add(mesa.player1Id);
                    pIdsConQuienJugo.Add(mesa.player2Id);
                    pIdsConQuienJugo.Add(mesa.player4Id);
                }
                else
                {
                    pIdsConQuienJugo.Add(mesa.player1Id);
                    pIdsConQuienJugo.Add(mesa.player2Id);
                    pIdsConQuienJugo.Add(mesa.player3Id);
                }
            }

            foreach(int id in pIdsConQuienJugo)
            {
                Player pAux = playersNoUsadosYQueNoHanJugadoYaConEstos.Find(x => x.id == id);
                if (pAux != null)
                {
                    playersNoUsadosYQueNoHanJugadoYaConEstos.Remove(pAux);
                }                    
            }

            return playersNoUsadosYQueNoHanJugadoYaConEstos;
        }

        private Player getPlayerById(int id)
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

        #endregion

        #region Table methods

        private int generateTournament(int numRounds)
        {
                tables.Clear();
                tablesWithNamesOnly.Clear();
                tablesWithAll.Clear();
                for (currentRound = 1; currentRound <= numRounds; currentRound++)
                {
                    playersNoUsadosEstaRonda = players.Select(x => x.Clone()).ToList();
                    for (currentTable = 1; currentTable <= players.Count / 4; currentTable++)
                    {
                        int p1 = 0, p2 = 0, p3 = 0, p4 = 0;
                        p1 = getRandomPlayer();
                        if (p1 < 0)
                        {
                            return -1;
                        }
                        p2 = getRandomPlayer(p1);
                        if (p2 < 0)
                        {
                            return -2;
                        }
                        p3 = getRandomPlayer(p1, p2);
                        if (p3 < 0)
                        {
                            return -3;
                        }
                        p4 = getRandomPlayer(p1, p2, p3);
                        if(p4 < 0)
                        {
                            return -4;
                        }
                        tables.Add(new Table(currentRound, currentTable, p1, p2, p3, p4));
                    }
                }
                return 0;
        }

        private void updateTablesWithAll()
        {
            tablesWithAll = new List<TableWithAll>();
            foreach (Table t in tables)
            {
                Player p1 = getPlayerById(t.player1Id);
                Player p2 = getPlayerById(t.player2Id);
                Player p3 = getPlayerById(t.player3Id);
                Player p4 = getPlayerById(t.player4Id);
                tablesWithAll.Add(new TableWithAll(
                    t.roundId, t.tableId,
                    p1.id, p2.id, p3.id, p4.id,
                    p1.name, p2.name, p3.name, p4.name,
                    p1.country, p2.country, p3.country, p4.country,
                    p1.team, p2.team, p3.team, p4.team));
            }
        }

        private void updateTablesWithNamesOnly()
        {
            tablesWithNamesOnly = new List<TableWithNamesOnly>();
            foreach (Table t in tables)
            {
                tablesWithNamesOnly.Add(new TableWithNamesOnly(
                    t.roundId,
                    t.tableId,
                    getPlayerById(t.player1Id).name,
                    getPlayerById(t.player2Id).name,
                    getPlayerById(t.player3Id).name,
                    getPlayerById(t.player4Id).name));
            }
        }

        private void updateTablesWithTeamsOnly()
        {
            tablesWithTeamsOnly = new List<TableWithTeamsOnly>();
            foreach (Table t in tables)
            {
                tablesWithTeamsOnly.Add(new TableWithTeamsOnly(
                    t.roundId,
                    t.tableId,
                    getPlayerById(t.player1Id).team,
                    getPlayerById(t.player2Id).team,
                    getPlayerById(t.player3Id).team,
                    getPlayerById(t.player4Id).team));
            }
        }

        private void updateTablesWithCountriesOnly()
        {
            tablesWithCountriesOnly = new List<TableWithCountriesOnly>();
            foreach (Table t in tables)
            {
                tablesWithCountriesOnly.Add(new TableWithCountriesOnly(
                    t.roundId,
                    t.tableId,
                    getPlayerById(t.player1Id).country,
                    getPlayerById(t.player2Id).country,
                    getPlayerById(t.player3Id).country,
                    getPlayerById(t.player4Id).country));
            }
        }

        #endregion

        #region Excel methods

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
                DataGridViewUtils.updateDataGridView(dataGridView, dataTable);
                foreach (DataRow row in dataTable.Rows)
                {
                    players.Add(
                        new Player(
                            row[0].ToString(),
                            row[1].ToString(),
                            row[2].ToString(),
                            row[3].ToString()));
                }
            }
        }

        public void ExportToExcel()
        {
            NsExcel.Application excel;
            NsExcel.Workbook excelworkBook;
            NsExcel.Worksheet excelSheet;
            DataTable dataTable = ConvertToDataTable(tablesWithAll);

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

        private DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
            TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        public void FormattingExcelCells(NsExcel.Range range, string HTMLcolorCode, System.Drawing.Color fontColor, bool IsFontbold)
        {
            range.Interior.Color = System.Drawing.ColorTranslator.FromHtml(HTMLcolorCode);
            range.Font.Color = System.Drawing.ColorTranslator.ToOle(fontColor);
            range.Font.Bold = IsFontbold;
        }

        #endregion
    }
}