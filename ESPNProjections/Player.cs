using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace ESPNProjections
{
    public class Player : IPlayer
    {
        public static IEnumerable<Player> LoadProjections()
        {
            List<string> jsons = new List<string>()
            {
                ESPNAPI.LoadBatterProjections(),
                ESPNAPI.LoadPitcherProjections()
            };

            foreach (string json in jsons)
            { 
                if (!string.IsNullOrEmpty(json))
                {
                    JObject file = JObject.Parse(json);
                    JArray players = (JArray)file["players"];
                    foreach (JToken player in players)
                    {
                        JToken statsRoot = player["player"]["stats"];
                        if (statsRoot == null || statsRoot.Count() == 0)
                        {
                            continue;
                        }

                        Player p = new Player();
                        p.FullName = (string)player["player"]["fullName"];
                        p.Id = int.Parse((string)player["player"]["id"]);
                        p.Rank = TryGetValue(player, "player", "draftRanksByRankType", "STANDARD", "rank");
                        p.SeasonOutlook = (string)player["player"]["seasonOutlook"];
                        p.Positions = new List<int>(((JArray)player["player"]["eligibleSlots"]).Select(s => (int)s).ToArray());
                        p.Stats = new Dictionary<string, string>();
                        IEnumerable<string> stats = p.IsBatter ? Constants.Stats.Batters.All : Constants.Stats.Pitchers.All;
                        bool foundStats = false;
                        foreach (JToken statSet in player["player"]["stats"].Children())
                        {
                            foreach (string stat in stats)
                            {
                                p.Stats[stat] = (string)statSet["stats"][stat];
                                if (!string.IsNullOrEmpty(p.Stats[stat]))
                                {
                                    foundStats = true;
                                }
                            }

                            if (foundStats)
                            {
                                break;
                            }
                        }
                        if (foundStats)
                        {
                            yield return p;
                        }
                    }
                }
            }
        }

        private static string TryGetValue(JToken root, params string[] path)
        {
            JToken node = root;
            foreach (string child in path)
            {                
                node = node[child];
                if (node == null)
                {
                    return string.Empty;
                }
            }

            return (string)node;
        }

        public string FullName { get; private set; }
        public int Id { get; private set; }
        public string Rank { get; private set; }
        public List<int> Positions { get; private set; }
        public Dictionary<string, string> Stats { get; private set; }
        public string SeasonOutlook { get; private set; }

        public bool IsBatter
        {
            get
            {
                return this.Positions.Contains(Constants.Positions.DH);
            }
        }

        private Player()
        { }
    }
}
