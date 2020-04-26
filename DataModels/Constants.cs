using System.Collections.Generic;

namespace FantasySports.DataModels
{
    public static class Constants
    {
        public enum StatSource
        {
            ESPNProjections,
            YahooOngoing,
        }

        public enum StatID : int
        {
            // Batter Stats
            B_AtBats,
            B_Runs,
            B_Hits,
            B_Walks,
            B_HomeRuns,
            B_RunsBattedIn,
            B_StolenBases,
            B_OnBasePercentage,

            // Pitcher Stats
            P_OutsRecorded,
            P_Hits,
            P_EarnedRuns,
            P_Walks,
            P_Strikeouts,
            P_Wins,
            P_Losses,
            P_Saves,
            P_Holds,
        }

        public static string StatIDToString(StatID id)
        {
            switch (id)
            {
                case StatID.B_AtBats: return "At Bats";
                case StatID.B_Runs: return "Runs";
                case StatID.B_Hits: return "Hits";
                case StatID.B_Walks: return "Walks";
                case StatID.B_HomeRuns: return "Home Runs";
                case StatID.B_RunsBattedIn: return "RBIs";
                case StatID.B_StolenBases: return "Steals";
                case StatID.B_OnBasePercentage: return "OBP";

                case StatID.P_OutsRecorded: return "Outs Recorded";
                case StatID.P_Hits: return "Hits";
                case StatID.P_EarnedRuns: return "Earned Runs";
                case StatID.P_Walks: return "Walks";
                case StatID.P_Strikeouts: return "Strikeouts";
                case StatID.P_Wins: return "Wins";
                case StatID.P_Losses: return "Losses";
                case StatID.P_Saves: return "Saves";
                case StatID.P_Holds: return "Holds";

                default: return "Unknown";
            }
        }
    }
}
