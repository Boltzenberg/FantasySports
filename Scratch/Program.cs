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
            List<Player> players = Player.GetAllPlayers(league.Key).Result;
            foreach (Player player in players)
            {
                Console.WriteLine(player.Name);
            }
        }
    }
}
