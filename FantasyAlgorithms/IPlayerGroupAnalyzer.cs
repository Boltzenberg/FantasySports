using FantasyAlgorithms.DataModel;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace FantasyAlgorithms
{
    public interface IPlayerGroupAnalyzer
    {
        PlayerGroupAnalysis Analyze(string groupDescription, IStatExtractor statExtractor, IEnumerable<IPlayer> playerSet, IPlayer selectedPlayer, Func<IPlayer, Brush> playerToBrush);

        PlayerGroupAnalysis Analyze(string groupDescription, IStatExtractor statExtractor, IEnumerable<IPlayer> playerSet, IPlayer selectedPlayer);

        int GetPercentile(IStatExtractor statExtractor, IEnumerable<IPlayer> playerSet, IPlayer selectedPlayer);
    }
}
