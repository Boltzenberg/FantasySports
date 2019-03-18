using FantasyAlgorithms.DataModel;
using System;
using System.Collections.Generic;

namespace FantasyAlgorithms
{
    public class LeagueAnalysis
    {
        public List<IRoster> Teams { get; private set; }

        public static LeagueAnalysis Analyze(League league, List<IStatExtractor> extractors, Func<IPlayer, string> extractTeam)
        {
            List<IRoster> teams = AssignPlayersToTeams(league, extractTeam);
            RosterAnalysis.AssignStatsAndPoints(teams, extractors);

            LeagueAnalysis leagueAnalysis = new LeagueAnalysis();
            leagueAnalysis.Teams = teams;
            return leagueAnalysis;
        }

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
    }
}
