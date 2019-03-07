using FantasyAuction.DataModel;
using System;
using System.Collections.Generic;

namespace FantasyAlgorithms
{
    public class RatioStatAnalyzer : IPlayerAnalyzer
    {
        private string stat;
        private bool moreIsBetter;
        private IEnumerable<IPlayer> players;
        private Func<IPlayer, int> numeratorExtractor;
        private Func<IPlayer, int> denominatorExtractor;
        private int totalNumerator;
        private int totalDenominator;
        private List<Tuple<int, int, PlayerAnalysis>> analysis;

        public RatioStatAnalyzer(string stat, int rosterablePlayers, bool moreIsBetter, IEnumerable<IPlayer> players, Func<IPlayer, int> numeratorExtractor, Func<IPlayer, int> denominatorExtractor)
        {
            this.stat = stat;
            this.moreIsBetter = moreIsBetter;
            this.players = players;
            this.numeratorExtractor = numeratorExtractor;
            this.denominatorExtractor = denominatorExtractor;

            this.totalNumerator = 0;
            this.totalDenominator = 0;
            this.analysis = new List<Tuple<int, int, PlayerAnalysis>>();
            foreach (IPlayer player in this.players)
            {
                int playerNumerator = this.numeratorExtractor(player);
                int playerDenominator = this.denominatorExtractor(player);
                this.totalNumerator += playerNumerator;
                this.totalDenominator += playerDenominator;
                this.analysis.Add(new Tuple<int, int, PlayerAnalysis>(playerNumerator, playerDenominator, new PlayerAnalysis(player, this.stat, (float)playerNumerator / (float)playerDenominator)));
            }

            this.analysis.Sort(this.Compare);

            for (int i = 0; i < this.analysis.Count; i++)
            {
                this.analysis[i].Item3.Rank = i + 1;
                this.analysis[i].Item3.Percentage = (float)this.analysis[i].Item1 / this.totalNumerator;
            }
        }

        private int Compare(Tuple<int, int, PlayerAnalysis> x, Tuple<int, int, PlayerAnalysis> y)
        {
            float xVal = x.Item1 * ((float)x.Item2 / this.totalDenominator);
            float yVal = y.Item1 * ((float)y.Item2 / this.totalDenominator);
            if (this.moreIsBetter)
            {
                return yVal.CompareTo(xVal);
            }
            else
            {
                return xVal.CompareTo(yVal);
            }
        }

        public string Name { get { return this.stat; } }

        public IEnumerable<PlayerAnalysis> Analyze()
        {
            foreach (var tuple in this.analysis)
            {
                yield return tuple.Item3;
            }
        }
    }
}
