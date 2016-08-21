namespace TournamentCalculator
{
    public class TableWithTeams
    {
        public int roundId;
        public int tableId;
        public string player1Team;
        public string player2Team;
        public string player3Team;
        public string player4Team;

        public TableWithTeams(int roundId, int tableId,
            string player1Team, string player2Team, string player3Team, string player4Team)
        {
            this.roundId = roundId;
            this.tableId = tableId;
            this.player1Team = player1Team;
            this.player2Team = player2Team;
            this.player3Team = player3Team;
            this.player4Team = player4Team;
        }

        public TableWithTeams()
        {

        }
    }
}