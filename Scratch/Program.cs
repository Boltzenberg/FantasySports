using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using YahooFantasySports;
using YahooFantasySports.DataModel;

namespace Scratch
{
    class Program
    {
        static async Task InitializeAuthManager()
        {
            Console.WriteLine("Browse to: {0}", AuthManager.GetAuthUrl());
            Console.Write("Enter Authorization Code: ");
            string authorizationCode = Console.ReadLine();
            await AuthManager.Instance.InitializeAuthorizationAsync(authorizationCode);
        }

        static async Task GetLeagueStats()
        {
            HttpWebRequest req = WebRequest.CreateHttp(UrlGen.LeagueSettingsUrl(Constants.Leagues.Rounders2018));
            await AuthManager.Instance.AuthorizeRequestAsync(req);

            using (HttpWebResponse res = await req.GetResponseAsync() as HttpWebResponse)
            using (StreamReader responseBody = new StreamReader(res.GetResponseStream()))
            {
                Console.WriteLine(await responseBody.ReadToEndAsync());
            }
        }

        static void Main(string[] args)
        {
            InitializeAuthManager().Wait();
            GetLeagueStats().Wait();

            List<Player> players = Player.GetAllPlayers(Constants.Leagues.Rounders2018).Result;
            foreach (Player player in players)
            {
                Console.WriteLine(player.Name);
            }
        }
    }
}
