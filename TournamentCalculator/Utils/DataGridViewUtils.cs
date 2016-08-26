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

        public static void updateDataGridView(DataGridView dataGrid, List<Player> players)
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
            dataGrid.DataSource = dataTable;
            dataGrid.Columns["Id"].DisplayIndex = 0;
            dataGrid.Columns["Name"].DisplayIndex = 1;
            dataGrid.Columns["Team"].DisplayIndex = 2;
            dataGrid.Columns["Country"].DisplayIndex = 3;
            dataGrid.Columns["Id"].Width = 72;
        }
        
        public static void updateDataGridView(DataGridView datagrid, List<TableWithNames> list)
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
                    datagrid.Columns["RoundId"].Width = 72;
                    datagrid.Columns["TableId"].Width = 72;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void updateDataGridView(DataGridView datagrid, List<TableWithTeams> list)
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
                    datagrid.Columns["RoundId"].Width = 72;
                    datagrid.Columns["TableId"].Width = 72;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void updateDataGridView(DataGridView datagrid, List<TableWithCountries> list)
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
                    datagrid.Columns["RoundId"].Width = 72;
                    datagrid.Columns["TableId"].Width = 72;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void updateDataGridView(DataGridView datagrid, List<TableWithIds> list)
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
    }
}
