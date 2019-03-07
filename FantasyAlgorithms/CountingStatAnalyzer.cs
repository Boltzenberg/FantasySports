using FantasyAlgorithms.DataModel;
using System;
using System.Collections.Generic;

namespace FantasyAlgorithms
{
    public class CountingStatAnalyzer : IPlayerAnalyzer
    {
        private string stat;
        private int rosterablePlayers;
        private IEnumerable<IPlayer> players;
        private Func<IPlayer, int> playerStatExtractor;
        private Func<IPlayer, int> totalStatExtractor;
        private float targetTotal;
        private List<Tuple<int, PlayerAnalysis>> analysis;

        public CountingStatAnalyzer(string stat, int rosterablePlayers, IEnumerable<IPlayer> players, Func<IPlayer, int> playerStatExtractor, Func<IPlayer, int> totalStatExtractor)
        {
            this.stat = stat;
            this.rosterablePlayers = rosterablePlayers;
            this.players = players;
            this.playerStatExtractor = playerStatExtractor;
            this.totalStatExtractor = totalStatExtractor;

            int countOfPlayersWithTotalStat = 0;
            int runningTotal = 0;
            this.analysis = new List<Tuple<int, PlayerAnalysis>>();
            foreach (IPlayer player in this.players)
            {
                int playerStat = this.playerStatExtractor(player);
                int totalStat = this.totalStatExtractor(player);
                if (totalStat > 0)
                {
                    countOfPlayersWithTotalStat++;
                    runningTotal += totalStat;
                }
                this.analysis.Add(new Tuple<int, PlayerAnalysis>(playerStat, new PlayerAnalysis(player, this.stat, playerStat)));
            }

            float avgPerPlayer = (float)runningTotal / countOfPlayersWithTotalStat;
            this.targetTotal = avgPerPlayer * this.rosterablePlayers;

            this.analysis.Sort((x, y) => y.Item1.CompareTo(x.Item1));
            for (int i = 0; i < this.analysis.Count; i++)
            {
                this.analysis[i].Item2.Rank = i + 1;
                this.analysis[i].Item2.Percentage = (float)this.analysis[i].Item1 / this.targetTotal;
            }
        }

        public string Name { get { return this.stat; } }

        public IEnumerable<PlayerAnalysis> Analyze()
        {
            foreach (var tuple in this.analysis)
            {
                yield return tuple.Item2;
            }
        }
    }
}
