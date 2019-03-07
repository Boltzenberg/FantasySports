using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace FantasyAlgorithms.DataModel
{
    public class League
    {
        public Batter[] Batters;
        public Pitcher[] Pitchers;
        public Team[] Teams;

        public const int TeamCount = 10;
        public const int RosterableBatterCountPerTeam = 14;
        public const int RosterablePitcherCountPerTeam = 13;

        public static readonly List<IStatExtractor> StatExtractors = new List<IStatExtractor>()
        {
            new CountingStatExtractor("Runs", true, Extractors.ExtractBatterRuns),
            new CountingStatExtractor("Home Runs", true, Extractors.ExtractBatterHomeRuns),
            new CountingStatExtractor("RBIs", true, Extractors.ExtractBatterRBIs),
            new CountingStatExtractor("Steals", true, Extractors.ExtractBatterSteals),
            new RatioStatExtractor("OBP", true, Extractors.ExtractBatterHitsPlusWalks, Extractors.ExtractBatterAtBats, Ratios.Divide),
            new CountingStatExtractor("Wins", true, Extractors.ExtractPitcherWins),
            new CountingStatExtractor("Saves", true, Extractors.ExtractPitcherSaves),
            new CountingStatExtractor("Strikeouts", true, Extractors.ExtractPitcherStrikeouts),
            new RatioStatExtractor("ERA", false, Extractors.ExtractPitcherEarnedRuns, Extractors.ExtractPitcherOutsRecorded, Ratios.ERA),
            new RatioStatExtractor("WHIP", false, Extractors.ExtractPitcherWalksPlusHits, Extractors.ExtractPitcherOutsRecorded, Ratios.WHIP)
        };

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
