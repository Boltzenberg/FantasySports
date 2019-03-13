using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ESPNProjections
{
    public class Batter : IPlayer
    {
        public static IEnumerable<Batter> Load()
        {
            foreach (string fileName in Constants.Files.Batters)
            {
                JObject file = JObject.Parse(File.ReadAllText(fileName));
                JArray players = (JArray)file["players"];
                foreach (JToken player in players)
                {
                    if (player["player"]["stats"].Count() == 0)
                    {
                        continue;
                    }

                    Batter b = new Batter();
                    b.FullName = (string)player["player"]["fullName"];
                    b.Rank = (string)player["player"]["draftRanksByRankType"]["STANDARD"]["rank"];
                    b.SeasonOutlook = (string)player["player"]["seasonOutlook"];
                    b.Positions = new List<int>(((JArray)player["player"]["eligibleSlots"]).Select(s => (int)s).ToArray());
                    b.Stats = new Dictionary<string, string>();
                    foreach (string stat in Constants.Stats.Batters.All)
                    {
                        b.Stats[stat] = (string)player["player"]["stats"][0]["stats"][stat];
                    }
                    yield return b;
                }
            }
        }

        public string FullName { get; private set; }
        public string Rank { get; private set; }
        public List<int> Positions { get; private set; }
        public Dictionary<string, string> Stats { get; private set; }
        public string SeasonOutlook { get; private set; }

        private Batter()
        {}

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}: {1}{2}", this.FullName, this.Rank, Environment.NewLine);
            sb.Append("Positions: ");
            foreach (int position in this.Positions)
            {
                sb.AppendFormat("{0}, ", Constants.Positions.ToString(position));
            }
            sb.Length -= 2;
            sb.AppendLine();
            sb.Append("Stats: ");
            foreach (string stat in Constants.Stats.Batters.All)
            {
                sb.AppendFormat("{0}: {1}, ", Constants.Stats.Batters.ToString(stat), this.Stats[stat]);
            }
            sb.Length -= 2;
            sb.AppendLine();
            sb.AppendLine(this.SeasonOutlook);
            return sb.ToString();
        }

        public static string CSVHeader()
        {
            return "Name,At Bats,Runs,Home Runs,Runs Batted In,Stolen Bases,On-Base Percentage";
        }

        public string ToCSV()
        {
            return string.Format("{0},{1},{2},{3},{4},{5},{6}",
                this.FullName,
                this.Stats[Constants.Stats.Batters.AB],
                this.Stats[Constants.Stats.Batters.R],
                this.Stats[Constants.Stats.Batters.HR],
                this.Stats[Constants.Stats.Batters.RBI],
                this.Stats[Constants.Stats.Batters.SB],
                this.Stats[Constants.Stats.Batters.OBP]);
        }
    }
}
