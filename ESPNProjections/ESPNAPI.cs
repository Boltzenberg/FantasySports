using System.CodeDom;
using System.IO;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;

namespace ESPNProjections
{
    public static class ESPNAPI
    {
        private static string Url = "http://fantasy.espn.com/apis/v3/games/flb/seasons/2020/segments/0/leaguedefaults/1?view=kona_player_info";
        private static string Host = "fantasy.espn.com";
        private static string Pitchers = "13,14,15";
        private static string Batters = "0,1,2,3,4,5,6,7,8,9,10,11,12,19";
        private static int PlayerCount = 300;
        private static string FantasyFilterFormatString = "{{\"players\":{{\"filterStatsForExternalIds\":{{\"value\":[2020]}},\"filterSlotIds\":{{\"value\":[{0}]}},\"filterStatsForSourceIds\":{{\"value\":[1]}},\"sortAppliedStatTotal\":{{\"sortPriority\":2,\"sortAsc\":false,\"value\":\"102020\"}},\"sortDraftRanks\":{{\"sortPriority\":3,\"sortAsc\":true,\"value\":\"STANDARD\"}},\"sortPercOwned\":{{\"sortPriority\":4,\"sortAsc\":false}},\"limit\":{1},\"offset\":0,\"filterRanksForScoringPeriodIds\":{{\"value\":[1]}},\"filterRanksForRankTypes\":{{\"value\":[\"STANDARD\"]}},\"filterStatsForTopScoringPeriodIds\":{{\"value\":5,\"additionalValue\":[\"002020\",\"102020\",\"002019\",\"012020\",\"022020\",\"032020\",\"042020\",\"010002020\"]}}}}}}";
        private const string Batter2020CacheFile = @"C:\Users\jon_r\OneDrive\Documents\2020ESPNBatters.json";
        private const string Pitcher2020CacheFile = @"C:\Users\jon_r\OneDrive\Documents\2020ESPNPitchers.json";

        public static string LoadBatterProjections()
        {
            if (File.Exists(Batter2020CacheFile))
            {
                return File.ReadAllText(Batter2020CacheFile);
            }

            HttpWebRequest req = HttpWebRequest.CreateHttp(Url);
            req.Host = Host;
            req.KeepAlive = true;
            req.Accept = "application/json";
            req.Headers["X-Fantasy-Filter"] = string.Format(FantasyFilterFormatString, Batters, PlayerCount);
            using (HttpWebResponse res = req.GetResponse() as HttpWebResponse)
            {
                if (res.StatusCode == HttpStatusCode.OK)
                {
                    using (StreamReader reader = new StreamReader(res.GetResponseStream()))
                    {
                        string result = reader.ReadToEnd();
                        File.WriteAllText(Batter2020CacheFile, result);
                        return result;
                    }
                }

                return string.Empty;
            }
        }

        public static string LoadPitcherProjections()
        {
            if (File.Exists(Pitcher2020CacheFile))
            {
                return File.ReadAllText(Pitcher2020CacheFile);
            }

            HttpWebRequest req = HttpWebRequest.CreateHttp(Url);
            req.Host = Host;
            req.KeepAlive = true;
            req.Accept = "application/json";
            req.Headers["X-Fantasy-Filter"] = string.Format(FantasyFilterFormatString, Pitchers, PlayerCount);
            using (HttpWebResponse res = req.GetResponse() as HttpWebResponse)
            {
                if (res.StatusCode == HttpStatusCode.OK)
                {
                    using (StreamReader reader = new StreamReader(res.GetResponseStream()))
                    {
                        string result = reader.ReadToEnd();
                        File.WriteAllText(Pitcher2020CacheFile, result);
                        return result;
                    }
                }

                return string.Empty;
            }
        }
    }
}
