using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TournamentCalculator;
using NsExcel = Microsoft.Office.Interop.Excel;

namespace MahjongTournamentCalculator
{
    class ExcelUtils
    {
        public static bool isExcelInstalled()
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


    }
}
