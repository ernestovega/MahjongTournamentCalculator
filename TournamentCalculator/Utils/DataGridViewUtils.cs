using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace TournamentCalculator.Utils
{
    public static class DataGridViewUtils
    {

        public static void updateDataGridViewPlayer(DataGridView dataGrid, List<string[]> sPlayers)
        {
            dataGrid.DataSource = ConvertListToDataTable(sPlayers);
            dataGrid.Columns[0].HeaderText = "Id";
            dataGrid.Columns[1].HeaderText = "Name";
            dataGrid.Columns[2].HeaderText = "Team";
            dataGrid.Columns[3].HeaderText = "Country";
            dataGrid.Columns[0].Width = 80;
            dataGrid.Columns[1].Width = 240;
            dataGrid.Columns[2].Width = 240;
            dataGrid.Columns[3].Width = 240;
        }
        
        public static void updateDataGridViewTable(DataGridView dataGrid, List<string[]> sTables, string header)
        {
            dataGrid.DataSource = ConvertListToDataTable(sTables);
            dataGrid.Columns[0].HeaderText = "Round";
            dataGrid.Columns[1].HeaderText = "Table";
            dataGrid.Columns[2].HeaderText = "Player 1 " + header;
            dataGrid.Columns[3].HeaderText = "Player 2 " + header;
            dataGrid.Columns[4].HeaderText = "Player 3 " + header;
            dataGrid.Columns[5].HeaderText = "Player 4 " + header;
            dataGrid.Columns[0].Width = 80;
            dataGrid.Columns[1].Width = 80;
            dataGrid.Columns[2].Width = 160;
            dataGrid.Columns[3].Width = 160;
            dataGrid.Columns[4].Width = 160;
            dataGrid.Columns[5].Width = 160;
        }

        private static DataTable ConvertListToDataTable(List<string[]> list)
        {
            // New table.
            DataTable table = new DataTable();

            // Get max columns.
            int columns = 0;
            foreach (var array in list)
            {
                if (array.Length > columns)
                {
                    columns = array.Length;
                }
            }

            // Add columns.
            for (int i = 0; i < columns; i++)
            {
                table.Columns.Add();
            }

            // Add rows.
            foreach (var array in list)
            {
                table.Rows.Add(array);
            }

            return table;
        }
    }
}
