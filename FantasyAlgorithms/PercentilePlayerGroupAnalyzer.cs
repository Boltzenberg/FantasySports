using FantasyAlgorithms.DataModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyAlgorithms
{
    public class PercentilePlayerGroupAnalyzer : IPlayerGroupAnalyzer
    {
        public PercentilePlayerGroupAnalyzer()
        {}

        public PlayerGroupAnalysis Analyze(string groupDescription, IStatExtractor statExtractor, IEnumerable<IPlayer> playerSet, IPlayer selectedPlayer, Func<IPlayer, Brush> playerToBrush)
        {
            List<Tuple<IStatValue, IPlayer>> playersWithStats = new List<Tuple<IStatValue, IPlayer>>();
            float barMin = float.MaxValue;
            float barMax = float.MinValue;

            foreach (IPlayer player in playerSet)
            {
                IStatValue statValue = statExtractor.Extract(player);
                if (statValue != null && !float.IsInfinity(statValue.Value))
                {
                    playersWithStats.Add(new Tuple<IStatValue, IPlayer>(statValue, player));
                    barMin = Math.Min(barMin, statValue.Value);
                    barMax = Math.Max(barMax, statValue.Value);
                }
            }

            if (statExtractor.MoreIsBetter)
            {
                playersWithStats.Sort((Tuple<IStatValue, IPlayer> x, Tuple<IStatValue, IPlayer> y) => y.Item1.Value.CompareTo(x.Item1.Value));
            }
            else
            {
                playersWithStats.Sort((Tuple<IStatValue, IPlayer> x, Tuple<IStatValue, IPlayer> y) => x.Item1.Value.CompareTo(y.Item1.Value));
            }

            const int barWidth = 3;
            const int imgHeight = 480;

            int playerPercentile = -1;
            float playerStatValue = float.MinValue;
            int imgWidth = barWidth * playersWithStats.Count;
            Bitmap graph = new Bitmap(imgWidth, imgHeight);
            using (Graphics g = Graphics.FromImage(graph))
            {
                for (int i = 0; i < playersWithStats.Count; i++)
                {
                    float barHeightPercent = playersWithStats[i].Item1.Value / barMax;
                    int barHeight = (int)(barHeightPercent * imgHeight);
                    Rectangle rect = new Rectangle(i * barWidth, imgHeight - barHeight, barWidth, barHeight);
                    Brush brush = playerToBrush(playersWithStats[i].Item2);
                    if (playersWithStats[i].Item2 == selectedPlayer)
                    {
                        playerPercentile = 100 - ((i * 100) / playersWithStats.Count);
                        playerStatValue = playersWithStats[i].Item1.Value;
                    }
                    g.FillRectangle(brush, rect);
                }
            }

            return new PlayerGroupAnalysis(groupDescription, statExtractor.StatName, graph, playersWithStats.Count, barMin, barMax, selectedPlayer, playerStatValue, playerPercentile);
        }

        public PlayerGroupAnalysis Analyze(string groupDescription, IStatExtractor statExtractor, IEnumerable<IPlayer> playerSet, IPlayer selectedPlayer)
        {
            return this.Analyze(groupDescription, statExtractor, playerSet, selectedPlayer, p => p == selectedPlayer ? Brushes.Green : Brushes.Red);
        }
    }
}