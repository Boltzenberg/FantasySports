using FantasySports.DataModels.DataProcessing;
using System.Collections.Generic;

namespace FantasySports.DataModels
{
    public interface ILeague
    {
        string Name { get; }
        List<Team> Teams { get; }
        List<Constants.StatID> ScoringStats { get; }
        List<Constants.StatID> SupportingStats { get; }
        IEnumerable<int> AllPlayers { get; }
    }
}
