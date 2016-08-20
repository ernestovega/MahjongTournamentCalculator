namespace TournamentCalculator
{
    public class TablePlayer
    {
        public int round;
        public int table;
        public int player;
        public int playerId;

        public TablePlayer(int round, int table, int player, int playerId)
        {
            this.round = round;
            this.table = table;
            this.player = player;
            this.playerId = playerId;
        }
    }
}