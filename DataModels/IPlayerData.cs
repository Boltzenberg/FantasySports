using System.Collections.Generic;

namespace FantasySports.DataModels
{
    public interface IPlayerData
    {
        string DisplayName { get; }

        Constants.StatSource StatSource { get; }

        string SourceID { get; }

        List<Position> Positions { get; }
        
        Dictionary<Constants.StatID, float> Stats { get; }

        string Outlook { get; }
    }
}
