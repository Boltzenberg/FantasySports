using System;
using System.Collections.Generic;
using System.Xml;

namespace YahooFantasySports.DataModel
{
    public class Standings
    {
        public string TeamKey { get; }
        public IReadOnlyDictionary<string, string> Stats { get; }
        public IReadOnlyDictionary<string, string> Points { get; }
        public int Rank { get; }

        internal static Standings Create(XmlNode node, NSMgr nsmgr)
        {
            string teamKey = nsmgr.GetValue(node, "team_key");

            string rankStr = nsmgr.GetValue(node, "team_standings", "rank");
            int rank;
            if (!Int32.TryParse(rankStr, out rank))
            {
                return null;
            }

            Dictionary<string, string> stats = new Dictionary<string, string>();
            foreach (XmlNode stat in node.SelectNodes(nsmgr.GetXPath("team_stats", "stats", "stat"), nsmgr))
            {
                string key = nsmgr.GetValue(stat, "stat_id");
                string value = nsmgr.GetValue(stat, "value");
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                {
                    stats[key] = value;
                }
            }

            Dictionary<string, string> points = new Dictionary<string, string>();
            foreach (XmlNode stat in node.SelectNodes(nsmgr.GetXPath("team_points", "stats", "stat"), nsmgr))
            {
                string key = nsmgr.GetValue(stat, "stat_id");
                string value = nsmgr.GetValue(stat, "value");
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                {
                    points[key] = value;
                }
            }

            return new Standings(teamKey, stats, points, rank);
        }

        private Standings(string teamKey, IReadOnlyDictionary<string, string> stats, IReadOnlyDictionary<string, string> points, int rank)
        {
            this.TeamKey = teamKey;
            this.Stats = stats;
            this.Points = points;
            this.Rank = rank;
        }
    }
}
