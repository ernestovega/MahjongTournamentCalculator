using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentCalculator.Model
{
    class Rivals
    {
        public string playerName;
        public string[] rivalsNames;

        public Rivals (string playerName, string[] rivalsNames)
        {
            this.playerName = playerName;
            this.rivalsNames = rivalsNames;
        }

        public Rivals()
        {

        }
    }
}
