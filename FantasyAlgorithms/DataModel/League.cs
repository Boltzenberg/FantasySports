using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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

        public static readonly List<IStatExtractor> ScoringStatExtractors = new List<IStatExtractor>()
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

        public static readonly List<IStatExtractor> SupportingStatExtractors = new List<IStatExtractor>()
        {
            new CountingStatExtractor("At Bats", true, Extractors.ExtractBatterAtBats),
            new CountingStatExtractor("Hits + Walks", true, Extractors.ExtractBatterHitsPlusWalks),
            new RatioStatExtractor("Innings Pitched", true, Extractors.ExtractPitcherOutsRecorded, p => 3, Ratios.Divide)
        };

        public static Roster GetEmptyRoster()
        {
            Roster emptyRoster = new Roster(new Dictionary<Position, int>()
            {
                { Position.C, 2 },
                { Position.B1, 1 },
                { Position.B2, 1 },
                { Position.B3, 1 },
                { Position.SS, 1 },
                { Position.CI, 1 },
                { Position.MI, 1 },
                { Position.OF, 4 },
                { Position.Util, 2 },
                { Position.P, 9 },
                { Position.BN, 5 },
                { Position.DL, 2 },
                { Position.NA, 1 },
            });

            return emptyRoster;
        }

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

        public void UpdateESPNProjections()
        {
            Dictionary<string, Batter> batterMap = new Dictionary<string, Batter>();
            foreach (var batter in this.Batters)
            {
                batterMap[batter.Name] = batter;
            }

            Dictionary<string, Pitcher> pitcherMap = new Dictionary<string, Pitcher>();
            foreach (var pitcher in this.Pitchers)
            {
                pitcherMap[pitcher.Name] = pitcher;
            }

            foreach (ESPNProjections.Player player in ESPNProjections.Player.LoadProjections())
            {
                if (player.IsBatter)
                {
                    Batter b;
                    if (batterMap.TryGetValue(player.FullName, out b))
                    {
                        b.Update(player);
                    }
                    else
                    {
                        b = Batter.Create(player);
                        batterMap[b.Name] = b;
                    }
                }
                else
                {
                    Pitcher p;
                    if (pitcherMap.TryGetValue(player.FullName, out p))
                    {
                        p.Update(player);
                    }
                    else
                    {
                        p = Pitcher.Create(player);
                        pitcherMap[p.Name] = p;
                    }
                }
            }

            if (this.Batters.Length != batterMap.Count)
            {
                this.Batters = new List<Batter>(batterMap.Values).ToArray();
            }

            if (this.Pitchers.Length != pitcherMap.Count)
            {
                this.Pitchers = new List<Pitcher>(pitcherMap.Values).ToArray();
            }
        }

        public async Task UpdateYahooData()
        {
            foreach (YahooFantasySports.DataModel.Player yPlayer in await YahooFantasySports.DataModel.Player.GetAllPlayers(YahooFantasySports.Constants.Leagues.Rounders2019))
            {
                IPlayer player = this.AllPlayers.FirstOrDefault(p => p.YahooId == yPlayer.Id);
                if (player != null)
                {
                    player.Update(yPlayer);
                }
                else
                {
                    List<IPlayer> sameName = new List<IPlayer>(this.AllPlayers.Where(p => p.Name == yPlayer.Name));
                    if (sameName.Count == 0)
                    {
                        continue;
                    }
                    if (sameName.Count == 1)
                    {
                        sameName[0].Update(yPlayer);
                    }
                    else
                    {
                        List<IPlayer> samePosition = new List<IPlayer>(sameName.Where(p => yPlayer.IsBatter == !p.Positions.Contains(Position.P)));
                        if (samePosition.Count == 1)
                        {
                            samePosition[0].Update(yPlayer);
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine(string.Format("Couldn't match player {0}", yPlayer.Name));
                        }
                    }
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

        private static List<Batter> LoadBatters()
        {
            List<Batter> batters = new List<Batter>();
            foreach (ESPNProjections.Batter batter in ESPNProjections.Batter.Load())
            {
                batters.Add(Batter.Create(batter));
            }

            return batters;
        }

        private static List<DataModel.Pitcher> LoadPitchers()
        {
            List<Pitcher> pitchers = new List<Pitcher>();
            foreach (ESPNProjections.Pitcher pitcher in ESPNProjections.Pitcher.Load())
            {
                pitchers.Add(Pitcher.Create(pitcher));
            }

            return pitchers;
        }
    }
}
