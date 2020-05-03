using System;

namespace Yahoo
{
    public static class UrlGen
    {
        private static string BaseUrl = "https://fantasysports.yahooapis.com/fantasy/v2";

        public static Uri LeagueUrl(string leagueId)
        {
            return new Uri(string.Format("{0}/league/{1}", BaseUrl, leagueId));
        }

        public static Uri LeagueSettingsUrl(string leagueId)
        {
            return new Uri(string.Format("{0}/settings", LeagueUrl(leagueId)));
        }

        public static Uri PaginatedPlayers(string leagueId, int start)
        {
            return new Uri(string.Format("{0}/players;start={1}/stats", LeagueUrl(leagueId), start));
        }

        public static Uri TeamsWithRostersUrl(string leagueId)
        {
            return new Uri(string.Format("{0}/league/{1}/teams/roster", BaseUrl, leagueId));
        }

        public static Uri LeagueStandings(string leagueId)
        {
            return new Uri(string.Format("{0}/league/{1}/standings", BaseUrl, leagueId));
        }
    }
}
