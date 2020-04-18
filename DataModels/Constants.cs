using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
