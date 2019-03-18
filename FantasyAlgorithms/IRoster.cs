using FantasyAlgorithms.DataModel;
using System.Collections.Generic;

namespace FantasyAlgorithms
{
    public interface IRoster
    {
        string TeamName { get; }

        IEnumerable<IPlayer> Players { get; }

        IDictionary<string, float> Stats { get; }

        IDictionary<string, float> Points { get; }
    }
}
