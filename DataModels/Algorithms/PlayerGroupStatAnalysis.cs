using FantasySports.DataModels.DataProcessing;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace FantasySports.DataModels.Algorithms
{
    public class PlayerGroupStatAnalysis
    {
        private IEnumerable<int> playerList;
        private HashSet<int> playerSubset;
        private int selectedPlayer;
        private IStatExtractor extractor;
        private Root root;
        private List<Tuple<IStatValue, int>> dataSet;

        public PlayerGroupStatAnalysis(IEnumerable<int> playerList, IStatExtractor extractor, Root root, Constants.StatSource statSource)
            : this(playerList, null, -1, extractor, root, statSource)
        {
        }

        public PlayerGroupStatAnalysis(IEnumerable<int> playerList, int selectedPlayer, IStatExtractor extractor, Root root, Constants.StatSource statSource)
            : this(playerList, null, selectedPlayer, extractor, root, statSource)
        {
        }

        public PlayerGroupStatAnalysis(IEnumerable<int> playerList, IEnumerable<int> playerSubset, IStatExtractor extractor, Root root, Constants.StatSource statSource)
            : this(playerList, playerSubset, -1, extractor, root, statSource)
        {
        }

        public PlayerGroupStatAnalysis(IEnumerable<int> playerList, IEnumerable<int> playerSubset, int selectedPlayer, IStatExtractor extractor, Root root, Constants.StatSource statSource)
        {
            this.playerList = playerList;
            this.extractor = extractor;
            this.root = root;
            this.StatSource = statSource;
            this.selectedPlayer = selectedPlayer;

            if (playerSubset != null)
            {
                this.playerSubset = new HashSet<int>();
                foreach (int playerId in playerSubset)
                {
                    this.playerSubset.Add(playerId);
                }
            }
            else
            {
                this.playerSubset = null;
            }

            this.dataSet = ExtractAnalysisDataSet(this.playerList, this.extractor, this.root, this.StatSource);

            this.DataPoints = this.dataSet.Count;
            this.MinStatValue = float.MaxValue;
            this.MaxStatValue = float.MinValue;
            foreach (Tuple<IStatValue, int> dataItem in this.dataSet)
            {
                this.MinStatValue = Math.Min(this.MinStatValue, dataItem.Item1.Value);
                this.MaxStatValue = Math.Max(this.MaxStatValue, dataItem.Item1.Value);
            }
        }

        public Constants.StatSource StatSource { get; private set; }

        public int DataPoints { get; private set; }

        public float MinStatValue { get; private set; }

        public float MaxStatValue { get; private set; }

        private Image _graph = null;
        public Image Graph
        {
            get
            {
                if (this._graph == null)
                {
                    this._graph = RenderAnalysisDataSetToGraph(this.dataSet, this.MinStatValue, this.MaxStatValue, this.PlayerToBrush);
                }

                return this._graph;
            }
        }

        private Brush PlayerToBrush(int playerId)
        {
            if (playerId == this.selectedPlayer)
            {
                return Brushes.Red;
            }
            else if (this.playerSubset != null && this.playerSubset.Contains(playerId))
            {
                return Brushes.Yellow;
            }

            return Brushes.Blue;
        }

        public static List<Tuple<IStatValue, int>> ExtractAnalysisDataSet(IEnumerable<int> playerList, IStatExtractor extractor, Root root, Constants.StatSource statSource)
        {
            List<Tuple<IStatValue, int>> playersWithStats = new List<Tuple<IStatValue, int>>();

            foreach (int player in playerList)
            {
                IStatValue statValue = extractor.Extract(root, player, statSource);
                if (statValue != null && !float.IsInfinity(statValue.Value))
                {
                    playersWithStats.Add(new Tuple<IStatValue, int>(statValue, player));
                }
            }

            if (extractor.MoreIsBetter)
            {
                playersWithStats.Sort((Tuple<IStatValue, int> x, Tuple<IStatValue, int> y) => y.Item1.Value.CompareTo(x.Item1.Value));
            }
            else
            {
                playersWithStats.Sort((Tuple<IStatValue, int> x, Tuple<IStatValue, int> y) => x.Item1.Value.CompareTo(y.Item1.Value));
            }

            return playersWithStats;
        }

        public static Image RenderAnalysisDataSetToGraph(List<Tuple<IStatValue, int>> dataSet, Func<int, Brush> playerToBrush)
        {
            float barMin = float.MaxValue;
            float barMax = float.MinValue;

            foreach (Tuple<IStatValue, int> dataItem in dataSet)
            {
                barMin = Math.Min(barMin, dataItem.Item1.Value);
                barMax = Math.Max(barMax, dataItem.Item1.Value);
            }

            return RenderAnalysisDataSetToGraph(dataSet, barMin, barMax, playerToBrush);
        }

        public static Image RenderAnalysisDataSetToGraph(List<Tuple<IStatValue, int>> dataSet, float barMin, float barMax, Func<int, Brush> playerToBrush)
        {
            const int barWidth = 3;
            const int imgHeight = 480;

            int imgWidth = barWidth * dataSet.Count;
            Bitmap graph = new Bitmap(imgWidth, imgHeight);
            using (Graphics g = Graphics.FromImage(graph))
            {
                for (int i = 0; i < dataSet.Count; i++)
                {
                    float barHeightPercent = dataSet[i].Item1.Value / barMax;
                    int barHeight = (int)(barHeightPercent * imgHeight);
                    Rectangle rect = new Rectangle(i * barWidth, imgHeight - barHeight, barWidth, barHeight);
                    Brush brush = playerToBrush(dataSet[i].Item2);
                    g.FillRectangle(brush, rect);
                }
            }

            return graph;
        }
    }
}
