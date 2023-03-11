using System.Collections.Generic;

namespace NirSiteLib.DataModel
{
    public class RosteredPlayer
    {
        public string Name { get; private set; }
        public string MLBTeamName { get; private set; }
        public List<string> Positions { get; private set; }
        public float Price { get; private set; }

        public RosteredPlayer(string name, string mlbTeamName, string positions, float price)
        {
            this.Name = name;
            this.MLBTeamName = mlbTeamName;
            this.Positions = new List<string>(positions.Split(','));
            this.Price = price;
        }
    }
}
