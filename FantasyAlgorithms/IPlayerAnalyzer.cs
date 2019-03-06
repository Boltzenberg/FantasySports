using System.Collections.Generic;

namespace FantasyAlgorithms
{
    public interface IPlayerAnalyzer
    {
        string Name { get; }

        IEnumerable<PlayerAnalysis> Analyze();
    }
}
