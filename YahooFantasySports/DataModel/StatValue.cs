using System;
using System.Collections.Generic;

namespace YahooFantasySports.DataModel
{
    public class StatValue : IComparable<StatValue>
    {
        public string Raw { get; }
        public int Processed { get; }
        public StatDefinition Stat { get; }

        private static HashSet<int> FloatStats = new HashSet<int>()
        {
            4, // On-base Percentage
            26, // ERA
            27, // WHIP
        };

        private static HashSet<int> RatioStats = new HashSet<int>()
        {
            60, // Hits / At Bats
        };

        private static HashSet<int> InningsStats = new HashSet<int>()
        {
            50, // Innings Pitched
        };

        public static StatValue Create(string raw, StatDefinition stat)
        {
            if (FloatStats.Contains(stat.Id))
            {
                // Multiply by 1000 and go with an int
                if (raw == "-")
                {
                    return new StatValue(raw, 0, stat);
                }

                float f = float.Parse(raw);
                f = f * 1000;
                return new StatValue(raw, (int)f, stat);
            }

            if (RatioStats.Contains(stat.Id))
            {
                // Do the math and multiply by 1000
                if (raw == "-/-")
                {
                    return new StatValue(raw, 0, stat);
                }

                string[] parts = raw.Split('/');
                float n = float.Parse(parts[0]) * 1000;
                float d = float.Parse(parts[1]);
                float v = n / d;
                return new StatValue(raw, (int)v, stat);
            }

            if (InningsStats.Contains(stat.Id))
            {
                if (raw == "-")
                {
                    return new StatValue(raw, 0, stat);
                }

                string[] parts = raw.Split('.');
                int wholeInnings = int.Parse(parts[0]);
                int additionalOuts = int.Parse(parts[1]);
                return new StatValue(raw, (wholeInnings * 3) + additionalOuts, stat);
            }

            // It's already an int!
            if (raw == "-")
            {
                return new StatValue(raw, 0, stat);
            }

            return new StatValue(raw, int.Parse(raw), stat);
        }

        public int CompareTo(StatValue other)
        {
            return this.Stat.MoreIsBetter ? other.Processed.CompareTo(this.Processed) : this.Processed.CompareTo(other.Processed);
        }

        private StatValue(string raw, int processed, StatDefinition stat)
        {
            this.Raw = raw;
            this.Processed = processed;
            this.Stat = stat;
        }
    }
}
