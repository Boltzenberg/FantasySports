using FantasySports.DataModels.DataProcessing;
using System.Collections.Generic;

namespace FantasySports.DataModels
{
    public class Team
    {
        public string Name { get; set; }
        public string Owner { get; set; }
        public float Budget { get; set; }
        public IEnumerable<int> Roster { get; private set; }
        public Dictionary<int, float> PlayerToAuctionPrice { get; private set; }

        public Team(string name, string owner)
        {
            this.Name = name;
            this.Owner = owner;
            this.Roster = new List<int>();
            this.Budget = 0.0f;
            this.PlayerToAuctionPrice = new Dictionary<int, float>();
        }
    }
}
