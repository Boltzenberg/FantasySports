using System.Collections.Generic;

namespace ESPNProjections
{
    public static class Constants
    {
        internal static class Files
        {
            internal static List<string> Batters = new List<string>()
            {
                "2019BatterProjections01.json",
                "2019BatterProjections02.json",
                "2019BatterProjections03.json",
                "2019BatterProjections04.json",
                "2019BatterProjections05.json",
                "2019BatterProjections06.json",
            };

            internal static List<string> Pitchers = new List<string>()
            {
                "2019PitcherProjections01.json",
                "2019PitcherProjections02.json",
                "2019PitcherProjections03.json",
                "2019PitcherProjections04.json",
                "2019PitcherProjections05.json",
                "2019PitcherProjections06.json",
            };
        }

        public static class Stats
        {
            public static class Batters
            {
                public const string AB = "0";
                public const string R = "20";
                public const string H = "1";
                public const string HR = "5";
                public const string RBI = "21";
                public const string SB = "23";
                public const string AVG = "2";
                public const string SLG = "9";
                public const string OBP = "17";
                public static List<string> All = new List<string>()
                {
                    AB, R, H, HR, RBI, SB, AVG, SLG, OBP
                };

                public static string ToString(string stat)
                {
                    switch (stat)
                    {
                        case AB: return "AB";
                        case R: return "R";
                        case H: return "H";
                        case HR: return "HR";
                        case RBI: return "RBI";
                        case SB: return "SB";
                        case AVG: return "AVG";
                        case SLG: return "SLG";
                        case OBP: return "OBP";
                    }
                    return "Unknown";
                }
            }

            public static class Pitchers
            {
                public const string OutsRecorded = "34";
                public const string H = "37";
                public const string ER = "45";
                public const string BB = "39";
                public const string K = "48";
                public const string W = "53";
                public const string SV = "57";
                public static List<string> All = new List<string>()
                {
                    OutsRecorded, H, ER, BB, K, W, SV
                };

                public static string ToString(string stat)
                {
                    switch (stat)
                    {
                        case OutsRecorded: return "Outs Recorded";
                        case H: return "H";
                        case ER: return "ER";
                        case BB: return "BB";
                        case K: return "K";
                        case W: return "W";
                        case SV: return "SV";
                    }
                    return "Unknown";
                }
            }
        }

        public static class Positions
        {
            public const int DH = 11;
            public const int C = 0;
            public const int B1 = 1;
            public const int B2 = 2;
            public const int B3 = 3;
            public const int SS = 4;
            public const int OF = 5;
            public const int SP = 14;
            public const int RP = 15;

            public static string ToString(int position)
            {
                switch (position)
                {
                    case DH: return "DH";
                    case C: return "C";
                    case B1: return "1B";
                    case B2: return "2B";
                    case B3: return "3B";
                    case SS: return "SS";
                    case OF: return "OF";
                    case SP: return "SP";
                    case RP: return "RP";
                }

                return "Unknown";
            }
        }
    }
}
