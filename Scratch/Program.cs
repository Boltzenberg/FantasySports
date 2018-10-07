using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
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
            InitializeAuthManager().Wait();

            League league = League.Create(Constants.Leagues.Rounders2018).Result;
            Console.WriteLine("{0} ({1})", league.Name, league.Key);

            Console.WriteLine("Stats:");
            foreach (StatDefinition stat in league.Stats)
            {
                Console.WriteLine(" {0} ({1})", stat.Name, stat.Id);
            }

            Console.WriteLine("Teams:");
            foreach (Team team in league.Teams)
            {
                Console.WriteLine(" {0} ({1}) - {2} ({3})", team.Name, team.Key, team.ManagerName, team.ManagerEmail);
                foreach (string playerKey in team.PlayerKeys)
                {
                    Console.WriteLine("   {0}", league.Players[playerKey].Name);
                }
            }
        }
    }
}
