using FastMember;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace TournamentCalculator.Utils
{
    public static class DataGridViewUtils
    {
        public static void updateDataGridView(DataGridView datagrid, DataTable dataTable)
        {
            datagrid.DataSource = dataTable;
        }

        public static void updateDataGridView(DataGridView dataGridView, List<Player> players)
        {
            DataTable dataTable = new DataTable();
            using (var reader = ObjectReader.Create(players))
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

        public static void updateDataGridView(DataGridView datagrid, List<Table> list)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (var reader = ObjectReader.Create(list))
                {
                    dataTable.Load(reader);
                    datagrid.DataSource = dataTable;
                    datagrid.Columns["RoundId"].DisplayIndex = 0;
                    datagrid.Columns["TableId"].DisplayIndex = 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void updateDataGridView(DataGridView datagrid, List<TableWithNamesOnly> list)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (var reader = ObjectReader.Create(list))
                {
                    dataTable.Load(reader);
                    datagrid.DataSource = dataTable;
                    datagrid.Columns["RoundId"].DisplayIndex = 0;
                    datagrid.Columns["TableId"].DisplayIndex = 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void updateDataGridView(DataGridView datagrid, List<TableWithTeamsOnly> list)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (var reader = ObjectReader.Create(list))
                {
                    dataTable.Load(reader);
                    datagrid.DataSource = dataTable;
                    datagrid.Columns["RoundId"].DisplayIndex = 0;
                    datagrid.Columns["TableId"].DisplayIndex = 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void updateDataGridView(DataGridView datagrid, List<TableWithCountriesOnly> list)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (var reader = ObjectReader.Create(list))
                {
                    dataTable.Load(reader);
                    datagrid.DataSource = dataTable;
                    datagrid.Columns["RoundId"].DisplayIndex = 0;
                    datagrid.Columns["TableId"].DisplayIndex = 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void updateDataGridView(DataGridView datagrid, List<TableWithAll> list)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (var reader = ObjectReader.Create(list))
                {
                    dataTable.Load(reader);
                    datagrid.DataSource = dataTable;
                    datagrid.Columns["RoundId"].DisplayIndex = 0;
                    datagrid.Columns["TableId"].DisplayIndex = 1;
                    datagrid.Columns["Player1Id"].DisplayIndex = 2;
                    datagrid.Columns["Player2Id"].DisplayIndex = 3;
                    datagrid.Columns["Player3Id"].DisplayIndex = 4;
                    datagrid.Columns["Player4Id"].DisplayIndex = 5;
                    datagrid.Columns["Player1Name"].DisplayIndex = 6;
                    datagrid.Columns["Player2Name"].DisplayIndex = 7;
                    datagrid.Columns["Player3Name"].DisplayIndex = 8;
                    datagrid.Columns["Player4Name"].DisplayIndex = 9;
                    datagrid.Columns["Player1Team"].DisplayIndex = 10;
                    datagrid.Columns["Player2Team"].DisplayIndex = 11;
                    datagrid.Columns["Player3Team"].DisplayIndex = 12;
                    datagrid.Columns["Player4Team"].DisplayIndex = 13;
                    datagrid.Columns["Player1Country"].DisplayIndex = 14;
                    datagrid.Columns["Player2Country"].DisplayIndex = 15;
                    datagrid.Columns["Player3Country"].DisplayIndex = 16;
                    datagrid.Columns["Player4Country"].DisplayIndex = 17;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
