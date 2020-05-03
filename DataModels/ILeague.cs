using FantasySports.DataModels.DataProcessing;
using System.Collections.Generic;

namespace FantasySports.DataModels
{
    public interface ILeague
    {
        string Name { get; }

        List<Team> Teams { get; }

        List<Constants.StatID> ScoringStats { get; }

        Dictionary<Position, int> PositionCounts { get; } // how many of each position make up a roster

        IEnumerable<int> AllPlayers { get; }
    }
}
