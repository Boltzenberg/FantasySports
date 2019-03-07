using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YahooFantasySports;
using YahooFantasySports.DataModel;
using YahooFantasySports.Services;

namespace Scratch
{
    class Program
    {
        static async Task InitializeAuthManager()
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

        static async Task GetLeagueStats()
        {
            string stats = await Http.GetRawDataAsync(UrlGen.LeagueSettingsUrl(Constants.Leagues.Rounders2018));
            Console.WriteLine(stats);
        }

        static void Main(string[] args)
        {
            UpdateTeams();
        }

        static void UpdateTeams()
        {
            const string file = "C:\\users\\jonro\\documents\\FantasyLeague2019.json";
            FantasyAlgorithms.DataModel.League league = FantasyAlgorithms.DataModel.League.Load(file);
            league.Teams = new FantasyAlgorithms.DataModel.Team[10];
            league.Teams[0] = new FantasyAlgorithms.DataModel.Team() { Name = "Mitch's Bitches", Owner = "Nir Modiano", Budget = 208.29f };
            league.Teams[1] = new FantasyAlgorithms.DataModel.Team() { Name = "Peanut Tossers", Owner = "Josh Kornblit", Budget = 216.83f };
            league.Teams[2] = new FantasyAlgorithms.DataModel.Team() { Name = "Kirby SMASH", Owner = "Bobby Ronaghy", Budget = 192.37f };
            league.Teams[3] = new FantasyAlgorithms.DataModel.Team() { Name = "See You In McCourt", Owner = "Eric Rudin", Budget = 205.72f };
            league.Teams[4] = new FantasyAlgorithms.DataModel.Team() { Name = "LeClerking for LeJudge", Owner = "Macus", Budget = 206.26f };
            league.Teams[5] = new FantasyAlgorithms.DataModel.Team() { Name = "Machado Chop House", Owner = "Michael Sneag", Budget = 189.77f };
            league.Teams[6] = new FantasyAlgorithms.DataModel.Team() { Name = "Jews on First", Owner = "Steve Lesser", Budget = 215.71f };
            league.Teams[7] = new FantasyAlgorithms.DataModel.Team() { Name = "Putz on Second", Owner = "Jon Rosenberg", Budget = 216.09f };
            league.Teams[8] = new FantasyAlgorithms.DataModel.Team() { Name = "Cool WHIP", Owner = "Jeff Selman", Budget = 242.71f };
            league.Teams[9] = new FantasyAlgorithms.DataModel.Team() { Name = "The S stand for OPS", Owner = "Jared Hersh", Budget = 206.25f };
            league.Save(file);
        }

        static void ESPNStuff()
        {
            Console.WriteLine(ESPNProjections.Batter.CSVHeader());
            foreach (var b in ESPNProjections.Batter.Load())
            {
                Console.WriteLine(b.ToCSV());
            }
            Console.WriteLine();

            Console.WriteLine(ESPNProjections.Pitcher.CSVHeader());
            foreach (var p in ESPNProjections.Pitcher.Load())
            {
                Console.WriteLine(p.ToCSV());
            }
        }

        static void YahooStuff()
        {
            InitializeAuthManager().Wait();

            League league = League.Create(Constants.Leagues.Rounders2018).Result;
            Console.WriteLine("{0} ({1})", league.Name, league.Key);

            Console.WriteLine("Stats:");
            foreach (StatDefinition stat in league.Stats.Values)
            {
                Console.WriteLine(" {0} ({1})", stat.Name, stat.Id);
            }

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
        }
    }
}
