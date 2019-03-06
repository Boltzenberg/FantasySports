using FantasyAuction;
using FantasyAuction.DataModel;
using System.Collections.Generic;

namespace FantasyAlgorithms
{
    public class LeagueAnalysis
    {
        public List<TeamAnalysis> Teams { get; private set; }

        public static LeagueAnalysis Analyze(League league, List<IStatExtractor> extractors)
        {
            Dictionary<string, TeamAnalysis> teamsMap = new Dictionary<string, TeamAnalysis>();
            foreach (Team team in league.Teams)
            {
                teamsMap[team.Name] = new TeamAnalysis(team);
            }

            foreach (Batter b in league.Batters)
            {
                if (!string.IsNullOrEmpty(b.FantasyTeam))
                {
                    teamsMap[b.FantasyTeam].Batters.Add(b);
                }
            }

            foreach (Pitcher p in league.Pitchers)
            {
                if (!string.IsNullOrEmpty(p.FantasyTeam))
                {
                    teamsMap[p.FantasyTeam].Pitchers.Add(p);
                }
            }

            foreach (TeamAnalysis team in teamsMap.Values)
            {
                foreach (IStatExtractor extractor in extractors)
                {
                    List<IStatValue> statValues = new List<IStatValue>();
                    foreach (IPlayer p in team.AllPlayers)
                    {
                        IStatValue value = extractor.Extract(p);
                        if (value != null)
                        {
                            statValues.Add(value);
                        }
                    }

                    IStatValue agg = extractor.Aggregate(statValues);
                    team.Stats[extractor.StatName] = agg.Value;
                }
            }

            List<TeamAnalysis> teams = new List<TeamAnalysis>(teamsMap.Values);

            foreach (IStatExtractor extractor in extractors)
            {
                if (extractor.MoreIsBetter)
                {
                    teams.Sort((x, y) => y.Stats[extractor.StatName].CompareTo(x.Stats[extractor.StatName]));
                }
                else
                {
                    teams.Sort((x, y) => x.Stats[extractor.StatName].CompareTo(y.Stats[extractor.StatName]));
                }

                for (int i = 0; i < teams.Count;)
                {
                    int countOfEqual = 1;
                    int totalPoints = teams.Count - i;
                    while (i + countOfEqual < teams.Count && teams[i + countOfEqual - 1].Stats[extractor.StatName] == teams[i + countOfEqual].Stats[extractor.StatName])
                    {
                        totalPoints += teams.Count - (i + countOfEqual);
                        countOfEqual++;
                    }

                    float pointsPer = (float)totalPoints / countOfEqual;
                    for (int k = 0; k < countOfEqual; k++)
                    {
                        teams[i + k].Points[extractor.StatName] = pointsPer;
                    }

                    i += countOfEqual;
                }
            }

            LeagueAnalysis leagueAnalysis = new LeagueAnalysis();
            leagueAnalysis.Teams = teams;
            return leagueAnalysis;
        }
    }
}
