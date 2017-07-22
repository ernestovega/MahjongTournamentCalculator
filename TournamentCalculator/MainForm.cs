using MahjongTournamentCalculator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using TournamentCalculator.Model;
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
        private int numTriesMax, result, numPlayers, numRounds, countTries;
        private Random random = new Random();        
        private string errorMessage = string.Empty;
        private string creationDate = string.Empty;
        private Thread calculateTournamentThread = null;

        #endregion

        public MainForm()
        {
            InitializeComponent();

            if (!ExcelUtils.isExcelInstalled())
            {
                MessageBox.Show(this, "Excel not present on your computer.\nPlease get it.");
                Application.Exit();
            }

            InitializeWorker();
        }

        #region Events

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy == true)
            {
                backgroundWorker1.CancelAsync();
                MakeViewsEnabled();
            }
            else
            {
                InitializeCalculation();
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy == true)
            {
                backgroundWorker1.CancelAsync();
            }
            Close();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        #endregion

        #region BackgroundWorker

        private void InitializeWorker()
        {
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            GeneratePlayers();

            creationDate = string.Format("{0}{1}{2}_{3}{4}{5}", DateTime.Now.Year, DateTime.Now.Month,
                DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            //Cada vez que un cálculo es imposible se reintenta desde cero, tantas veces como se hayan indicado.
            result = -1;
            countTries = 0;
            while (result < 0 && countTries < numTriesMax)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                countTries++;
                worker.ReportProgress(countTries, null);
                result = GenerateTournament(numRounds);
                Application.DoEvents();
            }

            /*Si no se ha podido calcular en los intentos indicados, se notifica,
              se muestra la lista de jugadores y se termina*/
            if (countTries >= numTriesMax)
            {
                MessageBox.Show(this, "Can't calculate tournament after " + numTriesMax + " tries.\nIf you want to try again, select more tries and calculate again.");
                Cursor.Current = Cursors.Default;
                worker.CancelAsync();
            }

            worker.ReportProgress(countTries, null);

            //Generamos todas las vistas y se mostramos las mesas
            GenerateTablesWhitAll(numRounds);
            GenerateSTablesWithNames();
            GenerateSTablesWithIds();
            GenerateTablesByPlayer();
            GenerateRivalsByPlayer();

            if (worker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            ExportTournament(worker, e);

            if (worker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            ExportScoreTables(worker, e);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MakeViewsEnabled();
            SystemSounds.Exclamation.Play();
        }

        #endregion

        #region Excel methods

        private static bool RequestPlayersFile(ref string path)
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

            path = string.Empty;
            return false;
        }

        private void ExportTournament(BackgroundWorker worker, System.ComponentModel.DoWorkEventArgs e)
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
                    if (worker.CancellationPending)
                    {
                        break;
                    }

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
                    newSheet.Cells[1, 3] = "Player1Id";
                    newSheet.Cells[1, 4] = "Player2Id";
                    newSheet.Cells[1, 5] = "Player3Id";
                    newSheet.Cells[1, 6] = "Player4Id"; ;
                    newSheet.Cells[1, 7] = "Player1Team";
                    newSheet.Cells[1, 8] = "Player2Team";
                    newSheet.Cells[1, 9] = "Player3Team";
                    newSheet.Cells[1, 10] = "Player4Team";

                    //Write data
                    var currentRoundTables = tablesWithAll.FindAll(x => x.roundId == currentRound).ToList();

                    for (currentTable = 1; currentTable <= tablesWithAll.Select(x => x.tableId).Distinct().Count(); currentTable++)
                    {
                        newSheet.Cells[currentTable + 1, 1] = currentRoundTables[currentTable - 1].roundId;
                        newSheet.Cells[currentTable + 1, 2] = currentRoundTables[currentTable - 1].tableId;
                        newSheet.Cells[currentTable + 1, 3] = currentRoundTables[currentTable - 1].player1Id;
                        newSheet.Cells[currentTable + 1, 4] = currentRoundTables[currentTable - 1].player2Id;
                        newSheet.Cells[currentTable + 1, 5] = currentRoundTables[currentTable - 1].player3Id;
                        newSheet.Cells[currentTable + 1, 6] = currentRoundTables[currentTable - 1].player4Id;
                        newSheet.Cells[currentTable + 1, 7] = currentRoundTables[currentTable - 1].player1Team;
                        newSheet.Cells[currentTable + 1, 8] = currentRoundTables[currentTable - 1].player2Team;
                        newSheet.Cells[currentTable + 1, 9] = currentRoundTables[currentTable - 1].player3Team;
                        newSheet.Cells[currentTable + 1, 10] = currentRoundTables[currentTable - 1].player4Team;
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

                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                //Write Tournament data by players
                WriteToExcelTablesByPlayers(excelSheets, worker, e);

                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                //Write players rivals by player
                WriteToExcelRivals(excelSheets, worker, e);

                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                //Now save the excel
                string excelName = "Tournament_" + creationDate;
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
                    MessageBox.Show(this, "Tournament Excel file couldn't be saved.");
                    return;
                }
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }
        }

        private void ExportScoreTables(BackgroundWorker worker, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                #region Create WorkBook

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

                #endregion

                #region First Sheet
                
                //Adding new Worksheet and deleting existing
                var newSheet = (NsExcel.Worksheet)excelSheets.Add(Type.Missing,
                    excelSheets[excelSheets.Count], Type.Missing, Type.Missing);
                newSheet.Name = "PlayersAndTeams";
                while (excelSheets.Count > 1)
                {
                    excelSheets[excelSheets.Count - 1].Delete();
                }

                //Write Ids, blank names and blank teams sheet, for reference in the other sheets
                //Write headers
                newSheet.Cells[1, 1] = "Id";
                newSheet.Cells[1, 2] = "Name";
                newSheet.Cells[1, 3] = "Team";

                //Write Ids
                for (int i = 1; i < players.Count; i++)
                {
                    newSheet.Cells[i + 1, 1] = i;
                }

                //Resize columns
                newSheet.Cells[1, 1].ColumnWidth = 6;
                newSheet.Cells[1, 2].ColumnWidth = 32;
                newSheet.Cells[1, 3].ColumnWidth = 32;

                //Paint headers
                newSheet.UsedRange.Rows[1].Cells.Interior.Color = ColorTranslator.ToOle(Color.FromArgb(0, 177, 106));
                newSheet.UsedRange.Rows[1].Cells.Font.Color = ColorTranslator.ToOle(Color.White);
                newSheet.UsedRange.Rows[1].Cells.Font.Bold = true;

                #endregion

                #region Rounds Sheets

                //Write Tournament data by rounds, with names and teams feeded from 1st sheet
                for (currentRound = 1;
                    currentRound <= tablesWithAll.Select(x => x.roundId).Distinct().Count();
                    currentRound++)
                {
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    //Adding new Worksheet and deleting existing
                    newSheet = (NsExcel.Worksheet)excelSheets.Add(Type.Missing,
                        excelSheets[excelSheets.Count], Type.Missing, Type.Missing);
                    newSheet.Name = string.Format("Round{0}", currentRound);

                    //Write headers
                    newSheet.Cells[1, 1] = "Round";
                    newSheet.Cells[1, 2] = "Id";
                    newSheet.Cells[1, 3] = "Name";
                    newSheet.Cells[1, 4] = "Points";
                    newSheet.Cells[1, 5] = "Score";
                    newSheet.Cells[1, 6] = "Team";

                    //Write data
                    var currentRoundTables = tablesWithAll.FindAll(x => x.roundId == currentRound).ToList();
                    
                    for (int i = 1; i < players.Count; i++)
                    {
                        int fila = i + 1;

                        newSheet.Cells[fila, 1] = currentRound;
                        newSheet.Cells[fila, 2] = i;
                        
                        newSheet.Cells[fila, 3].Formula = string.Format("=PlayersAndTeams!B{0}", fila);
                        newSheet.Cells[fila, 6].Formula = string.Format("=PlayersAndTeams!C{0}", fila);
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
                }

                #endregion

                #region Ranking Sheet

                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                //Adding new Worksheet
                newSheet = (NsExcel.Worksheet)excelSheets.Add(Type.Missing,
                    excelSheets[excelSheets.Count], Type.Missing, Type.Missing);
                newSheet.Name = "Ranking";

                //Write Ids, blank names and blank teams sheet, for reference in the other sheets
                //Write headers
                newSheet.Cells[1, 1] = "Position";
                newSheet.Cells[1, 2] = "Name";
                newSheet.Cells[1, 3] = "Points";
                newSheet.Cells[1, 4] = "Score";

                //Write Ids
                for (int i = 1; i < players.Count; i++)
                {
                    newSheet.Cells[i + 1, 1] = i;
                }

                //Resize columns
                newSheet.Cells[1, 1].ColumnWidth = 9;
                newSheet.Cells[1, 2].ColumnWidth = 32;
                newSheet.Cells[1, 3].ColumnWidth = 9;
                newSheet.Cells[1, 4].ColumnWidth = 9;

                //Write data

                for (int i = 1; i < players.Count; i++)
                {
                    int fila = i + 1;

                    newSheet.Cells[fila, 1] = i;
                    newSheet.Cells[fila, 2].Formula = string.Format("=PlayersAndTeams!B{0}", fila);
                    string formulaPuntos = string.Format("=SUM(Round{0}!D{1}", 1, fila);
                    string formulaScore = string.Format("=SUM(Round{0}!E{1}", 1, fila);
                    for (int j = 2; j <= numRounds; j++) {
                        formulaPuntos = string.Format("{0},Round{1}!D{2}", formulaPuntos, j, fila);
                        formulaScore = string.Format("{0},Round{1}!E{2}", formulaScore, j, fila);
                    }
                    formulaPuntos = string.Format("{0})", formulaPuntos);
                    formulaScore = string.Format("{0})", formulaScore);
                    newSheet.Cells[fila, 3].Formula = formulaPuntos;
                    newSheet.Cells[fila, 4].Formula = formulaScore;
                }

                //Paint headers
                newSheet.UsedRange.Rows[1].Cells.Interior.Color = ColorTranslator.ToOle(Color.FromArgb(0, 177, 106));
                newSheet.UsedRange.Rows[1].Cells.Font.Color = ColorTranslator.ToOle(Color.White);
                newSheet.UsedRange.Rows[1].Cells.Font.Bold = true;

                #endregion

                #region Save Workbook

                //Saving file
                string excelName = "Score_Tables_" + creationDate;
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
                    MessageBox.Show(this, "Excel file couldn't be saved.");
                    return;
                }

                #endregion
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, exception.Message);
                return;
            }
        }

        private void WriteToExcelTablesByPlayers(NsExcel.Sheets excelSheets, 
            BackgroundWorker worker, System.ComponentModel.DoWorkEventArgs e)
        {
            if (worker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            //Adding new Worksheet
            var newSheet = (NsExcel.Worksheet)excelSheets.Add(Type.Missing, 
                excelSheets[excelSheets.Count], Type.Missing, Type.Missing);
            newSheet.Name = "Player's tables";
           
            //Write headers
            newSheet.Cells[1, 1] = "Round";
            newSheet.Cells[1, 2] = "Table";
            newSheet.Cells[1, 3] = "Player 1 Id";
            newSheet.Cells[1, 4] = "Player 2 Id";
            newSheet.Cells[1, 5] = "Player 3 Id";
            newSheet.Cells[1, 6] = "Player 4 Id";
            newSheet.Cells[1, 7] = "Player 1 Team";
            newSheet.Cells[1, 8] = "Player 2 Team";
            newSheet.Cells[1, 9] = "Player 3 Team";
            newSheet.Cells[1, 10] = "Player 4 Team";

            //Write data

            for (int i = 0; i < tablesByPlayer.Count; i++)
            {
                TableWithAll twa = tablesByPlayer[i];

                newSheet.Cells[i + 2, 1] = twa.roundId;
                newSheet.Cells[i + 2, 2] = twa.tableId;
                newSheet.Cells[i + 2, 3] = twa.player1Id;
                newSheet.Cells[i + 2, 4] = twa.player2Id;
                newSheet.Cells[i + 2, 5] = twa.player3Id;
                newSheet.Cells[i + 2, 6] = twa.player4Id;
                newSheet.Cells[i + 2, 7] = twa.player1Team;
                newSheet.Cells[i + 2, 8] = twa.player2Team;
                newSheet.Cells[i + 2, 9] = twa.player3Team;
                newSheet.Cells[i + 2, 10] = twa.player4Team;
            }

            //Paint headers
            newSheet.UsedRange.Rows[1].Cells.Interior.Color = ColorTranslator.ToOle(Color.FromArgb(0, 177, 106));
            newSheet.UsedRange.Rows[1].Cells.Font.Color = ColorTranslator.ToOle(Color.White);
            newSheet.UsedRange.Rows[1].Cells.Font.Bold = true;

            //Paint odd lines
            for (int i = 1; i <= newSheet.UsedRange.Rows.Count; i++)
            {
                if (i > 1 && i % 2 != 0)
                {
                    newSheet.UsedRange.Rows[i].Cells.Interior.Color = ColorTranslator.ToOle(Color.FromArgb(224, 224, 224));
                }
            }

            //Resize columns
            newSheet.UsedRange.EntireColumn.AutoFit();
        }

        private void WriteToExcelRivals(NsExcel.Sheets excelSheets,
            BackgroundWorker worker, System.ComponentModel.DoWorkEventArgs e)
        {
            if (worker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            //Adding new Worksheet
            var newSheet = (NsExcel.Worksheet)excelSheets.Add(Type.Missing, 
                excelSheets[excelSheets.Count], Type.Missing, Type.Missing);
            newSheet.Name = "Player's rivals";

            //Write headers
            newSheet.Cells[1, 1] = "Player Id";
            int maxRivals = 0;
            foreach (Rivals r in rivalsByPlayer)
            {
                if (r.rivalsNames.Count() > maxRivals)
                    maxRivals = r.rivalsNames.Count();
            }
            for (int i = 1; i <= maxRivals; i++)
            {
                newSheet.Cells[1, i + 1] = "Rival " + i + " Id";
            }

            if (worker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            //Write data
            for (int i = 0; i < rivalsByPlayer.Count; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

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

        #region Calculation methods

        private void InitializeCalculation()
        {
            numPlayers = decimal.ToInt32(numUpDownPlayers.Value);
            numRounds = decimal.ToInt32(numUpDownRounds.Value);
            numTriesMax = decimal.ToInt32(numUpDownTriesMax.Value);
            progressBar.Maximum = numTriesMax;
            progressBar.Value = 0;
            MakeViewsDisabled();
        }

        private void GeneratePlayers()
        {
            for (int i = 1; i <= numPlayers / 4; i++)//Recorremos cada equipo
            {
                int tableId = (4 * i) / 4;//Generamos el id del equipo
                for (int j = 1; j <= 4; j++)//Recorremos cada jugador del equipo
                {
                    //Generamos el id del jugador
                    int playerId = (4 * i) - (4 - j);
                    //Creamos el jugador
                    Player player = new Player(playerId.ToString(),  playerId.ToString(), tableId.ToString());
                    //Añadimos el jugador
                    players.Add(player);
                }
            }
        }

        public delegate void UpdateProgressBarDelegate();

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
                            foreach (TablePlayer tp in currentTableTablePlayers)
                                currentTablePlayers.Add(GetPlayerById(tp.playerId));

                            //Elegimos un jugador al azar y lo quitamos de la lista de no descartados
                            int r = random.Next(0, arrayPlayersIdsNotDiscarded.Count());
                            Player choosenOne = GetPlayerById(arrayPlayersIdsNotDiscarded[r]);
                            playersIdsNotDiscarded.Remove(choosenOne.id);

                            //Obtenemos la lista de jugadores que han jugado en anteriores rondas contra el elegido
                            List<int> playersWHPATCO = GetPlayersWhoHavePlayedAgainstTheChoosenOne(choosenOne);
                            bool anyoneHavePlayed = false;
                            foreach (int ctp in currentTablePlayers.Select(x => x.id))
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
                        if (!playerFounded && playersIdsNotDiscarded.Count == 0)
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

        private bool FindDuplicates()
        {
            foreach (Player p in players)
            {
                string[] pRivals = rivalsByPlayer.Find(x => x.playerName.Equals(p.name)).rivalsNames;
                var dict = new Dictionary<string, int>();
                if (pRivals.Distinct().Count() != numRounds * 3)
                {
                    return true;
                }
            }
            return false;
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
                                tableWithAll.player1Id = player.id;
                                break;
                            case 2:
                                int player2Id = tablePlayers.Find(x => x.round == currentRound &&
                                x.table == currentTable && x.player == currentTablePlayer).playerId;
                                Player player2 = players.Find(x => x.id == player2Id);
                                tableWithAll.player2Name = player2.name;
                                tableWithAll.player2Team = player2.team;
                                tableWithAll.player2Id = player2.id;
                                break;
                            case 3:
                                int player3Id = tablePlayers.Find(x => x.round == currentRound &&
                                x.table == currentTable && x.player == currentTablePlayer).playerId;
                                Player player3 = players.Find(x => x.id == player3Id);
                                tableWithAll.player3Name = player3.name;
                                tableWithAll.player3Team = player3.team;
                                tableWithAll.player3Id = player3.id;
                                break;
                            case 4:
                                int player4Id = tablePlayers.Find(x => x.round == currentRound &&
                                x.table == currentTable && x.player == currentTablePlayer).playerId;
                                Player player4 = players.Find(x => x.id == player4Id);
                                tableWithAll.player4Name = player4.name;
                                tableWithAll.player4Team = player4.team;
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

        private void GenerateSPlayers()
        {
            foreach (Player p in players)
            {
                sPlayers.Add(new string[] { p.id.ToString(), p.name, p.team });
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

        #region Views methods

        private void MakeViewsEnabled()
        {
            HideProgressBar();
            numUpDownPlayers.Enabled = true;
            numUpDownRounds.Enabled = true;
            numUpDownTriesMax.Enabled = true;
            btnCalculate.BackColor = Color.FromArgb(0, 177, 106);
            btnCalculate.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 127, 56);
            btnCalculate.Text = "Go";
            Cursor.Current = Cursors.Default;
        }

        private void MakeViewsDisabled()
        {
            numUpDownPlayers.Enabled = false;
            numUpDownRounds.Enabled = false;
            numUpDownTriesMax.Enabled = false;
            btnCalculate.Text = "Stop";
            btnCalculate.BackColor = Color.FromArgb(65, 65, 65);
            btnCalculate.FlatAppearance.MouseOverBackColor = Color.FromArgb(224, 0, 0);
            Cursor.Current = Cursors.WaitCursor;
            ShowProgressBar();
        }

        private void ShowProgressBar()
        {
            progressBar.Visible = true;
            progressBar.Show();
        }

        private void HideProgressBar()
        {
            progressBar.Hide();//TODO: ¿Es necesario?
            progressBar.Visible = false;
        }

        #endregion
    }
}