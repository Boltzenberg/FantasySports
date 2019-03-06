using FantasyAuction.DataModel;
using System;
using System.Collections.Generic;

namespace FantasyAlgorithms
{
    public class CountingStatExtractor : IStatExtractor
    {
        public CountingStatExtractor(string statName, bool moreIsBetter, Func<IPlayer, int?> extractFunc)
        {
            this.StatName = statName;
            this.MoreIsBetter = moreIsBetter;
            this.Extract = p => DoExtract(extractFunc(p));
        }

        public string StatName { get; private set; }

        public bool MoreIsBetter { get; private set; }

        public Func<IPlayer, IStatValue> Extract { get; private set; }

        public IStatValue Aggregate(IEnumerable<IStatValue> values)
        {
            int total = 0;
            foreach (IStatValue value in values)
            {
                total += ((CountingStatValue)value).IntValue;
            }

            return new CountingStatValue(total);
        }

        private IStatValue DoExtract(int? value)
        {
            if (value == null)
            {
                return null;
            }

            return new CountingStatValue(value.Value);
        }

        private class CountingStatValue : IStatValue
        {
            public float Value { get; private set; }
            public int IntValue { get; private set; }

            public CountingStatValue(int val)
            {
                this.Value = val;
                this.IntValue = val;
            }
        }
    }
}
