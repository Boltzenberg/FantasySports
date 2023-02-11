using System;
using System.CodeDom;
using System.IO;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;

namespace ESPNProjections
{
    public static class ESPNAPI
    {
        interface IConstants
        {
            string Url { get; }
            string Host { get; }
            string Pitchers { get; }
            string Batters { get; }
            int PlayerCount { get; }
            string FantasyFilterFormatString { get; }
            string BatterCacheFile { get; }
            string PitcherCacheFile { get; }
            string PictureUrlFormatString { get; }
        }

        private class Constants2020 : IConstants
        {
            public string Url { get { return "http://fantasy.espn.com/apis/v3/games/flb/seasons/2020/segments/0/leaguedefaults/1?view=kona_player_info"; } }
            public string Host { get { return "fantasy.espn.com"; } }
            public string Pitchers { get { return "13,14,15"; } }
            public string Batters { get { return "0,1,2,3,4,5,6,7,8,9,10,11,12,19"; } }
            public int PlayerCount { get { return 300; } }
            public string FantasyFilterFormatString { get { return "{{\"players\":{{\"filterStatsForExternalIds\":{{\"value\":[2020]}},\"filterSlotIds\":{{\"value\":[{0}]}},\"filterStatsForSourceIds\":{{\"value\":[1]}},\"sortAppliedStatTotal\":{{\"sortPriority\":2,\"sortAsc\":false,\"value\":\"102020\"}},\"sortDraftRanks\":{{\"sortPriority\":3,\"sortAsc\":true,\"value\":\"STANDARD\"}},\"sortPercOwned\":{{\"sortPriority\":4,\"sortAsc\":false}},\"limit\":{1},\"offset\":0,\"filterRanksForScoringPeriodIds\":{{\"value\":[1]}},\"filterRanksForRankTypes\":{{\"value\":[\"STANDARD\"]}},\"filterStatsForTopScoringPeriodIds\":{{\"value\":5,\"additionalValue\":[\"002020\",\"102020\",\"002019\",\"012020\",\"022020\",\"032020\",\"042020\",\"010002020\"]}}}}}}"; } }
            public string BatterCacheFile { get { return @"C:\Users\jon_r\OneDrive\Documents\2020ESPNBatters.json"; } }
            public string PitcherCacheFile { get { return @"C:\Users\jon_r\OneDrive\Documents\2020ESPNPitchers.json"; } }
            public string PictureUrlFormatString { get { return string.Empty; } }
        }

        private class Constants2021 : IConstants
        {
            public string Url { get { return "http://fantasy.espn.com/apis/v3/games/flb/seasons/2021/segments/0/leaguedefaults/1?view=kona_player_info"; } }
            public string Host { get { return "fantasy.espn.com"; } }
            public string Pitchers { get { return "13,14,15"; } }
            public string Batters { get { return "0,1,2,3,4,5,6,7,8,9,10,11,12,19"; } }
            public int PlayerCount { get { return 300; } }
            public string FantasyFilterFormatString { get { return "{{\"players\":{{\"filterStatsForExternalIds\":{{\"value\":[2021]}},\"filterSlotIds\":{{\"value\":[{0}]}},\"filterStatsForSourceIds\":{{\"value\":[1]}},\"sortAppliedStatTotal\":{{\"sortPriority\":2,\"sortAsc\":false,\"value\":\"102021\"}},\"sortDraftRanks\":{{\"sortPriority\":3,\"sortAsc\":true,\"value\":\"STANDARD\"}},\"sortPercOwned\":{{\"sortPriority\":4,\"sortAsc\":false}},\"limit\":{1},\"offset\":0,\"filterRanksForScoringPeriodIds\":{{\"value\":[1]}},\"filterRanksForRankTypes\":{{\"value\":[\"STANDARD\"]}},\"filterStatsForTopScoringPeriodIds\":{{\"value\":5,\"additionalValue\":[\"002021\",\"102021\",\"002020\",\"012021\",\"022021\",\"032021\",\"042021\",\"010002021\"]}}}}}}"; } }
            public string BatterCacheFile { get { return @"C:\Users\jon_r\OneDrive\Documents\2021ESPNBatters.json"; } }
            public string PitcherCacheFile { get { return @"C:\Users\jon_r\OneDrive\Documents\2021ESPNPitchers.json"; } }
            public string PictureUrlFormatString { get { return string.Empty; } }
        }

        private class Constants2022 : IConstants
        {
            public string Url { get { return "http://fantasy.espn.com/apis/v3/games/flb/seasons/2022/segments/0/leaguedefaults/1?view=kona_player_info"; } }
            public string Host { get { return "fantasy.espn.com"; } }
            public string Pitchers { get { return "13,14,15"; } }
            public string Batters { get { return "0,1,2,3,4,5,6,7,8,9,10,11,12,19"; } }
            public int PlayerCount { get { return 300; } }
            public string FantasyFilterFormatString { get { return "{{\"players\":{{\"filterStatsForExternalIds\":{{\"value\":[2021]}},\"filterSlotIds\":{{\"value\":[{0}]}},\"filterStatsForSourceIds\":{{\"value\":[1]}},\"sortAppliedStatTotal\":{{\"sortPriority\":2,\"sortAsc\":false,\"value\":\"102021\"}},\"sortDraftRanks\":{{\"sortPriority\":3,\"sortAsc\":true,\"value\":\"STANDARD\"}},\"sortPercOwned\":{{\"sortPriority\":4,\"sortAsc\":false}},\"limit\":{1},\"offset\":0,\"filterRanksForScoringPeriodIds\":{{\"value\":[1]}},\"filterRanksForRankTypes\":{{\"value\":[\"STANDARD\"]}},\"filterStatsForTopScoringPeriodIds\":{{\"value\":5,\"additionalValue\":[\"002021\",\"102021\",\"002020\",\"012021\",\"022021\",\"032021\",\"042021\",\"010002021\"]}}}}}}"; } }
            public string BatterCacheFile { get { return @"C:\OneDrive\Documents\2022ESPNBatters.json"; } }
            public string PitcherCacheFile { get { return @"C:\OneDrive\Documents\2022ESPNPitchers.json"; } }
            public string PictureUrlFormatString { get { return string.Empty; } }
        }

        private class Constants2023 : IConstants
        {
            public string Url { get { return "http://fantasy.espn.com/apis/v3/games/flb/seasons/2023/segments/0/leaguedefaults/1?view=kona_player_info"; } }
            public string Host { get { return "fantasy.espn.com"; } }
            public string Pitchers { get { return "13,14,15"; } }
            public string Batters { get { return "0,1,2,3,4,5,6,7,8,9,10,11,12,19"; } }
            public int PlayerCount { get { return 300; } }
            //public string FantasyFilterFormatString { get { return "{{\"players\":{{\"filterStatsForExternalIds\":{{\"value\":[2022,2023]}},\"filterSlotIds\":{{\"value\":[{0}]}},\"filterStatsForSourceIds\":{{\"value\":[0,1]}},\"useFullProjectionTable\":{{\"value\":true}},\"sortAppliedStatTotal\":{{\"sortAsc\":false,\"sortPriority\":3,\"value\":\"102023\"}},\"sortDraftRanks\":{{\"sortPriority\":2,\"sortAsc\":true,\"value\":\"STANDARD\"}},\"sortPercOwned\":{{\"sortPriority\":4,\"sortAsc\":false}},\"limit\":{1},\"filterStatsForTopScoringPeriodIds\":{{\"value\":5,\"additionalValue\":[\"002023\",\"102023\",\"002022\",\"012023\",\"022023\",\"032023\",\"042023\",\"062023\",\"010002023\"]}}}}}}"; } }
            public string FantasyFilterFormatString { get { return "{{\"players\":{{\"filterStatsForExternalIds\":{{\"value\":[2023]}},\"filterSlotIds\":{{\"value\":[{0}]}},\"filterStatsForSourceIds\":{{\"value\":[0,1]}},\"useFullProjectionTable\":{{\"value\":true}},\"sortAppliedStatTotal\":{{\"sortAsc\":false,\"sortPriority\":3,\"value\":\"102023\"}},\"sortDraftRanks\":{{\"sortPriority\":2,\"sortAsc\":true,\"value\":\"STANDARD\"}},\"sortPercOwned\":{{\"sortPriority\":4,\"sortAsc\":false}},\"limit\":{1},\"filterStatsForTopScoringPeriodIds\":{{\"value\":5,\"additionalValue\":[\"002023\",\"102023\",\"012023\",\"022023\",\"032023\",\"042023\",\"062023\",\"010002023\"]}}}}}}"; } }
            public string BatterCacheFile { get { return @"C:\Users\jon_r\OneDrive\Documents\2023ESPNBatters.json"; } }
            public string PitcherCacheFile { get { return @"C:\Users\jon_r\OneDrive\Documents\2023ESPNPitchers.json"; } }
            public string PictureUrlFormatString { get { return "https://a.espncdn.com/combiner/i?img=/i/headshots/mlb/players/full/{0}.png&h=120&w=120&scale=crop"; } }
        }

        private static readonly IConstants Constants = new Constants2023();

        public static string GetPlayerImage(int playerId)
        {
            try
            {
                HttpWebRequest req = HttpWebRequest.CreateHttp(string.Format(Constants.PictureUrlFormatString, playerId));
                req.KeepAlive = true;
                using (HttpWebResponse res = req.GetResponse() as HttpWebResponse)
                {
                    if (res.StatusCode == HttpStatusCode.OK)
                    {
                        using (Stream stm = res.GetResponseStream())
                        {
                            byte[] buffer = new byte[64 * 1024];
                            int cbTotal = 0;
                            int cbRead = 0;
                            do
                            {
                                cbRead = stm.Read(buffer, cbTotal, buffer.Length - cbTotal);
                                cbTotal += cbRead;
                            } while (cbRead > 0);
                            return Convert.ToBase64String(buffer, 0, cbTotal);
                        }
                    }

                    return string.Empty;
                }
            }
            catch (WebException wex)
            {
                return string.Empty;
            }
        }

        public static string LoadBatterProjections()
        {
            if (File.Exists(Constants.BatterCacheFile))
            {
                return File.ReadAllText(Constants.BatterCacheFile);
            }

            HttpWebRequest req = HttpWebRequest.CreateHttp(Constants.Url);
            req.Host = Constants.Host;
            req.KeepAlive = true;
            req.Accept = "application/json";
            req.Headers["X-Fantasy-Filter"] = string.Format(Constants.FantasyFilterFormatString, Constants.Batters, Constants.PlayerCount);
            using (HttpWebResponse res = req.GetResponse() as HttpWebResponse)
            {
                if (res.StatusCode == HttpStatusCode.OK)
                {
                    using (StreamReader reader = new StreamReader(res.GetResponseStream()))
                    {
                        string result = reader.ReadToEnd();
                        File.WriteAllText(Constants.BatterCacheFile, result);
                        return result;
                    }
                }

                return string.Empty;
            }
        }

        public static string LoadPitcherProjections()
        {
            if (File.Exists(Constants.PitcherCacheFile))
            {
                return File.ReadAllText(Constants.PitcherCacheFile);
            }

            HttpWebRequest req = HttpWebRequest.CreateHttp(Constants.Url);
            req.Host = Constants.Host;
            req.KeepAlive = true;
            req.Accept = "application/json";
            req.Headers["X-Fantasy-Filter"] = string.Format(Constants.FantasyFilterFormatString, Constants.Pitchers, Constants.PlayerCount);
            using (HttpWebResponse res = req.GetResponse() as HttpWebResponse)
            {
                if (res.StatusCode == HttpStatusCode.OK)
                {
                    using (StreamReader reader = new StreamReader(res.GetResponseStream()))
                    {
                        string result = reader.ReadToEnd();
                        File.WriteAllText(Constants.PitcherCacheFile, result);
                        return result;
                    }
                }

                return string.Empty;
            }
        }
    }
}
