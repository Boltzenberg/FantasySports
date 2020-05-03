using FantasySports.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Yahoo.DataModel
{
    public class YahooLeague : ILeague
    {
        public string Name { get; private set; }

        public List<Team> Teams { get; private set; }

        public List<Constants.StatID> ScoringStats { get; private set; }

        public Dictionary<Position, int> PositionCounts { get; private set; }

        public IEnumerable<int> AllPlayers { get; private set; }

        public static async Task<YahooLeague> Create(string leagueId)
        {
            string leagueXml = await Services.Http.GetRawDataAsync(UrlGen.LeagueSettingsUrl(leagueId));
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(leagueXml);
            NSMgr nsmgr = new NSMgr(doc);
            XmlNode league = doc.SelectSingleNode(nsmgr.GetXPath("fantasy_content", "league"), nsmgr);

            string name = nsmgr.GetValue(league, "name");

            List<Constants.StatID> scoringStats = new List<Constants.StatID>();
            foreach (XmlNode node in league.SelectNodes(nsmgr.GetXPath("settings", "stat_categories", "stats", "stat"), nsmgr))
            {
                string displayOnly = nsmgr.GetValue(node, "is_only_display_stat"); // !1 -> counts for points
                if (displayOnly != "1")
                {
                    string idString = nsmgr.GetValue(node, "stat_id");
                    int id = int.Parse(idString);
                    scoringStats.Add(YConstants.Stats.ScoringStatIdFromYahooStatId(id));
                }
            }

            Dictionary<Position, int> positionCounts = new Dictionary<Position, int>();
            foreach (XmlNode node in league.SelectNodes(nsmgr.GetXPath("settings", "roster_positions", "roster_position"), nsmgr))
            {
                string position = nsmgr.GetValue(node, "position");
                string countString = nsmgr.GetValue(node, "count");
                int count = int.Parse(countString);
                positionCounts[YConstants.Positions.PositionFromYahooPosition(position)] = count;
            }

            List<YahooPlayerData> allPlayers = await YahooPlayerData.GetAllPlayers(leagueId);

            string leagueTeams = await Services.Http.GetRawDataAsync(UrlGen.TeamsWithRostersUrl(leagueId));
            doc.LoadXml(leagueTeams);
            nsmgr = new NSMgr(doc);
            league = doc.SelectSingleNode(nsmgr.GetXPath("fantasy_content", "league"), nsmgr);

            List<Team> teams = new List<Team>();
            foreach (XmlNode team in league.SelectNodes(nsmgr.GetXPath("teams", "team"), nsmgr))
            {
            }

            return null;
        }

        private YahooLeague()
        {

        }
    }
}
