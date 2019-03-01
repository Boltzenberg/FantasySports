using System.Collections.Generic;
using System.IO;

namespace FantasyAuction
{
    public static class Year
    {
        public static void Create(string batterFile, string pitcherFile)
        {
            File.WriteAllText(batterFile, DataModel.Batter.Serialize(LoadBatters().ToArray()));
            File.WriteAllText(pitcherFile, DataModel.Pitcher.Serialize(LoadPitchers().ToArray()));
        }

        public static void Update(List<DataModel.Batter> batters, List<DataModel.Pitcher> pitchers)
        {
            Dictionary<string, DataModel.Batter> batterMap = new Dictionary<string, DataModel.Batter>();
            foreach (var batter in batters)
            {
                batterMap[batter.Name] = batter;
            }

            Dictionary<string, DataModel.Pitcher> pitcherMap = new Dictionary<string, DataModel.Pitcher>();
            foreach (var pitcher in pitchers)
            {
                pitcherMap[pitcher.Name] = pitcher;
            }

            foreach (ESPNProjections.Batter batter in ESPNProjections.Batter.Load())
            {
                DataModel.Batter b;
                if (batterMap.TryGetValue(batter.FullName, out b))
                {
                    b.Update(batter);
                }
            }

            foreach (ESPNProjections.Pitcher pitcher in ESPNProjections.Pitcher.Load())
            {
                DataModel.Pitcher p;
                if (pitcherMap.TryGetValue(pitcher.FullName, out p))
                {
                    p.Update(pitcher);
                }
            }
        }

        public static List<DataModel.Batter> LoadBatters()
        {
            List<DataModel.Batter> batters = new List<DataModel.Batter>();
            foreach (ESPNProjections.Batter batter in ESPNProjections.Batter.Load())
            {
                batters.Add(DataModel.Batter.Create(batter));
            }

            return batters;
        }

        public static List<DataModel.Pitcher> LoadPitchers()
        {
            List<DataModel.Pitcher> pitchers = new List<DataModel.Pitcher>();
            foreach (ESPNProjections.Pitcher pitcher in ESPNProjections.Pitcher.Load())
            {
                pitchers.Add(DataModel.Pitcher.Create(pitcher));
            }

            return pitchers;
        }
    }
}
