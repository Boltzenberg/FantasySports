using FantasySports.DataModels;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ESPNProjections
{
    public static class ESPNConstants
    {
        public static class Stats
        {
            public static class Batters
            {
                public const string AB = "0";
                public const string R = "20";
                public const string H = "1";
                public const string BB = "10";
                public const string HR = "5";
                public const string RBI = "21";
                public const string SB = "23";
                public static List<string> All = new List<string>()
                {
                    AB, R, H, BB, HR, RBI, SB
                };
            }

            public static class Pitchers
            {
                public const string OutsRecorded = "34";
                public const string H = "37";
                public const string ER = "45";
                public const string BB = "39";
                public const string K = "48";
                public const string W = "53";
                public const string L = "54";
                public const string SV = "57";
                public const string Hld = "60";
                public static List<string> All = new List<string>()
                {
                    OutsRecorded, H, ER, BB, K, W, L, SV, Hld
                };
            }

            public static void MapESPNStatDictionaryToDataModelStatDictionary(Dictionary<string, string> espnStats, Dictionary<Constants.StatID, float> dmStats)
            {
                bool handledHitsAndWalks = false;
                foreach (string espnStatID in espnStats.Keys)
                {
                    switch (espnStatID)
                    {
                        case Batters.AB: OneToOneMapping(espnStats[espnStatID], Constants.StatID.B_AtBats, dmStats); break;
                        case Batters.H: OneToOneMapping(espnStats[espnStatID], Constants.StatID.B_Hits, dmStats); break;
                        case Batters.HR: OneToOneMapping(espnStats[espnStatID], Constants.StatID.B_HomeRuns, dmStats); break;
                        case Batters.R: OneToOneMapping(espnStats[espnStatID], Constants.StatID.B_Runs, dmStats); break;
                        case Batters.RBI: OneToOneMapping(espnStats[espnStatID], Constants.StatID.B_RunsBattedIn, dmStats); break;
                        case Batters.SB: OneToOneMapping(espnStats[espnStatID], Constants.StatID.B_StolenBases, dmStats); break;
                        case Batters.BB: OneToOneMapping(espnStats[espnStatID], Constants.StatID.B_Walks, dmStats); break;
                        case Pitchers.ER: OneToOneMapping(espnStats[espnStatID], Constants.StatID.P_EarnedRuns, dmStats); break;
                        case Pitchers.Hld: OneToOneMapping(espnStats[espnStatID], Constants.StatID.P_Holds, dmStats); break;
                        case Pitchers.L: OneToOneMapping(espnStats[espnStatID], Constants.StatID.P_Losses, dmStats); break;
                        case Pitchers.OutsRecorded: OneToOneMapping(espnStats[espnStatID], Constants.StatID.P_OutsRecorded, dmStats); break;
                        case Pitchers.SV: OneToOneMapping(espnStats[espnStatID], Constants.StatID.P_Saves, dmStats); break;
                        case Pitchers.K: OneToOneMapping(espnStats[espnStatID], Constants.StatID.P_Strikeouts, dmStats); break;
                        case Pitchers.W: OneToOneMapping(espnStats[espnStatID], Constants.StatID.P_Wins, dmStats); break;
                        case Pitchers.H: { if (!handledHitsAndWalks) { handledHitsAndWalks = true; HandleHitsAndWalks(espnStats, dmStats); } break; }
                        case Pitchers.BB: { if (!handledHitsAndWalks) { handledHitsAndWalks = true; HandleHitsAndWalks(espnStats, dmStats); } break; }
                        default: throw new NotSupportedException("ESPN stat " + espnStatID + " isn't supported yet!");
                    }
                }

                Constants.SetCalculatedStats(dmStats);
            }

            private static void OneToOneMapping(string espnValue, Constants.StatID key, Dictionary<Constants.StatID, float> dmStats)
            {
                float statValue;
                if (float.TryParse(espnValue, out statValue))
                {
                    dmStats[key] = statValue;
                }
            }

            private static void HandleHitsAndWalks(Dictionary<string, string> espnStats, Dictionary<Constants.StatID, float> dmStats)
            {
                string strHits, strWalks;
                if (espnStats.TryGetValue(Pitchers.H, out strHits) && espnStats.TryGetValue(Pitchers.BB, out strWalks))
                {
                    int hits, walks;
                    if (int.TryParse(strHits, out hits) && int.TryParse(strWalks, out walks))
                    {
                        dmStats[Constants.StatID.P_WalksAndHits] = hits + walks;
                    }
                }
            }
        }

        public static class Positions
        {
            public const int DH = 12;
            public const int C = 0;
            public const int B1 = 1;
            public const int B2 = 2;
            public const int B3 = 3;
            public const int SS = 4;
            public const int OF = 5;
            public const int P = 13;
            public const int SP = 14;
            public const int RP = 15;

            public static string ToString(int position)
            {
                switch (position)
                {
                    case DH: return "DH";
                    case C:  return "C";
                    case B1: return "1B";
                    case B2: return "2B";
                    case B3: return "3B";
                    case SS: return "SS";
                    case OF: return "OF";
                    case P:  return "P";
                    case SP: return "SP";
                    case RP: return "RP";
                }

                return "Unknown";
            }
        }
    }
}
