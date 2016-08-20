namespace TournamentCalculator
{
    public class TableWithTeams
    {
        public int roundId;
        public int tableId;
        public int player1Team;
        public int player2Team;
        public int player3Team;
        public int player4Team;

        public TableWithTeams(int roundId, int tableId,
            int player1Team, int player2Team, int player3Team, int player4Team)
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