using FantasyAlgorithms.DataModel;

namespace FantasyAlgorithms
{
    public static class Extractors
    {
        public static int? ExtractRank(IPlayer player)
        {
            string strRank = string.Empty;
            Batter b = player as Batter;
            if (b == null)
            {
                Pitcher p = player as Pitcher;
                if (p == null)
                {
                    return null;
                }
                else
                {
                    strRank = p.Rank;
                }
            }
            else
            {
                strRank = b.Rank;
            }

            int rank;
            if (!int.TryParse(strRank, out rank))
            {
                return null;
            }

            return rank;
        }

        public static int? ExtractTotalRanking(IPlayer player)
        {
            string strTotalRanking = string.Empty;
            Batter b = player as Batter;
            if (b == null)
            {
                Pitcher p = player as Pitcher;
                if (p == null)
                {
                    return null;
                }
                else
                {
                    strTotalRanking = p.TotalRanking;
                }
            }
            else
            {
                strTotalRanking = b.TotalRanking;
            }

            int totalRanking;
            if (!int.TryParse(strTotalRanking, out totalRanking))
            {
                return null;
            }

            return totalRanking;
        }

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

        public static int? ExtractBatterHits(IPlayer player)
        {
            Batter b = player as Batter;
            if (b == null)
            {
                return null;
            }

            return b.ProjectedH;
        }

        public static int? ExtractBatterWalks(IPlayer player)
        {
            Batter b = player as Batter;
            if (b == null)
            {
                return null;
            }

            return b.ProjectedBB;
        }

        public static int? ExtractBatterHitsPlusWalks(IPlayer player)
        {
            Batter b = player as Batter;
            if (b == null)
            {
                return null;
            }

            return b.ProjectedBB + b.ProjectedH;
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

        public static int? ExtractPitcherDecisions(IPlayer player)
        {
            Pitcher p = player as Pitcher;
            if (p == null)
            {
                return null;
            }

            return p.ProjectedW + p.ProjectedL;
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

        public static int? ExtractPitcherQualityStarts(IPlayer player)
        {
            Pitcher p = player as Pitcher;
            if (p == null)
            {
                return null;
            }

            return p.ProjectedQS;
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

        public static int? ExtractPitcherSavesPlusHolds(IPlayer player)
        {
            Pitcher p = player as Pitcher;
            if (p == null)
            {
                return null;
            }

            return p.ProjectedSV + p.ProjectedHld;
        }

        public static int? ExtractPitcherHolds(IPlayer player)
        {
            Pitcher p = player as Pitcher;
            if (p == null)
            {
                return null;
            }

            return p.ProjectedHld;
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
