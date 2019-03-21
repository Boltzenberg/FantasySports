using FantasyAlgorithms.DataModel;
using System.Collections.Generic;

namespace FantasyAlgorithms
{
    public class TeamAnalysis : IRoster
    {
        public Team Team { get; private set; }
        public List<Batter> Batters { get; private set; }
        public List<Pitcher> Pitchers { get; private set; }
        public IDictionary<string, float> Stats { get; private set; }
        public IDictionary<string, float> Points { get; private set; }
        public string TeamName { get { return this.Team.Name; } }

        public float TotalPoints
        {
            get
            {
                float total = 0;
                foreach (float f in this.Points.Values)
                {
                    total += f;
                }

                return total;
            }
        }

        public IEnumerable<IPlayer> Players
        {
            get
            {
                foreach (Batter b in this.Batters)
                {
                    yield return b;
                }

                foreach (Pitcher p in this.Pitchers)
                {
                    yield return p;
                }
            }
        }

        internal TeamAnalysis(Team team)
        {
            this.Team = team;
            this.Batters = new List<Batter>();
            this.Pitchers = new List<Pitcher>();
            this.Stats = new Dictionary<string, float>();
            this.Points = new Dictionary<string, float>();
        }
    }
}
