﻿using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;

namespace YahooFantasySports.DataModel
{
    public class League
    {
        public string Key { get; }
        public string Name { get; }
        public IReadOnlyDictionary<int, StatDefinition> Stats { get; }
        public IReadOnlyList<RosterPosition> RosterPositions { get; }
        public IReadOnlyList<Team> Teams { get; }
        public IReadOnlyDictionary<string, Player> Players { get; }
        public IReadOnlyList<Standings> TeamStandings { get; }

        public static async Task<League> Create(string leagueId)
        {
            string leagueXml = await Services.Http.GetRawDataAsync(UrlGen.LeagueSettingsUrl(leagueId));
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(leagueXml);
            NSMgr nsmgr = new NSMgr(doc);
            XmlNode league = doc.SelectSingleNode(nsmgr.GetXPath("fantasy_content", "league"), nsmgr);

            string key = nsmgr.GetValue(league, "league_key");
            string name = nsmgr.GetValue(league, "name");
            Dictionary<int, StatDefinition> stats = new Dictionary<int, StatDefinition>();
            foreach (XmlNode node in league.SelectNodes(nsmgr.GetXPath("settings", "stat_categories", "stats", "stat"), nsmgr))
            {
                StatDefinition stat = StatDefinition.Create(node, nsmgr);
                stats[stat.Id] = stat;
            }

            List<RosterPosition> rosterPositions = new List<RosterPosition>();
            foreach (XmlNode node in league.SelectNodes(nsmgr.GetXPath("settings", "roster_positions", "roster_position"), nsmgr))
            {
                rosterPositions.Add(RosterPosition.Create(node, nsmgr));
            }

            string leagueTeams = await Services.Http.GetRawDataAsync(UrlGen.TeamsWithRostersUrl(leagueId));
            doc.LoadXml(leagueTeams);
            nsmgr = new NSMgr(doc);
            league = doc.SelectSingleNode(nsmgr.GetXPath("fantasy_content", "league"), nsmgr);

            List<Team> teams = new List<Team>();
            foreach (XmlNode team in league.SelectNodes(nsmgr.GetXPath("teams", "team"), nsmgr))
            {
                teams.Add(Team.Create(team, nsmgr));
            }

            Dictionary<string, Player> players = new Dictionary<string, Player>();
            foreach (Player player in await Player.GetAllPlayers(leagueId))
            {
                player.SetStatValues(stats);
                players[player.Key] = player;
            }

            string leagueStandings = await Services.Http.GetRawDataAsync(UrlGen.LeagueStandings(leagueId));
            doc.LoadXml(leagueStandings);
            nsmgr = new NSMgr(doc);
            league = doc.SelectSingleNode(nsmgr.GetXPath("fantasy_content", "league"), nsmgr);

            List<Standings> standings = new List<Standings>();
            foreach (XmlNode node in league.SelectNodes(nsmgr.GetXPath("standings", "teams", "team"), nsmgr))
            {
                standings.Add(Standings.Create(node, nsmgr));
            }

            return new League(key, name, stats, rosterPositions, teams, players, standings);
        }

        private League(string key, string name, IReadOnlyDictionary<int, StatDefinition> stats, IReadOnlyList<RosterPosition> rosterPositions, IReadOnlyList<Team> teams, IReadOnlyDictionary<string, Player> players, IReadOnlyList<Standings> standings)
        {
            this.Key = key;
            this.Name = name;
            this.Stats = stats;
            this.RosterPositions = rosterPositions;
            this.Teams = teams;
            this.Players = players;
            this.TeamStandings = standings;
        }
    }
}
