using FantasyAlgorithms.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FantasyAlgorithms
{
    public static class RosterAnalysis
    {
        public static List<IRoster> AssignPlayersToTeams(League league, Func<IPlayer, string> extractTeam)
        {
            Dictionary<string, TeamAnalysis> teamsMap = new Dictionary<string, TeamAnalysis>();
            foreach (Team team in league.Teams)
            {
                teamsMap[team.Name] = new TeamAnalysis(team);
            }

            foreach (Batter b in league.Batters)
            {
                if (!string.IsNullOrEmpty(extractTeam(b)))
                {
                    teamsMap[extractTeam(b)].Batters.Add(b);
                }
            }

            foreach (Pitcher p in league.Pitchers)
            {
                if (!string.IsNullOrEmpty(extractTeam(p)))
                {
                    teamsMap[extractTeam(p)].Pitchers.Add(p);
                }
            }

            return new List<IRoster>(teamsMap.Values);
        }

        public static void AssignStatsAndPoints(List<IRoster> teams, List<IStatExtractor> extractors)
        {
            foreach (IRoster team in teams)
            {
                foreach (IStatExtractor extractor in extractors)
                {
                    List<IStatValue> statValues = new List<IStatValue>();
                    foreach (IPlayer p in team.Players)
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
        }

        public static float ExtractTotalPoints(IRoster roster)
        {
            float total = 0.0f;
            foreach (float points in roster.Points.Values)
            {
                total += points;
            }

            return total;
        }

        public static string GetTopNFreeAgentPickups(League league, List<IStatExtractor> extractors, string teamName)
        {
            List<IRoster> baselineTeams = RosterAnalysis.AssignPlayersToTeams(league, p => p.FantasyTeam);
            RosterAnalysis.AssignStatsAndPoints(baselineTeams, extractors);
            IRoster baselineTeam = baselineTeams.Find(t => t.TeamName == teamName);
            if (baselineTeam == null)
            {
                return string.Empty;
            }

            StringBuilder result = new StringBuilder("<HTML><BODY><TABLE BORDER='1'><TR><TD>Player To Add</TD><TD>Player To Drop</TD><TD>Total Delta</TD>");
            foreach (IStatExtractor extractor in extractors)
            {
                result.AppendFormat("<TD>{0}</TD>", extractor.StatName);
            }
            result.Append("</TR>");

            League l = league.Clone();
            List<Tuple<float, string>> lines = new List<Tuple<float, string>>();
            foreach (IPlayer p in l.AllPlayers)
            {
                if (!string.IsNullOrEmpty(p.FantasyTeam))
                {
                    continue;
                }

                p.FantasyTeam = baselineTeam.TeamName;
                HashSet<Position> newPlayerPositions = new HashSet<Position>(p.Positions.Where(position => Positions.DisplayPositions.Contains(position)));
                if (newPlayerPositions.Count > 1)
                {
                    newPlayerPositions.Remove(Position.Util);
                }
                foreach (IPlayer teamPlayerToSwap in baselineTeam.Players.Where(tpts => tpts.Positions.Intersect(newPlayerPositions).Count() > 0))
                {
                    List<IRoster> teams = RosterAnalysis.AssignPlayersToTeams(l, pb => pb.ESPNId == teamPlayerToSwap.ESPNId ? string.Empty : pb.FantasyTeam);
                    RosterAnalysis.AssignStatsAndPoints(teams, extractors);
                    IRoster team = teams.Find(t => t.TeamName == baselineTeam.TeamName);

                    float totalDelta = 0f;
                    StringBuilder sb = new StringBuilder();
                    foreach (IStatExtractor extractor in extractors)
                    {
                        float delta = team.Points[extractor.StatName] - baselineTeam.Points[extractor.StatName];
                        sb.AppendFormat("<TD>{0}</TD>", delta);
                        totalDelta += delta;
                    }

                    if (totalDelta > 0)
                    {
                        string line = string.Format("<TR><TD>{0}</TD><TD>{1}</TD><TD>{2}</TD>{3}</TR>", p.Name, teamPlayerToSwap.Name, totalDelta, sb.ToString());
                        lines.Add(new Tuple<float, string>(totalDelta, line));
                    }
                }

                p.FantasyTeam = string.Empty;
            }
            lines.Sort((a, b) => b.Item1.CompareTo(a.Item1));
            foreach (Tuple<float, string> line in lines)
            {
                result.AppendLine(line.Item2);
            }
            result.Append("</TABLE></BODY></HTML>");
            return result.ToString();
        }
    }
}
