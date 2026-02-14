using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooFantasySports
{
    public static class Constants
    {
        internal static class OAuth
        {
            internal const string AppID = "aAsbTS48";
            internal const string ClientID = "dj0yJmk9VFZKWXZDeEJFTjNGJmQ9WVdrOVlVRnpZbFJUTkRnbWNHbzlNQS0tJnM9Y29uc3VtZXJzZWNyZXQmeD04ZQ--";
            internal const string ClientSecret = "146e3ec4bb840f618131a2509da26b1a7092731e";
        }

        public static class Leagues
        {
            public const string Rounders2018 = "378.l.5418";
            public const string Rounders2019 = "388.l.21375";
            public const string Rounders2020 = "398.l.19492";
            public const string Rounders2021 = "404.l.18043";
            public const string Rounders2022 = "412.l.44139";
            public const string Rounders2023 = "422.l.34352";
            public const string Rounders2024 = "431.l.14487";
            public const string Rounders2025 = "458.l.25419";
            public const string Rounders2026 = "458.l.25419";
            public const string CrossCountryRivals2019 = "388.l.41352";
        }

        public static class Positions
        {
            public static readonly HashSet<string> Individual = new HashSet<string>()
            {
                "C", "1B", "2B", "SS", "3B", "OF", "P", "SP", "RP", "Util"
            };

            public const string Catcher = "C";
            public const string FirstBase = "1B";
            public const string SecondBase = "2B";
            public const string Shortstop = "SS";
            public const string ThridBase = "3B";
            public const string Outfield = "OF";
            public const string Pitcher = "P";
            public const string StartingPitcher = "SP";
            public const string ReliefPitcher = "RP";
            public const string Utility = "Util";
        }
    }
}
