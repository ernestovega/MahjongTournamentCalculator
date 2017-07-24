using System;
using System.Windows.Forms;

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
