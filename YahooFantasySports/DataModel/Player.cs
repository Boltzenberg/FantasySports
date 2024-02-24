using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Xml;

namespace YahooFantasySports.DataModel
{
    public class Player
    {
        public string Key { get; }
        public string Id { get; }
        public bool IsBatter { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string TeamAbbreviation { get; }
        public string Name { get { return FirstName + " " + LastName; } }
        public string Status { get; }
        public List<string> Positions { get; }
        public IReadOnlyDictionary<int, string> Stats { get; }
        public IReadOnlyDictionary<int, StatValue> StatValues { get; private set; }

        public static async Task<List<Player>> GetAllPlayers(string leagueId)
        {
            List<Player> players = new List<Player>();
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
                    players.Add(Player.Create(playerRoot, nsmgr));
                }
            }

            return players;
        }

        private static Player Create(XmlNode node, NSMgr nsmgr)
        {
            string positionType = nsmgr.GetValue(node, "position_type");
            string playerKey = nsmgr.GetValue(node, "player_key");
            string playerId = nsmgr.GetValue(node, "player_id");
            string status = nsmgr.GetValue(node, "status");
            string firstName = nsmgr.GetValue(node, "name", "ascii_first");
            string lastName = nsmgr.GetValue(node, "name", "ascii_last");
            string teamAbbreviation = nsmgr.GetValue(node, "editorial_team_abbr");

            List<string> positions = new List<string>();
            XmlNode eligiblePositions = node["eligible_positions"];
            if (eligiblePositions != null)
            {
                foreach (XmlNode position in eligiblePositions.SelectNodes(NSMgr.GetNodeName("position"), nsmgr))
                {
                    positions.Add(position.InnerText);
                }
            }

            Dictionary<int, string> stats = new Dictionary<int, string>();
            foreach (XmlNode stat in node.SelectNodes(nsmgr.GetXPath("player_stats", "stats", "stat"), nsmgr))
            {
                string keyString = nsmgr.GetValue(stat, "stat_id").Trim();
                string value = nsmgr.GetValue(stat, "value").Trim();
                int key;
                if (int.TryParse(keyString, out key) && !string.IsNullOrEmpty(value))
                {
                    stats[key] = value;
                }
            }

            return new Player(playerKey, playerId, positionType == "B", firstName, lastName, teamAbbreviation, status, positions, stats);
        }

        private Player(string key, string id, bool isBatter, string firstName, string lastName, string teamAbbreviation, string status, List<string> positions, IReadOnlyDictionary<int, string> stats)
        {
            this.Key = key;
            this.Id = id;
            this.IsBatter = isBatter;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.TeamAbbreviation = teamAbbreviation;
            this.Status = string.IsNullOrEmpty(status) ? "OK" : status;
            this.Positions = positions;
            this.Stats = stats;
        }

        public void SetStatValues(IReadOnlyDictionary<int, StatDefinition> stats)
        {
            Dictionary<int, StatValue> statValues = new Dictionary<int, StatValue>();
            foreach (int statId in this.Stats.Keys)
            {
                statValues[statId] = StatValue.Create(this.Stats[statId], stats[statId]);
            }

            this.StatValues = statValues;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} ({2} - {3})", this.FirstName, this.LastName, this.TeamAbbreviation, string.Join(", ", this.Positions));
        }
    }
}
