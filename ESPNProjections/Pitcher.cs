using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ESPNProjections
{
    public class Pitcher
    {
        public static IEnumerable<Pitcher> Load()
        {
            foreach (string fileName in Constants.Files.Pitchers)
            {
                JObject file = JObject.Parse(File.ReadAllText(fileName));
                JArray players = (JArray)file["players"];
                foreach (JToken player in players)
                {
                    if (player["player"]["stats"].Count() == 0)
                    {
                        continue;
                    }

                    Pitcher p = new Pitcher();
                    p.FullName = (string)player["player"]["fullName"];
                    p.Rank = (string)player["player"]["draftRanksByRankType"]["STANDARD"]["rank"];
                    p.SeasonOutlook = (string)player["player"]["seasonOutlook"];
                    p.Positions = new List<int>(((JArray)player["player"]["eligibleSlots"]).Select(s => (int)s).ToArray());
                    p.Stats = new Dictionary<string, string>();
                    foreach (string stat in Constants.Stats.Pitchers.All)
                    {
                        p.Stats[stat] = (string)player["player"]["stats"][0]["stats"][stat];
                    }
                    yield return p;
                }
            }
        }

        public string FullName { get; private set; }
        public string Rank { get; private set; }
        public List<int> Positions { get; private set; }
        public Dictionary<string, string> Stats { get; private set; }
        public string SeasonOutlook { get; private set; }

        public float InningsPitched
        {
            get
            {
                int outs;
                if (int.TryParse(this.Stats[Constants.Stats.Pitchers.OutsRecorded], out outs))
                {
                    return outs / 3 + ((float)(outs % 3) / 10);
                }
                return 0f;
            }
        }

        public float ERA
        {
            get
            {
                int er;
                if (int.TryParse(this.Stats[Constants.Stats.Pitchers.ER], out er))
                {
                    return er / (float)(InningsPitched / 9);
                }

                return 0f;
            }
        }

        public float WHIP
        {
            get
            {
                int h, bb;
                if (int.TryParse(this.Stats[Constants.Stats.Pitchers.H], out h) &&
                    int.TryParse(this.Stats[Constants.Stats.Pitchers.BB], out bb))
                {
                    return (h + bb) / InningsPitched;
                }

                return 0f;
            }
        }

        private Pitcher()
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
            foreach (string stat in Constants.Stats.Pitchers.All)
            {
                sb.AppendFormat("{0}: {1}, ", Constants.Stats.Pitchers.ToString(stat), this.Stats[stat]);
            }
            sb.Length -= 2;
            sb.AppendLine();
            sb.AppendLine(this.SeasonOutlook);
            return sb.ToString();
        }

        public static string CSVHeader()
        {
            return "Name,Outs Recorded,Innings Pitched,Hits,Walks,Earned Runs,Wins,Saves,ERA,WHIP";
        }

        public string ToCSV()
        {
            return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                this.FullName,
                this.Stats[Constants.Stats.Pitchers.OutsRecorded],
                this.InningsPitched,
                this.Stats[Constants.Stats.Pitchers.H],
                this.Stats[Constants.Stats.Pitchers.BB],
                this.Stats[Constants.Stats.Pitchers.ER],
                this.Stats[Constants.Stats.Pitchers.W],
                this.Stats[Constants.Stats.Pitchers.SV],
                this.ERA,
                this.WHIP);
        }
    }
}
