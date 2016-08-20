namespace TournamentCalculator
{
    public class TableWithNames
    {
        public int roundId;
        public int tableId;
        public int player1Name;
        public int player2Name;
        public int player3Name;
        public int player4Name;

        public TableWithNames(int roundId, int tableId,
            int player1Name, int player2Name, int player3Name, int player4Name)
        {
            this.roundId = roundId;
            this.tableId = tableId;
            this.player1Name = player1Name;
            this.player2Name = player2Name;
            this.player3Name = player3Name;
            this.player4Name = player4Name;
        }

        public TableWithNames()
        {

        }
    }
}