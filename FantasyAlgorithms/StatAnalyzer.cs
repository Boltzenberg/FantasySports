using FantasyAlgorithms.DataModel;
using System.Collections.Generic;

namespace FantasyAlgorithms
{
    public class StatAnalyzer
    {
        private IStatExtractor extractor;

        public string StatName { get { return this.extractor.StatName; } }

        public StatAnalyzer(IStatExtractor extractor)
        {
            this.extractor = extractor;
        }

        public IEnumerable<PlayerAnalysis> Analyze(IEnumerable<IPlayer> players)
        {
            int total = 0;
            List<PlayerAnalysis> analyses = new List<PlayerAnalysis>();
            foreach (IPlayer player in players)
            {
                IStatValue value = this.extractor.Extract(player);
                if (value != null)
                {
                    analyses.Add(new PlayerAnalysis(player, this.extractor.StatName, value.Value));
                    total += value.CountableValue;
                }
            }

            if (this.extractor.MoreIsBetter)
            {
                analyses.Sort((x, y) => y.RawValue.CompareTo(x.RawValue));
            }
            else
            {
                analyses.Sort((x, y) => x.RawValue.CompareTo(y.RawValue));
            }

            for (int i = 0; i < analyses.Count; i++)
            {
                analyses[i].Rank = i + 1;
                analyses[i].Percentage = analyses[i].RawValue / (float)total;
                yield return analyses[i];
            }
        }
    }
}
