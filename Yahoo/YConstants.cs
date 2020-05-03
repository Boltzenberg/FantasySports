using FantasySports.DataModels;
using System;
using System.Collections.Generic;

namespace Yahoo
{
    public static class YConstants
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
            public const string ThirdBase = "3B";
            public const string CornerInfield = "CI";
            public const string MiddleInfield = "MI";
            public const string Outfield = "OF";
            public const string Pitcher = "P";
            public const string StartingPitcher = "SP";
            public const string ReliefPitcher = "RP";
            public const string Utility = "Util";
            public const string Bench = "BN";
            public const string DisabledList = "DL";
            public const string NotActive = "NA";

            public static Position PositionFromYahooPosition(string yahooPosition)
            {
                switch (yahooPosition)
                {
                    case Catcher: return Position.C;
                    case FirstBase: return Position.B1;
                    case SecondBase: return Position.B2;
                    case ThirdBase: return Position.B3;
                    case Shortstop: return Position.SS;
                    case CornerInfield: return Position.CI;
                    case MiddleInfield: return Position.MI;
                    case Outfield: return Position.OF;
                    case Utility: return Position.Util;
                    case Pitcher: return Position.P;
                    case Bench: return Position.BN;
                    case DisabledList: return Position.DL;
                    case NotActive: return Position.NA;
                    default: throw new NotSupportedException("Position " + yahooPosition + " not supported yet!");
                }
            }
        }

        public static class Stats
        {
            public const int B_OnBasePercentage = 4;
            public const int B_Runs = 7;
            public const int B_HomeRuns = 12;
            public const int B_RunsBattedIn = 13;
            public const int B_StolenBases = 16;
            public const int P_EarnedRunAverage = 26;
            public const int P_WHIP = 27;
            public const int P_Wins = 28;
            public const int P_Saves = 32;
            public const int P_Strikeouts = 42;
            public const int P_InningsPitched = 50;
            public const int B_HitsAndAtBats = 60;

            public static Constants.StatID ScoringStatIdFromYahooStatId(int yahooStatID)
            {
                switch (yahooStatID)
                {
                    case B_OnBasePercentage: return Constants.StatID.B_OnBasePercentage;
                    case B_Runs: return Constants.StatID.B_Runs;
                    case B_HomeRuns: return Constants.StatID.B_HomeRuns;
                    case B_RunsBattedIn: return Constants.StatID.B_RunsBattedIn;
                    case B_StolenBases: return Constants.StatID.B_StolenBases;
                    case P_EarnedRunAverage: return Constants.StatID.P_EarnedRunAverage;
                    case P_WHIP: return Constants.StatID.P_WHIP;
                    case P_Wins: return Constants.StatID.P_Wins;
                    case P_Saves: return Constants.StatID.P_Saves;
                    case P_Strikeouts: return Constants.StatID.P_Strikeouts;
                    default: throw new NotSupportedException("Yahoo Stat ID " + yahooStatID + " not supported yet!");
                }
            }

            public static void MapYahooStatDictionaryToDataModelStatDictionary(Dictionary<int, string> yahooStats, Dictionary<Constants.StatID, float> dmStats)
            {
                foreach (int yahooStatID in yahooStats.Keys)
                {
                    switch (yahooStatID)
                    {
                        case B_OnBasePercentage: OneToOneMapping(yahooStats[yahooStatID], Constants.StatID.B_OnBasePercentage, dmStats); break;
                        case B_Runs: OneToOneMapping(yahooStats[yahooStatID], Constants.StatID.B_Runs, dmStats); break;
                        case B_HomeRuns: OneToOneMapping(yahooStats[yahooStatID], Constants.StatID.B_HomeRuns, dmStats); break;
                        case B_RunsBattedIn: OneToOneMapping(yahooStats[yahooStatID], Constants.StatID.B_RunsBattedIn, dmStats); break;
                        case B_StolenBases: OneToOneMapping(yahooStats[yahooStatID], Constants.StatID.B_StolenBases, dmStats); break;
                        case P_EarnedRunAverage: OneToOneMapping(yahooStats[yahooStatID], Constants.StatID.P_EarnedRunAverage, dmStats); break;
                        case P_WHIP: OneToOneMapping(yahooStats[yahooStatID], Constants.StatID.P_WHIP, dmStats); break;
                        case P_Wins: OneToOneMapping(yahooStats[yahooStatID], Constants.StatID.P_Wins, dmStats); break;
                        case P_Saves: OneToOneMapping(yahooStats[yahooStatID], Constants.StatID.P_Saves, dmStats); break;
                        case P_Strikeouts: OneToOneMapping(yahooStats[yahooStatID], Constants.StatID.P_Strikeouts, dmStats); break;
                        case P_InningsPitched: HandleInningsPitched(yahooStats[yahooStatID], dmStats); break;
                        case B_HitsAndAtBats: HandleHitsAndAtBats(yahooStats[yahooStatID], dmStats); break;
                        default: throw new NotSupportedException("Yahoo stat " + yahooStatID + " isn't supported yet!");
                    }
                }

                Constants.SetCalculatedStats(dmStats);
            }

            private static void OneToOneMapping(string yahooValue, Constants.StatID key, Dictionary<Constants.StatID, float> dmStats)
            {
                float statValue;
                if (float.TryParse(yahooValue, out statValue))
                {
                    dmStats[key] = statValue;
                }
            }

            private static void HandleInningsPitched(string yahooValue, Dictionary<Constants.StatID, float> dmStats)
            {
                // yahooValue is number of innings dot outs in the inning, like 3.2 is 3 full innings and 2 other outs
                string[] parts = yahooValue.Split('.');
                int outsRecorded = 0;
                int inningsPitched;
                if (int.TryParse(parts[0], out inningsPitched))
                {
                    outsRecorded = inningsPitched * 3;
                }

                if (parts.Length == 2)
                {
                    int additionalOutsRecorded;
                    if (int.TryParse(parts[1], out additionalOutsRecorded))
                    {
                        outsRecorded += additionalOutsRecorded;
                    }
                }

                dmStats[Constants.StatID.P_OutsRecorded] = outsRecorded;
            }

            private static void HandleHitsAndAtBats(string yahooValue, Dictionary<Constants.StatID, float> dmStats)
            {
                // yahooValue is "<hits>/<at bats>"
                // -/- is valid for no data
                string[] parts = yahooValue.Split('/');

                if (parts.Length == 2)
                {
                    int hits;
                    int atBats;
                    if (int.TryParse(parts[0], out hits) && int.TryParse(parts[1], out atBats))
                    {
                        dmStats[Constants.StatID.B_Hits] = hits;
                        dmStats[Constants.StatID.B_AtBats] = atBats;
                    }
                }
            }
        }
    }
}
