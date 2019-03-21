using System.Collections.Generic;

namespace FantasyAlgorithms.DataModel
{
    public class Roster : IRoster
    {
        public string TeamName { get; private set; }
        public List<IPlayer> AllPlayers { get; private set; }
        public IDictionary<string, float> Stats { get; private set; }
        public IDictionary<string, float> Points { get; private set; }

        public Roster(string teamName)
        {
            this.TeamName = teamName;
            this.AllPlayers = new List<IPlayer>();
            this.Stats = new Dictionary<string, float>();
            this.Points = new Dictionary<string, float>();
        }

        public IEnumerable<IPlayer> Players
        {
            get
            {
                return this.AllPlayers;
            }
        }
    }
}
