using DM = FantasySports.DataModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using YahooFantasySports;
//using YahooFantasySports.DataModel;
//using YahooFantasySports.Services;
using Yahoo.DataModel;
using Yahoo.Services;
using Yahoo;

namespace Scratch
{
    class Program
    {
        static async Task InitializeAuthManager()
        {
            if (!AuthManager.Instance.InitializeAuthorizationFromCache())
            {
                Console.WriteLine("Browse to: {0}", AuthManager.GetAuthUrl());
                System.Diagnostics.Process.Start(AuthManager.GetAuthUrl().AbsoluteUri);
                Console.Write("Enter Authorization Code: ");
                string authorizationCode = Console.ReadLine();
                if (!string.IsNullOrEmpty(authorizationCode))
                {
                    await AuthManager.Instance.InitializeAuthorizationAsync(authorizationCode);
                }
            }
        }

        static async Task GetLeagueStats()
        {
            string stats = await Http.GetRawDataAsync(UrlGen.LeagueSettingsUrl(YConstants.Leagues.Rounders2019));
            Console.WriteLine(stats);
        }

        static void Main(string[] args)
        {
            //SetESPN2020ProjectionsInRoot();
            LoadESPNProjections();
            //UpdateTeams();
            //YahooStuff();
            //Percentiles();
            //RootPercentiles();
        }

        static void SetESPN2020ProjectionsInRoot()
        {
            const string file = "C:\\Users\\jon_r\\OneDrive\\Documents\\FantasyData.json";

            DM.Root root = null;
            if (File.Exists(file))
            {
                root = DM.Root.Load(file);
            }
            else
            {
                root = DM.Root.Create();
            }

            ESPNProjections.ESPNPlayerData.LoadProjectionsIntoRoot(root);
            root.Save(file);
        }

        static IEnumerable<int> GetPlayersAtPosition(DM.Root root, DM.Constants.StatSource source, DM.Position position)
        {
            foreach (int playerId in root.Players.Keys)
            {
                DM.IPlayerData data;
                if (root.Players[playerId].PlayerData.TryGetValue(source, out data))
                {
                    if (data.Positions.Contains(position))
                    {
                        yield return playerId;
                    }
                }
            }
        }

        static int FindPlayer(DM.Root root, string name)
        {
            foreach (int playerId in root.Players.Keys)
            {
                if (root.Players[playerId].Name.ToLowerInvariant() == name.ToLowerInvariant())
                {
                    return playerId;
                }
            }

            return -1;
        }

        static void RootPercentiles()
        {
            const string file = "C:\\Users\\jon_r\\OneDrive\\Documents\\FantasyData.json";
            DM.Root root = DM.Root.Load(file);

            StringBuilder sb = new StringBuilder();
            sb.Append("<HTML><BODY><H1>Analysis of 2020 Projections, Gary Sanchez Selected Among Catchers</H1><TABLE BORDER=1><TR><TD>Stat Name</TD><TD>Player Count</TD><TD>Max Value</TD><TD>Min Value</TD><TD>Graph</TD><TD>Player Percentile</TD></TR>");
            DM.DataProcessing.CountingStatExtractor extractor = new DM.DataProcessing.CountingStatExtractor(true, DM.Constants.StatID.B_HomeRuns);
            DM.Algorithms.PlayerGroupStatAnalysis analysis = new DM.Algorithms.PlayerGroupStatAnalysis(root.Players.Keys, GetPlayersAtPosition(root, DM.Constants.StatSource.ESPNProjections, DM.Position.C), FindPlayer(root, "gary sanchez"), extractor, root, DM.Constants.StatSource.ESPNProjections);

            byte[] img;
            using (MemoryStream stm = new MemoryStream())
            {
                analysis.Graph.Save(stm, System.Drawing.Imaging.ImageFormat.Jpeg);
                img = stm.ToArray();
            }

            sb.AppendLine($"<TR><TD>{FantasySports.DataModels.Constants.StatIDToString(extractor.StatID)}</TD><TD>{analysis.DataPoints}</TD><TD>{analysis.MaxStatValue}</TD><TD>{analysis.MinStatValue}</TD><TD><img src=\"data:image/jpg;base64,{Convert.ToBase64String(img)}\"/></TD><TD>TBD</TD></TR>");

            sb.AppendLine("</TABLE></BODY></HTML>");
            System.IO.File.WriteAllText("analysis.html", sb.ToString());
            System.Diagnostics.Process.Start("analysis.html");
        }

        static void Percentiles()
        {
            const string file = "C:\\Users\\Jon\\OneDrive\\Documents\\Rounders2020.json";
            StringBuilder sb = new StringBuilder();
            sb.Append("<HTML><BODY><H1>Analysis of 2020 Projections, Aaron Judge Selected</H1><TABLE BORDER=1><TR><TD>Stat Name</TD><TD>Player Count</TD><TD>Max Value</TD><TD>Min Value</TD><TD>Graph</TD><TD>Player Percentile</TD></TR>");
            FantasyAlgorithms.DataModel.League league = FantasyAlgorithms.DataModel.League.Load(file);
            FantasyAlgorithms.PercentilePlayerGroupAnalyzer analyzer = new FantasyAlgorithms.PercentilePlayerGroupAnalyzer();
            FantasyAlgorithms.DataModel.LeagueConstants constants = FantasyAlgorithms.DataModel.LeagueConstants.For(league.FantasyLeague);
            foreach (FantasyAlgorithms.IStatExtractor extractor in  constants.ScoringStatExtractors)
            {
                using (FantasyAlgorithms.PlayerGroupAnalysis analysis = analyzer.Analyze("Entire League, selecting Aaron Judge", extractor, league.AllPlayers, league.AllPlayers.FirstOrDefault(p => p.Name.Contains("Judge"))))
                {
                    byte[] img;
                    using (MemoryStream stm = new MemoryStream())
                    {
                        analysis.Graph.Save(stm, System.Drawing.Imaging.ImageFormat.Jpeg);
                        img = stm.ToArray();
                    }
                    sb.AppendLine($"<TR><TD>{analysis.Stat}</TD><TD>{analysis.DataPoints}</TD><TD>{analysis.MaxStatValue}</TD><TD>{analysis.MinStatValue}</TD><TD><img src=\"data:image/jpg;base64,{Convert.ToBase64String(img)}\"/></TD><TD>{analysis.PlayerPercentile}</TD></TR>");
                }
            }
            sb.AppendLine("</TABLE></BODY></HTML>");
            System.IO.File.WriteAllText("C:\\users\\jon\\desktop\\analysis.html", sb.ToString());
        }

        static void PercentileAnalysis(IEnumerable<FantasyAlgorithms.DataModel.IPlayer> playerSet, Func<FantasyAlgorithms.DataModel.IPlayer, float> statExtractor, FantasyAlgorithms.DataModel.IPlayer selectedPlayer)
        {
            List<Tuple<float, FantasyAlgorithms.DataModel.IPlayer>> playersWithStats = new List<Tuple<float, FantasyAlgorithms.DataModel.IPlayer>>();
            int barCount = 0;
            float barMin = float.MaxValue;
            float barMax = float.MinValue;

            foreach (FantasyAlgorithms.DataModel.IPlayer player in playerSet)
            {
                float stat = statExtractor(player);
                playersWithStats.Add(new Tuple<float, FantasyAlgorithms.DataModel.IPlayer>(stat, player));
                barCount++;
                barMin = Math.Min(barMin, stat);
                barMax = Math.Max(barMax, stat);
            }

            playersWithStats.Sort((Tuple<float, FantasyAlgorithms.DataModel.IPlayer> x, Tuple<float, FantasyAlgorithms.DataModel.IPlayer> y) => y.Item1.CompareTo(x.Item1));
            
            const int imgWidth = 800;
            const int imgHeight = 480;

            int barWidth = (int)(imgWidth / barCount);
            Bitmap graph = new Bitmap(imgWidth, imgHeight);
            using (Graphics g = Graphics.FromImage(graph))
            {
                for (int i = 0; i < playersWithStats.Count; i++)
                {
                    float barHeightPercent = playersWithStats[i].Item1 / barMax;
                    int barHeight = (int)(barHeightPercent * imgHeight);
                    Rectangle rect = new Rectangle(i * barWidth, imgHeight - barHeight, barWidth, barHeight);
                    g.FillRectangle(playersWithStats[i].Item2 == selectedPlayer ? Brushes.Green : Brushes.Red, rect);
                }
            }

            graph.Save("C:\\users\\jon\\desktop\\test.bmp");
        }

        static void LoadESPNProjections()
        {
            foreach (ESPNProjections.Player player in ESPNProjections.Player.LoadProjections())
            {
                Console.WriteLine(player.FullName);
            }
        }

        static void UpdateTeams()
        {
            const string file = "C:\\Users\\Jon\\OneDrive\\Documents\\Rounders2020.json";
            FantasyAlgorithms.DataModel.League league = FantasyAlgorithms.DataModel.League.Load(file);
            league.Teams = new FantasyAlgorithms.DataModel.Team[10];
            league.Teams[0] = new FantasyAlgorithms.DataModel.Team() { Name = "Price is wrong Mitch", Owner = "Nir Modiano", Budget = 209.68f };
            league.Teams[1] = new FantasyAlgorithms.DataModel.Team() { Name = "Peanut Tossers", Owner = "Josh Kornblit", Budget = 225.08f };
            league.Teams[2] = new FantasyAlgorithms.DataModel.Team() { Name = "Kirby SMASH", Owner = "Bobby Ronaghy", Budget = 190.62f };
            league.Teams[3] = new FantasyAlgorithms.DataModel.Team() { Name = "See You In McCourt", Owner = "Eric Rudin", Budget = 204.94f };
            league.Teams[4] = new FantasyAlgorithms.DataModel.Team() { Name = "20% off at Cole’s", Owner = "Macus", Budget = 202.57f };
            league.Teams[5] = new FantasyAlgorithms.DataModel.Team() { Name = "Cain and Able", Owner = "Michael Sneag", Budget = 186.33f };
            league.Teams[6] = new FantasyAlgorithms.DataModel.Team() { Name = "Jews on First", Owner = "Steve Lesser", Budget = 210.89f };
            league.Teams[7] = new FantasyAlgorithms.DataModel.Team() { Name = "Putz on Second", Owner = "Jon Rosenberg", Budget = 217.90f };
            league.Teams[8] = new FantasyAlgorithms.DataModel.Team() { Name = "Cool WHIP", Owner = "Jeff Selman", Budget = 248.23f };
            league.Teams[9] = new FantasyAlgorithms.DataModel.Team() { Name = "The S stand for OPS", Owner = "Jared Hersh", Budget = 203.67f };
            league.Save(file);
        }

        static void YahooStuff()
        {
            InitializeAuthManager().Wait();

            const string file = "C:\\Users\\jon_r\\OneDrive\\Documents\\FantasyData.json";
            DM.Root root = DM.Root.Load(file);

            YahooLeague league = YahooLeague.Create(YConstants.Leagues.Rounders2019).Result;
            Console.WriteLine("{0}", league.Name);

            Console.WriteLine("Stats:");
            foreach (DM.Constants.StatID stat in league.ScoringStats)
            {
                Console.WriteLine(" {0} ({1})", DM.Constants.StatIDToString(stat), stat);
            }
            Console.WriteLine();

            Console.WriteLine("Positions:");
            foreach (DM.Position position in league.PositionCounts.Keys)
            {
                Console.WriteLine("{0} {1}", league.PositionCounts[position], DM.Positions.ToShortString(position));
            }

            /*
            Console.WriteLine("Teams:");
            foreach (Team team in league.Teams)
            {
                Console.WriteLine(" {0} ({1}) - {2} ({3})", team.Name, team.Key, team.ManagerName, team.ManagerEmail);
                foreach (string playerKey in team.PlayerKeys)
                {
                    Player player = league.Players[playerKey];
                    Console.WriteLine("   {0}", player.Name);
                    foreach (int statId in player.Stats.Keys)
                    {
                        StatDefinition stat = league.Stats[statId];
                        Console.WriteLine("      {0}: {1}", stat.Name, player.Stats[statId]);
                    }
                }
            }

            foreach (StatDefinition stat in league.Stats.Values)
            {
                List<Player> players = new List<Player>();
                foreach (Player player in league.Players.Values)
                {
                    if (player.IsBatter == (stat.PositionType == "B"))
                    {
                        players.Add(player);
                    }
                }

                players.Sort((a, b) => a.StatValues[stat.Id].CompareTo(b.StatValues[stat.Id]));
                Console.WriteLine(stat.Name);

                int count = 0;
                foreach (Player player in players)
                {
                    Console.WriteLine("  {0}: {1}", player.Name, player.Stats[stat.Id]);
                    if (count++ == 15)
                    {
                        break;
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine("Standings:");
            foreach (Standings standings in league.TeamStandings.OrderBy(s => s.Rank))
            {
                Team team = league.Teams.FirstOrDefault(t => t.Key == standings.TeamKey);
                float total = 0;
                float current;
                foreach (string pointValue in standings.Points.Values)
                {
                    if (float.TryParse(pointValue, out current))
                    {
                        total += current;
                    }
                }

                Console.WriteLine("{0}: {1} with {2} points", standings.Rank, team.Name, total);
            }
            */
        }
    }
}
