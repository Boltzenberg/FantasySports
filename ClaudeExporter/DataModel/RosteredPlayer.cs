using System.Collections.Generic;

namespace ClaudeExporter.DataModel
{
    public class Team
    {
        public string Name { get; private set; }
        public string Balance { get; private set; }

        public Team(string name, string balance)
        {
            this.Name = name;
            this.Balance = balance;
        }
    }

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

    public class TeamsTableResult
    {
        public List<string> teams { get; set; }
        public List<RosterRow> rows { get; set; }
    }

    public class RosterRow
    {
        public string rowIndex { get; set; }
        public List<RosterCell> cells { get; set; }
    }

    public class RosterCell
    {
        public string text { get; set; }
        public string href { get; set; }
    }
}
