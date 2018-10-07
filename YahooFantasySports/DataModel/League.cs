using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Xml;

namespace YahooFantasySports.DataModel
{
    public class League
    {
        public string Key { get; }
        public string Name { get; }
        public IReadOnlyList<StatDefinition> Stats { get; }
        public IReadOnlyList<RosterPosition> RosterPositions { get; }

        public static async Task<League> Create(string leagueId)
        {
            string leagueXml = await Services.Http.GetRawDataAsync(UrlGen.LeagueSettingsUrl(Constants.Leagues.Rounders2018));
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(leagueXml);
            NSMgr nsmgr = new NSMgr(doc);

            XmlNode league = doc.SelectSingleNode(nsmgr.GetXPath("fantasy_content", "league"), nsmgr);

            string key = nsmgr.GetValue(league, "league_key");
            string name = nsmgr.GetValue(league, "name");
            List<StatDefinition> stats = new List<StatDefinition>();
            foreach (XmlNode node in league.SelectNodes(nsmgr.GetXPath("settings", "stat_categories", "stats", "stat"), nsmgr))
            {
                stats.Add(StatDefinition.Create(node, nsmgr));
            }

            List<RosterPosition> rosterPositions = new List<RosterPosition>();
            foreach (XmlNode node in league.SelectNodes(nsmgr.GetXPath("settings", "roster_positions", "roster_position"), nsmgr))
            {
                rosterPositions.Add(RosterPosition.Create(node, nsmgr));
            }

            return new League(key, name, stats, rosterPositions);
        }

        private League(string key, string name, IReadOnlyList<StatDefinition> stats, IReadOnlyList<RosterPosition> rosterPositions)
        {
            this.Key = key;
            this.Name = name;
            this.Stats = stats;
            this.RosterPositions = rosterPositions;
        }
    }
}
