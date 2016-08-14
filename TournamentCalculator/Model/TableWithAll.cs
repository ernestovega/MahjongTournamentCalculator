namespace TournamentCalculator
{
    public class TableWithAll : Table
    {
        public string player1Name;
        public string player2Name;
        public string player3Name;
        public string player4Name;
        public string player1Country;
        public string player2Country;
        public string player3Country;
        public string player4Country;
        public string player1Team;
        public string player2Team;
        public string player3Team;
        public string player4Team;

        public TableWithAll(int roundId, int tableId,
            int player1Id, int player2Id, int player3Id, int player4Id,
            string player1Name, string player2Name, string player3Name,
            string player4Name, string player1Country, string player2Country,
            string player3Country, string player4Country, string player1Team,
            string player2Team, string player3Team, string player4Team)
            : base(roundId, tableId, player1Id, player2Id, player3Id,
            player4Id)
        {
            this.player1Name = player1Name;
            this.player2Name = player2Name;
            this.player3Name = player3Name;
            this.player4Name = player4Name;
            this.player1Country = player1Country;
            this.player2Country = player2Country;
            this.player3Country = player3Country;
            this.player4Country = player4Country;
            this.player1Team = player1Team;
            this.player2Team = player2Team;
            this.player3Team = player3Team;
            this.player4Team = player4Team;
        }
    }
}