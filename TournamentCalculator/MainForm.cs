using FastMember;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NsExcel = Microsoft.Office.Interop.Excel;

namespace TournamentCalculator
{
    public partial class MainForm : Form
    {
        #region Fields

        private List<Player> players = new List<Player>();
        private List<Table> tables = new List<Table>();

        private int numPlayers;
        private int numTables;
        private int numRounds = 1;

        private int currentRound;
        private int currentTable;

        private List<Player> playersNoUsadosEstaRonda;
        Random random = new Random();
        private List<TableWithNamesOnly> tablesWithNamesOnly;
        private List<TableWithAll> tablesWithAll;

        #endregion

        #region Public methods

        public MainForm()
        {
            InitializeComponent();
            DataTable dataTable = new DataTable();
            using (var reader = ObjectReader.Create(new List<Player>() {
                new Player(1.ToString(), "Example name", "Example Country", "Example Team") }))
            {
                try
                {
                    dataTable.Load(reader);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            dataGridView.DataSource = dataTable;
            dataGridView.Columns["Id"].DisplayIndex = 0;
            dataGridView.Columns["Name"].DisplayIndex = 1;
            dataGridView.Columns["Team"].DisplayIndex = 2;
            dataGridView.Columns["Country"].DisplayIndex = 3;
        }

        #endregion

        #region Events

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            numPlayers = 0;
            numTables = 0;
            lblPlayers.Text = string.Empty;
            lblTables.Text = string.Empty;
            btnCalculate.Enabled = false;
            btnExportar.Enabled = false;
            string ruta = string.Empty;

            OpenFileDialog fDialog = new OpenFileDialog();
            fDialog.Title = "Select Excel file";
            fDialog.Filter = "Excel Files|*.xlsx;*.xls;";
            fDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (fDialog.ShowDialog() == DialogResult.OK)
            {
                ruta = fDialog.FileName.ToString();
                string strConnXlsx = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ruta 
                    + ";Extended Properties=" + '"' + "Excel 12.0 Xml;HDR=YES;IMEX=1" + '"';
                string strConnXls = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ruta 
                    + ";Extended Properties=" + '"' + "Excel 8.0;HDR=YES;IMEX=1" + '"';
                string sqlExcel;
                DataTable dataTable = new DataTable();
                try
                {
                    using (OleDbConnection conn = new OleDbConnection(
                        ruta.Substring(ruta.Length - 4).ToLower().Equals("xlsx") ? 
                        strConnXlsx : strConnXls))
                    {
                        conn.Open();
                        var dtSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                        var Sheet1 = dtSchema.Rows[0].Field<string>("TABLE_NAME");
                        sqlExcel = "SELECT * FROM [" + Sheet1 + "]";
                        OleDbDataAdapter oleDbdataAdapter = new OleDbDataAdapter(sqlExcel, conn);
                        oleDbdataAdapter.Fill(dataTable);
                        dataGridView.DataSource = dataTable;
                        dataGridView.Columns["Id"].DisplayIndex = 0;
                        dataGridView.Columns["Name"].DisplayIndex = 1;
                        dataGridView.Columns["Team"].DisplayIndex = 2;
                        dataGridView.Columns["Country"].DisplayIndex = 3;
                        foreach (DataRow row in dataTable.Rows)
                        {
                            players.Add(
                                new Player(
                                    row[0].ToString(),
                                    row[1].ToString(),
                                    row[2].ToString(),
                                    row[3].ToString()));
                        }
                        numPlayers = players.Count;
                    }

                    lblPlayers.Text = "Players: " + numPlayers;
                    if (players.Count % 4 != 0)
                    {
                        MessageBox.Show("The number of players must be a multiple of 4.\nCheck the Excel.");
                    }
                    else
                    {
                        numTables = numPlayers / 4;
                        lblTables.Text = "Tables: " + numTables;
                        btnCalculate.Enabled = true;
                        numUpDownRounds.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void numUpDownRounds_ValueChanged(object sender, EventArgs e)
        {
            numRounds = Decimal.ToInt32(numUpDownRounds.Value);
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            numUpDownRounds.Enabled = false;
            btnExportar.Enabled = false;
            tables.Clear();

            for(currentRound = 1; currentRound <= numRounds; currentRound++)
            {
                playersNoUsadosEstaRonda = players.Select(item => (Player)item.Clone()).ToList();

                for (currentTable = 1; currentTable <= numTables; currentTable++)
                {
                    int p1 = 0, p2 = 0, p3 = 0, p4 = 0;
                    p1 = getRandomPlayer1();
                    p2 = getRandomPlayer2(p1);
                    p3 = getRandomPlayer3(p1, p2);
                    p4 = getRandomPlayer4(p1, p2, p3);
                    tables.Add(new Table(currentRound, currentTable, p1, p2, p3, p4));
                }
            }
            tablesWithNamesOnly = new List<TableWithNamesOnly>();
            foreach (Table t in tables)
            {
                tablesWithNamesOnly.Add(new TableWithNamesOnly(
                    t.roundId,
                    t.tableId,
                    getNameById(t.player1Id),
                    getNameById(t.player2Id),
                    getNameById(t.player3Id),
                    getNameById(t.player4Id)));
            }

            DataTable dataTable = new DataTable();
            using (var reader = ObjectReader.Create(tablesWithNamesOnly))
            {
                try
                {
                    dataTable.Load(reader);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            dataGridView.DataSource = dataTable;
            dataGridView.Columns["RoundId"].DisplayIndex = 0;
            dataGridView.Columns["TableId"].DisplayIndex = 1;
            numUpDownRounds.Enabled = true;
            btnExportar.Enabled = true;
        }

        private void btnExportar_Click(object sender, EventArgs e)
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

            DataTable dataTable = new DataTable();
            using (var reader = ObjectReader.Create(tablesWithAll))
            {
                try
                {
                    dataTable.Load(reader);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            dataGridView.DataSource = dataTable;
            dataGridView.Columns["RoundId"].DisplayIndex = 0;
            dataGridView.Columns["TableId"].DisplayIndex = 1;
            dataGridView.Columns["Player1Id"].DisplayIndex = 2;
            dataGridView.Columns["Player2Id"].DisplayIndex = 3;
            dataGridView.Columns["Player3Id"].DisplayIndex = 4;
            dataGridView.Columns["Player4Id"].DisplayIndex = 5;
            dataGridView.Columns["Player1Name"].DisplayIndex = 6;
            dataGridView.Columns["Player2Name"].DisplayIndex = 7;
            dataGridView.Columns["Player3Name"].DisplayIndex = 8;
            dataGridView.Columns["Player4Name"].DisplayIndex = 9;
            dataGridView.Columns["Player1Team"].DisplayIndex = 10;
            dataGridView.Columns["Player2Team"].DisplayIndex = 11;
            dataGridView.Columns["Player3Team"].DisplayIndex = 12;
            dataGridView.Columns["Player4Team"].DisplayIndex = 13;
            dataGridView.Columns["Player1Country"].DisplayIndex = 14;
            dataGridView.Columns["Player2Country"].DisplayIndex = 15;
            dataGridView.Columns["Player3Country"].DisplayIndex = 16;
            dataGridView.Columns["Player4Country"].DisplayIndex = 17;

            ExportToExcel();
        }

        #endregion

        #region Private methods

        private int getRandomPlayer1()
        {
            int r = random.Next(playersNoUsadosEstaRonda.Count);
            Player playerNoUsado = playersNoUsadosEstaRonda[r];
            playersNoUsadosEstaRonda.RemoveAt(r);
            return playerNoUsado.id;
        }

        private int getRandomPlayer2(int p1)
        {
            List<Player> pNoUsadosYQueNoHanJugadoYaConEste = playersNoUsadosYQueNoHanJugadoYaConEste(p1);
            int r = random.Next(pNoUsadosYQueNoHanJugadoYaConEste.Count);
            Player playerNoUsado = pNoUsadosYQueNoHanJugadoYaConEste[r];
            while (playerNoUsado.team.Equals(players[p1 - 1].team))
            {
                r = random.Next(playersNoUsadosEstaRonda.Count);
                playerNoUsado = playersNoUsadosEstaRonda[r];
            }
            playersNoUsadosEstaRonda.RemoveAt(r);
            return playerNoUsado.id;
        }

        private int getRandomPlayer3(int p1, int p2)
        {
            List<Player> pNoUsadosYQueNoHanJugadoYaConEste = playersNoUsadosYQueNoHanJugadoYaConEste(p1);
            pNoUsadosYQueNoHanJugadoYaConEste = playersNoUsadosYQueNoHanJugadoYaConEste(p2);
            int r = random.Next(pNoUsadosYQueNoHanJugadoYaConEste.Count);
            Player playerNoUsado = pNoUsadosYQueNoHanJugadoYaConEste[r];
            while (playerNoUsado.team.Equals(players[p1 - 1].team) &&
                playerNoUsado.team.Equals(players[p2 - 1].team))
            {
                r = random.Next(playersNoUsadosEstaRonda.Count);
                playerNoUsado = playersNoUsadosEstaRonda[r];
            }
            return playerNoUsado.id;
        }

        private int getRandomPlayer4(int p1, int p2, int p3)
        {
            List<Player> pNoUsadosYQueNoHanJugadoYaConEste = playersNoUsadosYQueNoHanJugadoYaConEste(p1);
            pNoUsadosYQueNoHanJugadoYaConEste = playersNoUsadosYQueNoHanJugadoYaConEste(p2);
            pNoUsadosYQueNoHanJugadoYaConEste = playersNoUsadosYQueNoHanJugadoYaConEste(p3);
            int r = random.Next(pNoUsadosYQueNoHanJugadoYaConEste.Count);
            Player playerNoUsado = pNoUsadosYQueNoHanJugadoYaConEste[r];
            while (playerNoUsado.team.Equals(players[p1 - 1].team) &&
                playerNoUsado.team.Equals(players[p2 - 1].team) &&
                playerNoUsado.team.Equals(players[p3 - 1].team))
            {
                r = random.Next(playersNoUsadosEstaRonda.Count);
                playerNoUsado = playersNoUsadosEstaRonda[r];
            }
            return playerNoUsado.id;
        }

        private List<Player> playersNoUsadosYQueNoHanJugadoYaConEste(int pId)
        {
            List<Player> playersNoUsadosYQueNoHanJugadoYaConEstos = playersNoUsadosEstaRonda.Select(item => (Player)item.Clone()).ToList();
            List<Table> mesasDondeJugo = (from t in tables
                                         where t.roundId < currentRound
                                         where pId == t.player1Id || pId == t.player2Id || pId == t.player3Id || pId == t.player4Id
                                         select t).ToList();
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
                Player pAux = getPlayerById(id);
                if(pAux != null && playersNoUsadosYQueNoHanJugadoYaConEstos.Contains(pAux))
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

        private string getNameById(int pId)
        {
            return getPlayerById(pId).name;
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

        public void ExportToExcel()
        {
            NsExcel.Application excel;
            NsExcel.Workbook excelworkBook;
            NsExcel.Worksheet excelSheet;
            NsExcel.Range excelCellrange;
            DataTable dataTable = ConvertToDataTable<TableWithAll>(tablesWithAll);

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
            excelSheet.Cells[1, 1] = "Sample test data";
            excelSheet.Cells[1, 2] = "Date : " + DateTime.Now.ToShortDateString();


            //// now we resize the columns
            //excelCellrange = excelSheet.Range[excelSheet.Cells[1, 1], excelSheet.Cells[tablesWithAll.Count, dataTable.Columns.Count]];
            //excelCellrange.EntireColumn.AutoFit();

            //NsExcel.Borders border = excelCellrange.Borders;
            //border.LineStyle = NsExcel.XlLineStyle.xlContinuous;
            //border.Weight = 2d;

            ////Cabecera
            //FormattingExcelCells(excelSheet.Range["A1"].EntireRow, "#20AA20", Color.PaleVioletRed, true);

            ////Resto
            //FormattingExcelCells(excelSheet.Range[excelSheet.Cells[2, 1], excelSheet.Cells[tablesWithAll.Count, dataTable.Columns.Count]], "#20AA20", Color.PaleVioletRed, true);
        }

        public void FormattingExcelCells(NsExcel.Range range, string HTMLcolorCode, System.Drawing.Color fontColor, bool IsFontbold)
        {
            range.Interior.Color = System.Drawing.ColorTranslator.FromHtml(HTMLcolorCode);
            range.Font.Color = System.Drawing.ColorTranslator.ToOle(fontColor);
            range.Font.Bold = IsFontbold;
        }

        #endregion
    }

    #region Auxiliar classes

    class Player
    {
        public int id;
        public string name;
        public string country;
        public string team;

        public Player(string id, string name, string country, string team)
        {
            this.id = int.Parse(id);
            this.name = name;
            this.country = country;
            this.team = team;
        }

        internal Player Clone()
        {
            return new Player(id.ToString(), name, country, team);
        }
    }

    class Table
    {
        public int roundId;
        public int tableId;
        public int player1Id;
        public int player2Id;
        public int player3Id;
        public int player4Id;

        public Table(int roundId, int tableId, 
            int player1Id, int player2Id, int player3Id, int player4Id)
        {
            this.roundId = roundId;
            this.tableId = tableId;
            this.player1Id = player1Id;
            this.player2Id = player2Id;
            this.player3Id = player3Id;
            this.player4Id = player4Id;
        }
    }

    class TableWithAll : Table
    {
        public string player1Name;
        public string player2Name;
        public string player3Name;
        public string player4Name;
        public string player1Country;
        public string player2Country;
        public string player3Country;
        public string player4Country;
        public string player1Team;
        public string player2Team;
        public string player3Team;
        public string player4Team;

        public TableWithAll(int roundId, int tableId,
            int player1Id, int player2Id, int player3Id, int player4Id,
            string player1Name, string player2Name, string player3Name,
            string player4Name, string player1Country, string player2Country, 
            string player3Country, string player4Country, string player1Team, 
            string player2Team, string player3Team, string player4Team) 
            : base(roundId, tableId, player1Id, player2Id, player3Id, 
            player4Id)
        {
            this.player1Name = player1Name;
            this.player2Name = player2Name;
            this.player3Name = player3Name;
            this.player4Name = player4Name;
            this.player1Country = player1Country;
            this.player2Country = player2Country;
            this.player3Country = player3Country;
            this.player4Country = player4Country;
            this.player1Team = player1Team;
            this.player2Team = player2Team;
            this.player3Team = player3Team;
            this.player4Team = player4Team;
        }
    }

    class TableWithNamesOnly
    {
        public int roundId;
        public int tableId;
        public string player1Name;
        public string player2Name;
        public string player3Name;
        public string player4Name;

        public TableWithNamesOnly(int roundId, int tableId,
            string player1Name, string player2Name, string player3Name,
            string player4Name)
        {
            this.roundId = roundId;
            this.tableId = tableId;
            this.player1Name = player1Name;
            this.player2Name = player2Name;
            this.player3Name = player3Name;
            this.player4Name = player4Name;
        }

        public TableWithNamesOnly()
        {
        }
    }

    #endregion
}
