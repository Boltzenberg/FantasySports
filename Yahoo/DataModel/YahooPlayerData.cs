using FantasySports.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Yahoo.DataModel
{
    public class YahooPlayerData : IPlayerData
    {
        public string DisplayName { get; private set; }

        public Constants.StatSource StatSource { get { return Constants.StatSource.YahooOngoing; } }

        public string SourceID { get; private set; }

        public List<Position> Positions { get; private set; }

        public Dictionary<Constants.StatID, float> Stats { get; private set; }

        public string Outlook { get; private set; }

        private string playerKey;

        public static async Task<List<YahooPlayerData>> GetAllPlayers(string leagueId)
        {
            List<YahooPlayerData> players = new List<YahooPlayerData>();
            int start = 1;
            while (true)
            {
                string playerXml = await Services.Http.GetRawDataAsync(UrlGen.PaginatedPlayers(leagueId, start));
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(playerXml);
                NSMgr nsmgr = new NSMgr(doc);

                XmlNodeList playerRoots = doc.SelectNodes("//" + NSMgr.GetNodeName("player"), nsmgr);
                if (playerRoots.Count == 0)
                {
                    break;
                }

                start += playerRoots.Count;
                foreach (XmlNode playerRoot in playerRoots)
                {
                    players.Add(YahooPlayerData.Create(playerRoot, nsmgr));
                }
            }

            return players;
        }

        private static YahooPlayerData Create(XmlNode node, NSMgr nsmgr)
        {
            YahooPlayerData player = new YahooPlayerData();

            player.playerKey = nsmgr.GetValue(node, "player_key");
            player.SourceID = nsmgr.GetValue(node, "player_id");
            player.DisplayName = nsmgr.GetValue(node, "name", "full");

            XmlNode eligiblePositions = node["eligible_positions"];
            if (eligiblePositions != null)
            {
                foreach (XmlNode position in eligiblePositions.SelectNodes(NSMgr.GetNodeName("position"), nsmgr))
                {
                    player.Positions.Add(YConstants.Positions.PositionFromYahooPosition(position.InnerText));
                }
            }

            Dictionary<int, string> yahooStats = new Dictionary<int, string>();
            foreach (XmlNode stat in node.SelectNodes(nsmgr.GetXPath("player_stats", "stats", "stat"), nsmgr))
            {
                string statIdString = nsmgr.GetValue(stat, "stat_id").Trim();
                string statValue = nsmgr.GetValue(stat, "value").Trim();
                int statId;
                if (int.TryParse(statIdString, out statId))
                {
                    yahooStats[statId] = statValue;
                }
            }
            YConstants.Stats.MapYahooStatDictionaryToDataModelStatDictionary(yahooStats, player.Stats);

            return player;
        }

        public YahooPlayerData()
        {
            this.DisplayName = string.Empty;
            this.SourceID = string.Empty;
            this.Positions = new List<Position>();
            this.Stats = new Dictionary<FantasySports.DataModels.Constants.StatID, float>();
            this.Outlook = string.Empty;
            this.playerKey = string.Empty;
        }
    }
}
