﻿using System.Collections.Generic;

namespace NirSiteLib.DataModel
{
    public class RosteredPlayer
    {
        public string Name { get; private set; }
        public string YahooId { get; private set; }
        public string MLBTeamName { get; private set; }
        public List<string> Positions { get; private set; }
        public float Price { get; private set; }

        public RosteredPlayer(string name, string yahooId, string mlbTeamName, string positions, float price)
        {
            this.Name = FixName(name);
            this.YahooId = yahooId;
            this.MLBTeamName = mlbTeamName;
            this.Positions = new List<string>(positions.Split(','));
            this.Price = price;
        }

        private static string FixName(string name)
        {
            switch (name)
            {
                case "Shohei Ohtani (Pitcher)": return "Shohei Ohtani";
                default: return name;
            }
        }
    }
}
