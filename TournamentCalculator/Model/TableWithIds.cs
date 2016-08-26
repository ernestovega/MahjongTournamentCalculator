namespace TournamentCalculator
{
    public class TableWithIds
    {
        public int roundId;
        public int tableId;
        public int player1Id;
        public int player2Id;
        public int player3Id;
        public int player4Id;

        public TableWithIds(int roundId, int tableId,
            int player1Id, int player2Id, int player3Id, int player4Id)
        {
            this.roundId = roundId;
            this.tableId = tableId;
            this.player1Id = player1Id;
            this.player2Id = player2Id;
            this.player3Id = player3Id;
            this.player4Id = player4Id;
        }

        public TableWithIds()
        {

        }
    }
}