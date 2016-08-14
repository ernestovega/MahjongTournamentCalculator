namespace TournamentCalculator
{
    public class Player
    {
        public int id;
        public string name;
        public string country;
        public string team;

        public Player(string id, string name, string country, string team)
        {
            this.id = int.Parse(id);
            this.name = name;
            this.country = country;
            this.team = team;
        }

        internal Player Clone()
        {
            return new Player(id.ToString(), name, country, team);
        }
    }
}