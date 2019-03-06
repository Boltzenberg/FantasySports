using FantasyAuction.DataModel;

namespace FantasyAlgorithms
{
    public static class Extractors
    {
        public static int? ExtractBatterRuns(IPlayer player)
        {
            Batter b = player as Batter;
            if (b == null)
            {
                return null;
            }

            return b.ProjectedR;
        }

        public static int? ExtractBatterHomeRuns(IPlayer player)
        {
            Batter b = player as Batter;
            if (b == null)
            {
                return null;
            }

            return b.ProjectedHR;
        }

        public static int? ExtractBatterRBIs(IPlayer player)
        {
            Batter b = player as Batter;
            if (b == null)
            {
                return null;
            }

            return b.ProjectedRBI;
        }

        public static int? ExtractBatterSteals(IPlayer player)
        {
            Batter b = player as Batter;
            if (b == null)
            {
                return null;
            }

            return b.ProjectedSB;
        }

        public static int? ExtractBatterHitsPlusWalks(IPlayer player)
        {
            Batter b = player as Batter;
            if (b == null)
            {
                return null;
            }

            return b.ProjectedWalksPlusHits;
        }

        public static int? ExtractBatterAtBats(IPlayer player)
        {
            Batter b = player as Batter;
            if (b == null)
            {
                return null;
            }

            return b.ProjectedAB;
        }

        public static int? ExtractPitcherWins(IPlayer player)
        {
            Pitcher p = player as Pitcher;
            if (p == null)
            {
                return null;
            }

            return p.ProjectedW;
        }

        public static int? ExtractPitcherSaves(IPlayer player)
        {
            Pitcher p = player as Pitcher;
            if (p == null)
            {
                return null;
            }

            return p.ProjectedSV;
        }

        public static int? ExtractPitcherStrikeouts(IPlayer player)
        {
            Pitcher p = player as Pitcher;
            if (p == null)
            {
                return null;
            }

            return p.ProjectedK;
        }

        public static int? ExtractPitcherEarnedRuns(IPlayer player)
        {
            Pitcher p = player as Pitcher;
            if (p == null)
            {
                return null;
            }

            return p.ProjectedER;
        }

        public static int? ExtractPitcherOutsRecorded(IPlayer player)
        {
            Pitcher p = player as Pitcher;
            if (p == null)
            {
                return null;
            }

            return p.ProjectedOutsRecorded;
        }

        public static int? ExtractPitcherWalksPlusHits(IPlayer player)
        {
            Pitcher p = player as Pitcher;
            if (p == null)
            {
                return null;
            }

            return (p.ProjectedHits + p.ProjectedWalks);
        }
    }
}
