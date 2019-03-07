namespace FantasyAlgorithms.DataModel
{
    public interface IPlayer
    {
        string Name { get; }
        string FantasyTeam { get; set; }
        float AuctionPrice { get; set; }
        string GetHTML();
    }
}
