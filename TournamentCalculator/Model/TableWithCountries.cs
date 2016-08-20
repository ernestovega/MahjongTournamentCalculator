namespace TournamentCalculator
{
    public class TableWithCountries
    {
        public int roundId;
        public int tableId;
        public int player1Country;
        public int player2Country;
        public int player3Country;
        public int player4Country;

        public TableWithCountries(int roundId, int tableId,
            int player1Country, int player2Country, int player3Country, int player4Country)
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