using System.Collections.Generic;

namespace FantasyAlgorithms.DataModel
{
    public interface IPlayer
    {
        string Name { get; }
        int ESPNId { get; }
        string YahooId { get; }
        string FantasyTeam { get; set; }
        string AssumedFantasyTeam { get; set; }
        float AuctionPrice { get; set; }
        string GetHTML();
        IEnumerable<Position> Positions { get; }
        string Status { get; }
        string ProfilePicture { get; }

        void Update(ESPNProjections.IPlayer batter);
        void Update(YahooFantasySports.DataModel.Player player);
    }
}
