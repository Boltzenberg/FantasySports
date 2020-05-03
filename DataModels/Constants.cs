using System.Collections.Generic;
using System.Linq;

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
            P_WalksAndHits,
            P_EarnedRunAverage,
            P_WHIP,
            P_EarnedRuns,
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
                case StatID.P_EarnedRunAverage: return "Earned Run Average";
                case StatID.P_WHIP: return "WHIP";
                case StatID.P_EarnedRuns: return "Earned Runs";
                case StatID.P_WalksAndHits: return "Walks and Hits";
                case StatID.P_Strikeouts: return "Strikeouts";
                case StatID.P_Wins: return "Wins";
                case StatID.P_Losses: return "Losses";
                case StatID.P_Saves: return "Saves";
                case StatID.P_Holds: return "Holds";

                default: return "Unknown";
            }
        }

        public static void SetCalculatedStats(Dictionary<Constants.StatID, float> stats)
        {
            // Pitcher calculated stats
            // Expects:
            // Outs Recorded
            if (stats.ContainsKey(StatID.P_OutsRecorded))
            {
                if (stats.ContainsKey(StatID.P_EarnedRunAverage) && !stats.ContainsKey(StatID.P_EarnedRuns))
                {
                    stats[StatID.P_EarnedRuns] = stats[StatID.P_EarnedRunAverage] * stats[StatID.P_OutsRecorded] / 27;
                }

                if (stats.ContainsKey(StatID.P_WHIP) && !stats.ContainsKey(StatID.P_WalksAndHits))
                {
                    stats[StatID.P_WalksAndHits] = stats[StatID.P_WHIP] * stats[StatID.P_OutsRecorded] / 3;
                }
            }

            if (stats.ContainsKey(StatID.B_AtBats) && stats.ContainsKey(StatID.B_Hits) && stats.ContainsKey(StatID.B_Walks) && !stats.ContainsKey(StatID.B_OnBasePercentage))
            {
                stats[StatID.B_OnBasePercentage] = (stats[StatID.B_Hits] + stats[StatID.B_Walks]) / (stats[StatID.B_AtBats] + stats[StatID.B_Walks]);
            }

            if (stats.ContainsKey(StatID.B_AtBats) && stats.ContainsKey(StatID.B_Hits) && stats.ContainsKey(StatID.B_OnBasePercentage) && !stats.ContainsKey(StatID.B_Walks))
            {
                // OBP == (hits + walks) / (at bats + walks)
                // OBP * (at bats + walks) == (hits + walks)
                // (OBP * at bats) + (OBP * walks) == hits + walks
                // (OBP * at bats) + (OBP * walks) - hits == walks
                // (OBP * at bats) - hits == walks - (OBP * walks)
                // (OBP * at bats) - hits == (1 - OBP) * walks
                // ((OBP * at bats) - hits) / (1 - OBP) == walks
                float OBP = stats[StatID.B_OnBasePercentage];
                float hits = stats[StatID.B_Hits];
                float atBats = stats[StatID.B_AtBats];
                stats[StatID.B_Walks] = ((OBP * atBats) - hits) / (1 - OBP);
            }
        }
    }
}
