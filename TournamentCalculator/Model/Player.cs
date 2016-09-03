namespace TournamentCalculator
{
    public class Player
    {
        public int id;
        public string name;
        public string team;
        public string country;

        public Player(string id, string name, string team, string country)
        {
            this.id = int.Parse(string.IsNullOrEmpty(id) ? "0" : id);
            this.name = name;
            this.team = team;
            this.country = country;
        }

        public Player()
        {

        }

        internal Player Clone()
        {
            return new Player(id.ToString(), name, country, team);
        }
    }
}