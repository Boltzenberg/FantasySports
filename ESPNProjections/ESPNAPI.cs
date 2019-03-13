using System.IO;
using System.Net;

namespace ESPNProjections
{
    public static class ESPNAPI
    {
        private static string Url = "http://fantasy.espn.com/apis/v3/games/flb/seasons/2019/segments/0/leaguedefaults/1?view=kona_player_info";
        private static string Host = "fantasy.espn.com";
        private static string Pitchers = "13,14,15";
        private static string Batters = "0,1,2,3,4,5,6,7,8,9,10,11,12,19";
        private static int PlayerCount = 300;
        private static string FantasyFilterFormatString = "{{\"players\":{{\"filterStatsForSplitTypeIds\":{{\"value\":[0]}},\"filterStatsForSourceIds\":{{\"value\":[0,1]}},\"filterStatsForExternalIds\":{{\"value\":[2018,2019]}},\"sortDraftRanks\":{{\"sortPriority\":2,\"sortAsc\":true,\"value\":\"STANDARD\"}},\"sortPercOwned\":{{\"sortPriority\":3,\"sortAsc\":false}},\"filterSlotIds\":{{\"value\":[{0}]}},\"limit\":{1},\"offset\":0,\"filterStatsForTopScoringPeriodIds\":{{\"value\":1,\"additionalValue\":[\"002019\",\"102019\",\"002018\",\"012019\",\"022019\",\"032019\",\"042019\"]}}}}}}";

        public static string LoadBatterProjections()
        {
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
                        return reader.ReadToEnd();
                    }
                }

                return string.Empty;
            }
        }

        public static string LoadPitcherProjections()
        {
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
                        return reader.ReadToEnd();
                    }
                }

                return string.Empty;
            }
        }
    }
}
