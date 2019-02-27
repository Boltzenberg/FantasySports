using System.Collections.Generic;

namespace FantasyAuction.DataModel
{
    public class Team
    {
        public static IEnumerable<Team> Load(IEnumerable<Batter> batters, IEnumerable<Pitcher> pitchers)
        {
            Dictionary<string, Team> teams = new Dictionary<string, Team>();
            foreach (Batter batter in batters)
            {
                if (string.IsNullOrEmpty(batter.FantasyTeam))
                {
                    continue;
                }

                Team team;
                if (!teams.TryGetValue(batter.FantasyTeam, out team))
                {
                    team = new Team(batter.FantasyTeam);
                    teams[team.Name] = team;
                }
                team.Batters.Add(batter);
            }

            foreach (Pitcher pitcher in pitchers)
            {
                if (string.IsNullOrEmpty(pitcher.FantasyTeam))
                {
                    continue;
                }

                Team team;
                if (!teams.TryGetValue(pitcher.FantasyTeam, out team))
                {
                    team = new Team(pitcher.FantasyTeam);
                    teams[team.Name] = team;
                }
                team.Pitchers.Add(pitcher);
            }

            return teams.Values;
        }

        public string Name { get; private set; }
        public List<Batter> Batters { get; private set; }
        public List<Pitcher> Pitchers { get; private set; }

        private Team(string name)
        {
            this.Name = name;
            this.Batters = new List<Batter>();
            this.Pitchers = new List<Pitcher>();
        }
    }
}
