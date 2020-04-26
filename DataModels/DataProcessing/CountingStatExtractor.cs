using System.Collections.Generic;

namespace FantasySports.DataModels.DataProcessing
{
    public class CountingStatExtractor : IStatExtractor
    {
        public CountingStatExtractor(bool moreIsBetter, Constants.StatID statId)
        {
            this.MoreIsBetter = moreIsBetter;
            this.StatID = statId;
        }

        public Constants.StatID StatID { get; private set; }

        public bool MoreIsBetter { get; private set; }

        public IStatValue Extract(Root root, int playerId, Constants.StatSource statSource)
        {
            Player player;
            if (root.Players.TryGetValue(playerId, out player))
            {
                IPlayerData playerData;
                if (player.PlayerData.TryGetValue(statSource, out playerData))
                {
                    float statValue;
                    if (playerData.Stats.TryGetValue(this.StatID, out statValue))
                    {
                        return new CountingStatValue((int)statValue);
                    }
                }
            }

            return null;
        }

        public IStatValue Aggregate(IEnumerable<IStatValue> values)
        {
            int total = 0;
            foreach (IStatValue value in values)
            {
                total += ((CountingStatValue)value).CountableValue;
            }

            return new CountingStatValue(total);
        }

        private class CountingStatValue : IStatValue
        {
            public float Value { get; private set; }
            public int CountableValue { get; private set; }

            public CountingStatValue(int val)
            {
                this.Value = val;
                this.CountableValue = val;
            }
        }
    }
}
