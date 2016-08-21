namespace TournamentCalculator
{
    public class TableWithCountries
    {
        public int roundId;
        public int tableId;
        public string player1Country;
        public string player2Country;
        public string player3Country;
        public string player4Country;

        public TableWithCountries(int roundId, int tableId,
            string player1Country, string player2Country, string player3Country, string player4Country)
        {
            this.roundId = roundId;
            this.tableId = tableId;
            this.player1Country = player1Country;
            this.player2Country = player2Country;
            this.player3Country = player3Country;
            this.player4Country = player4Country;
        }

        public TableWithCountries()
        {

        }
    }
}