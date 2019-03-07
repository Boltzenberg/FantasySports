using FantasyAlgorithms.DataModel;
using System;
using System.Collections.Generic;

namespace FantasyAlgorithms
{
    public class RatioStatExtractor : IStatExtractor
    {
        private Func<int, int, float> ratio;

        public RatioStatExtractor(string statName, bool moreIsBetter, Func<IPlayer, int?> extractNumerator, Func<IPlayer, int?> extractDenominator, Func<int, int, float> ratio)
        {
            this.StatName = statName;
            this.MoreIsBetter = moreIsBetter;
            this.Extract = p => DoExtract(extractNumerator(p), extractDenominator(p));
            this.ratio = ratio;
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

            return new RatioStatValue(totalNumerator, totalDenominator, this.ratio);
        }

        private IStatValue DoExtract(int? numerator, int? denominator)
        {
            if (numerator == null || denominator == null)
            {
                return null;
            }

            return new RatioStatValue(numerator.Value, denominator.Value, this.ratio);
        }

        private class RatioStatValue : IStatValue
        {
            public float Value { get; private set; }
            public int Numerator { get; set; }
            public int Denominator { get; set; }

            public RatioStatValue(int numerator, int denominator, Func<int, int, float> ratio)
            {
                this.Numerator = numerator;
                this.Denominator = denominator;
                this.Value = ratio(this.Numerator, this.Denominator);
            }
        }
    }
}
