using System.Collections.Generic;

namespace ESPNProjections
{
    public static class Constants
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
