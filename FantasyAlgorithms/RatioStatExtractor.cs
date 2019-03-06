using FantasyAuction.DataModel;
using System;
using System.Collections.Generic;

namespace FantasyAlgorithms
{
    public class RatioStatExtractor : IStatExtractor
    {
        public RatioStatExtractor(string statName, bool moreIsBetter, Func<IPlayer, int> extractNumerator, Func<IPlayer, int> extractDenominator)
        {
            this.StatName = statName;
            this.MoreIsBetter = moreIsBetter;
            this.Extract = p => new RatioStatValue(extractNumerator(p), extractDenominator(p));
        }

        public string StatName { get; private set; }

        public bool MoreIsBetter { get; private set; }

        public Func<IPlayer, IStatValue> Extract { get; private set; }

        public IStatValue Aggregate(IEnumerable<IStatValue> values)
        {
            int totalNumerator = 0;
            int totalDenominator = 0;
            foreach (IStatValue value in values)
            {
                RatioStatValue r = (RatioStatValue)value;
                totalNumerator += r.Numerator;
                totalDenominator += r.Denominator;
            }

            return new RatioStatValue(totalNumerator, totalDenominator);
        }

        private class RatioStatValue : IStatValue
        {
            public float Value { get; private set; }
            public int Numerator { get; set; }
            public int Denominator { get; set; }

            public RatioStatValue(int numerator, int denominator)
            {
                this.Numerator = numerator;
                this.Denominator = denominator;
                this.Value = (float)this.Numerator / this.Denominator;
            }
        }
    }
}
