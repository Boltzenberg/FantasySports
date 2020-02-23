using FantasyAlgorithms.DataModel;
using System.Collections.Generic;

namespace FantasyAlgorithms
{
    public interface IPlayerGroupAnalyzer
    {
        PlayerGroupAnalysis Analyze(string groupDescription, IStatExtractor statExtractor, IEnumerable<IPlayer> playerSet, IPlayer selectedPlayer);
    }
}
