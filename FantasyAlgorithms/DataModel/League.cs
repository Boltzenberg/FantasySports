using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FantasyAlgorithms.DataModel
{
    public enum Leagues
    {
        Rounders2019,
        CrossCountryRivals2019
    }

    public class LeagueConstants
    {
        public int TeamCount { get; private set; }
        public int RosterableBatterCountPerTeam { get; private set; }
        public int RosterablePitcherCountPerTeam { get; private set; }
        public IReadOnlyList<IStatExtractor> ScoringStatExtractors { get; private set; }
        public IReadOnlyList<IStatExtractor> SupportingStatExtractors { get; private set; }
        public string YahooLeagueId { get; private set; }

        public static LeagueConstants For(Leagues league)
        {
            switch (league)
            {
                case Leagues.Rounders2019: return Rounders2019;
                case Leagues.CrossCountryRivals2019: return CrossCountryRivals2019;
                default: return null;
            }
        }

        private static LeagueConstants _rounders2019 = null;
        public static LeagueConstants Rounders2019
        {
            get
            {
                if (_rounders2019 == null)
                {
                    _rounders2019 = new LeagueConstants();
                    _rounders2019.TeamCount = 10;
                    _rounders2019.RosterableBatterCountPerTeam = 14;
                    _rounders2019.RosterablePitcherCountPerTeam = 13;
                    _rounders2019.ScoringStatExtractors = new List<IStatExtractor>()
                    {
                        new CountingStatExtractor("Runs", true, Extractors.ExtractBatterRuns),
                        new CountingStatExtractor("Home Runs", true, Extractors.ExtractBatterHomeRuns),
                        new CountingStatExtractor("RBIs", true, Extractors.ExtractBatterRBIs),
                        new CountingStatExtractor("Steals", true, Extractors.ExtractBatterSteals),
                        new RatioStatExtractor("OBP", true, Extractors.ExtractBatterHitsPlusWalks, Extractors.ExtractBatterAtBats, Ratios.Divide),
                        new CountingStatExtractor("Wins", true, Extractors.ExtractPitcherWins),
                        new CountingStatExtractor("Saves", true, Extractors.ExtractPitcherSaves),
                        new CountingStatExtractor("Strikeouts", true, Extractors.ExtractPitcherStrikeouts),
                        new RatioStatExtractor("ERA", false, Extractors.ExtractPitcherEarnedRuns, Extractors.ExtractPitcherOutsRecorded, Ratios.PerNineInnings),
                        new RatioStatExtractor("WHIP", false, Extractors.ExtractPitcherWalksPlusHits, Extractors.ExtractPitcherOutsRecorded, Ratios.PerInning)
                    };
                    _rounders2019.SupportingStatExtractors = new List<IStatExtractor>()
                    {
                        new CountingStatExtractor("At Bats", true, Extractors.ExtractBatterAtBats),
                        new CountingStatExtractor("Hits + Walks", true, Extractors.ExtractBatterHitsPlusWalks),
                        new RatioStatExtractor("Innings Pitched", true, Extractors.ExtractPitcherOutsRecorded, p => 3, Ratios.Divide)
                    };
                    _rounders2019.YahooLeagueId = YahooFantasySports.Constants.Leagues.Rounders2019;
                }

                return _rounders2019;
            }
        }

        private static LeagueConstants _crossCountryRivals2019 = null;
        public static LeagueConstants CrossCountryRivals2019
        {
            get
            {
                if (_crossCountryRivals2019 == null)
                {
                    _crossCountryRivals2019 = new LeagueConstants();
                    _crossCountryRivals2019.TeamCount = 12;
                    _crossCountryRivals2019.RosterableBatterCountPerTeam = 9;
                    _crossCountryRivals2019.RosterablePitcherCountPerTeam = 6;
                    _crossCountryRivals2019.ScoringStatExtractors = new List<IStatExtractor>()
                    {
                        new CountingStatExtractor("Runs", true, Extractors.ExtractBatterRuns),
                        new CountingStatExtractor("Home Runs", true, Extractors.ExtractBatterHomeRuns),
                        new CountingStatExtractor("RBIs", true, Extractors.ExtractBatterRBIs),
                        new CountingStatExtractor("Steals", true, Extractors.ExtractBatterSteals),
                        new RatioStatExtractor("AVG", true, Extractors.ExtractBatterHits, Extractors.ExtractBatterAtBats, Ratios.Divide),
                        new RatioStatExtractor("OBP", true, Extractors.ExtractBatterHitsPlusWalks, Extractors.ExtractBatterAtBats, Ratios.Divide),
                        new CountingStatExtractor("Saves", true, Extractors.ExtractPitcherSaves),
                        new CountingStatExtractor("Holds", true, Extractors.ExtractPitcherHolds),
                        new RatioStatExtractor("ERA", false, Extractors.ExtractPitcherEarnedRuns, Extractors.ExtractPitcherOutsRecorded, Ratios.PerNineInnings),
                        new RatioStatExtractor("WHIP", false, Extractors.ExtractPitcherWalksPlusHits, Extractors.ExtractPitcherOutsRecorded, Ratios.PerInning),
                        new RatioStatExtractor("K/9", true, Extractors.ExtractPitcherStrikeouts, Extractors.ExtractPitcherOutsRecorded, Ratios.PerNineInnings),
                        new RatioStatExtractor("Win%", true, Extractors.ExtractPitcherWins, Extractors.ExtractPitcherDecisions, Ratios.Divide),
                    };
                    _crossCountryRivals2019.SupportingStatExtractors = new List<IStatExtractor>()
                    {
                        new CountingStatExtractor("At Bats", true, Extractors.ExtractBatterAtBats),
                        new CountingStatExtractor("Hits + Walks", true, Extractors.ExtractBatterHitsPlusWalks),
                        new RatioStatExtractor("Innings Pitched", true, Extractors.ExtractPitcherOutsRecorded, p => 3, Ratios.Divide)
                    };
                    _crossCountryRivals2019.YahooLeagueId = YahooFantasySports.Constants.Leagues.CrossCountryRivals2019;
                }

                return _crossCountryRivals2019;
            }
        }
    }

    public class League
    {
        public Batter[] Batters;
        public Pitcher[] Pitchers;
        public Team[] Teams;
        public Leagues FantasyLeague;

        public static League Create(Leagues fantasyLeague)
        {
            League league = new League();
            league.Batters = new Batter[0];
            league.Pitchers = new Pitcher[0];
            league.UpdateESPNProjections();
            league.FantasyLeague = fantasyLeague;
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
            foreach (YahooFantasySports.DataModel.Player yPlayer in await YahooFantasySports.DataModel.Player.GetAllPlayers(LeagueConstants.For(this.FantasyLeague).YahooLeagueId))
            {
                IPlayer player = this.AllPlayers.FirstOrDefault(p => p.YahooId == yPlayer.Id);
                if (player != null)
                {
                    player.Update(yPlayer);
                }
                else
                {
                    List<IPlayer> sameName = new List<IPlayer>(this.AllPlayers.Where(p => string.IsNullOrEmpty(p.YahooId) && (p.Name == yPlayer.Name)));
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
    }
}
