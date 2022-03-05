using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESPNProjections
{
    public class Player : IPlayer
    {
        public static IEnumerable<Player> LoadProjections()
        {
            // Map from JSON file to set of relevant stats for the players in that file
            List<Tuple<string, bool>> playerSets = new List<Tuple<string, bool>>()
            {
                new Tuple<string, bool>(ESPNAPI.LoadBatterProjections(), true),
                new Tuple<string, bool>(ESPNAPI.LoadPitcherProjections(), false),
            };

            foreach (Tuple<string, bool> playerSet in playerSets)
            {
                string json = playerSet.Item1;
                IEnumerable<string> stats = playerSet.Item2 ? ESPNConstants.Stats.Batters.All : ESPNConstants.Stats.Pitchers.All;
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
                        p.IsBatter = playerSet.Item2;
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
        public bool IsBatter { get; private set; }

        private Player()
        { }
    }
}
