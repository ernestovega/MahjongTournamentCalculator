namespace TournamentCalculator
{
    public class TableWithAll
    {
        public int roundId;
        public int tableId;
        public string player1Name;
        public string player2Name;
        public string player3Name;
        public string player4Name;
        public string player1Team;
        public string player2Team;
        public string player3Team;
        public string player4Team;
        public string player1Country;
        public string player2Country;
        public string player3Country;
        public string player4Country;
        public int player1Id;
        public int player2Id;
        public int player3Id;
        public int player4Id;

        public TableWithAll(int roundId, int tableId,
            string player1Name, string player2Name, string player3Name, string player4Name,
            string player1Team, string player2Team, string player3Team, string player4Team,
            string player1Country, string player2Country, string player3Country, string player4Country,
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