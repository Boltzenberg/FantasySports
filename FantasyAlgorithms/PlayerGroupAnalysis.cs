using FantasyAlgorithms.DataModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyAlgorithms
{
    public class PlayerGroupAnalysis : IDisposable
    {
        public string GroupDescription { get; private set; }

        public string Stat { get; private set; }

        public Image Graph { get; private set; }

        public int DataPoints { get; private set; }

        public float MinStatValue { get; private set; }

        public float MaxStatValue { get; private set; }

        public IPlayer SelectedPlayer { get; private set; }

        public float PlayerStatValue { get; private set; }

        public int PlayerPercentile { get; private set; }

        public PlayerGroupAnalysis(string groupDescription, string stat, Image graph, int dataPoints, float minStatValue, float maxStatValue, IPlayer selectedPlayer, float playerStatValue, int playerPercentile)
        {
            this.GroupDescription = groupDescription;
            this.Stat = stat;
            this.Graph = graph;
            this.DataPoints = dataPoints;
            this.MinStatValue = minStatValue;
            this.MaxStatValue = maxStatValue;
            this.SelectedPlayer = selectedPlayer;
            this.PlayerStatValue = playerStatValue;
            this.PlayerPercentile = playerPercentile;
        }

        public void Dispose()
        {
            if (this.Graph != null)
            {
                this.Graph.Dispose();
                this.Graph = null;
            }
        }
    }
}
