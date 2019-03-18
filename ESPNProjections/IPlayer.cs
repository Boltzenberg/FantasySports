using System.Collections.Generic;

namespace ESPNProjections
{
    public interface IPlayer
    {
        string FullName { get; }

        int Id { get; }

        string Rank { get; }

        List<int> Positions { get; }

        Dictionary<string, string> Stats { get; }

        string SeasonOutlook { get; }
    }
}
