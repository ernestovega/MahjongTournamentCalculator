namespace TournamentCalculator
{
    public class TableWithAll
    {
        public int roundId;
        public int tableId;
        public int player1Name;
        public int player2Name;
        public int player3Name;
        public int player4Name;
        public int player1Team;
        public int player2Team;
        public int player3Team;
        public int player4Team;
        public int player1Country;
        public int player2Country;
        public int player3Country;
        public int player4Country;
        public int player1Id;
        public int player2Id;
        public int player3Id;
        public int player4Id;

        public TableWithAll(int roundId, int tableId,
            int player1Name, int player2Name, int player3Name, int player4Name,
            int player1Team, int player2Team, int player3Team, int player4Team,
            int player1Country, int player2Country, int player3Country, int player4Country,
            int player1Id, int player2Id, int player3Id, int player4Id)
        {
            this.roundId = roundId;
            this.tableId = tableId;
            this.player1Name = player1Name;
            this.player2Name = player2Name;
            this.player3Name = player3Name;
            this.player4Name = player4Name;
            this.player1Team = player1Team;
            this.player2Team = player2Team;
            this.player3Team = player3Team;
            this.player4Team = player4Team;
            this.player1Country = player1Country;
            this.player2Country = player2Country;
            this.player3Country = player3Country;
            this.player4Country = player4Country;
            this.player1Id = player1Id;
            this.player2Id = player2Id;
            this.player3Id = player3Id;
            this.player4Id = player4Id;
        }

        public TableWithAll()
        {

        }
    }
}