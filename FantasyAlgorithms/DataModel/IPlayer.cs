using System.Collections.Generic;

namespace FantasyAlgorithms.DataModel
{
    public interface IPlayer
    {
        string Name { get; }
        string FantasyTeam { get; set; }
        string AssumedFantasyTeam { get; set; }
        float AuctionPrice { get; set; }
        string GetHTML();
        IEnumerable<Position> Positions { get; }
    }
}
