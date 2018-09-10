using System;

namespace YahooFantasySports
{
    public static class UrlGen
    {
        private static string BaseUrl = "https://fantasysports.yahooapis.com/fantasy/v2";

        public static Uri LeagueUrl(string leagueId)
        {
            return new Uri(string.Format("{0}/league/{1}", BaseUrl, leagueId));
        }

        public static Uri PaginatedPlayers(string leagueId, int start)
        {
            return new Uri(string.Format("{0}/players;start={1}/stats", LeagueUrl(leagueId), start));
        }
    }
}
