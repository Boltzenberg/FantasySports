using FantasyAuction.DataModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace FantasyAuction
{
    public class League
    {
        public DataModel.Batter[] Batters;
        public DataModel.Pitcher[] Pitchers;
        public DataModel.Team[] Teams;

        public const int TeamCount = 10;
        public const int RosterableBatterCountPerTeam = 14;
        public const int RosterablePitcherCountPerTeam = 13;

        public static League Create()
        {
            League league = new League();
            league.Batters = LoadBatters().ToArray();
            league.Pitchers = LoadPitchers().ToArray();
            return league;
        }

        public static League Load(string fileName)
        {
            return JsonConvert.DeserializeObject<League>(File.ReadAllText(fileName));
        }

        public void UpdateProjections()
        {
            Dictionary<string, DataModel.Batter> batterMap = new Dictionary<string, DataModel.Batter>();
            foreach (var batter in LoadBatters())
            {
                batterMap[batter.Name] = batter;
            }

            Dictionary<string, DataModel.Pitcher> pitcherMap = new Dictionary<string, DataModel.Pitcher>();
            foreach (var pitcher in LoadPitchers())
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

        public League Clone()
        {
            string content = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<League>(content);
        }

        public void Save(string fileName)
        {
            File.WriteAllText(fileName, JsonConvert.SerializeObject(this));
        }

        public IEnumerable<IPlayer> AllPlayers
        {
            get
            {
                foreach (var b in this.Batters)
                {
                    yield return b;
                }

                foreach (var p in this.Pitchers)
                {
                    yield return p;
                }
            }
        }

        private static List<DataModel.Batter> LoadBatters()
        {
            List<DataModel.Batter> batters = new List<DataModel.Batter>();
            foreach (ESPNProjections.Batter batter in ESPNProjections.Batter.Load())
            {
                batters.Add(DataModel.Batter.Create(batter));
            }

            return batters;
        }

        private static List<DataModel.Pitcher> LoadPitchers()
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
