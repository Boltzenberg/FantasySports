using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyAuction
{
    public static class Year
    {
        public static void Create(string batterFile, string pitcherFile)
        {
            List<DataModel.Batter> batters = new List<DataModel.Batter>();
            foreach (ESPNProjections.Batter batter in ESPNProjections.Batter.Load())
            {
                batters.Add(DataModel.Batter.Create(batter));
            }

            File.WriteAllText(batterFile, DataModel.Batter.Serialize(batters.ToArray()));

            List<DataModel.Pitcher> pitchers = new List<DataModel.Pitcher>();
            foreach (ESPNProjections.Pitcher pitcher in ESPNProjections.Pitcher.Load())
            {
                pitchers.Add(DataModel.Pitcher.Create(pitcher));
            }

            File.WriteAllText(pitcherFile, DataModel.Pitcher.Serialize(pitchers.ToArray()));
        }
    }
}
