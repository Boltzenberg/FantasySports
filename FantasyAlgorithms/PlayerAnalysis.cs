using FantasyAlgorithms.DataModel;

namespace FantasyAlgorithms
{
    public class PlayerAnalysis
    {
        public IPlayer Player { get; private set; }
        public string Stat { get; private set; }
        public float RawValue { get; private set; }
        public int Rank { get; set; }
        public float Percentage { get; set; }

        public PlayerAnalysis(IPlayer player, string stat, float rawValue)
        {
            this.Player = player;
            this.Stat = stat;
            this.RawValue = rawValue;
        }
    }
}
