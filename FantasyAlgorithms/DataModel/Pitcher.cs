using Newtonsoft.Json;
using System;
using System.Text;

namespace FantasyAlgorithms.DataModel
{
    public class Pitcher : IPlayer
    {
        public string Name { get; set; }
        public bool IsSP { get; set; }
        public bool IsRP { get; set; }

        public float AuctionPrice { get; set; }
        public string FantasyTeam { get; set; }
        public string AssumedFantasyTeam { get; set; }

        public int ProjectedOutsRecorded { get; set; }
        public int ProjectedW { get; set; }
        public int ProjectedSV { get; set; }
        public int ProjectedK { get; set; }
        public int ProjectedHits { get; set; }
        public int ProjectedWalks { get; set; }
        public int ProjectedER { get; set; }
        public string SeasonOutlook { get; set; }

        public float ProjectedIP { get; set; }
        public float ProjectedERA { get; set; }
        public float ProjectedWHIP { get; set; }

        public override string ToString()
        {
            return this.Name;
        }

        public string GetHTML()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<HTML><BODY><TABLE BORDER='1'>");
            sb.AppendFormat("<TR><TD>Name</TD><TD>{0}</TD></TR>", this.Name);
            sb.AppendFormat("<TR><TD>Is SP</TD><TD>{0}</TD></TR>", this.IsSP ? "Yes" : "No");
            sb.AppendFormat("<TR><TD>Is RP</TD><TD>{0}</TD></TR>", this.IsRP ? "Yes" : "No");
            sb.AppendFormat("<TR><TD>Projected Innings Pitched</TD><TD>{0}</TD></TR>", this.ProjectedIP);
            sb.AppendFormat("<TR><TD>Projected Wins</TD><TD>{0}</TD></TR>", this.ProjectedW);
            sb.AppendFormat("<TR><TD>Projected Saves</TD><TD>{0}</TD></TR>", this.ProjectedSV);
            sb.AppendFormat("<TR><TD>Projected Strikeouts</TD><TD>{0}</TD></TR>", this.ProjectedK);
            sb.AppendFormat("<TR><TD>Projected ERA</TD><TD>{0}</TD></TR>", this.ProjectedERA);
            sb.AppendFormat("<TR><TD>Projected WHIP</TD><TD>{0}</TD></TR>", this.ProjectedWHIP);
            sb.AppendFormat("<TR><TD>Projected Outs Recorded</TD><TD>{0}</TD></TR>", this.ProjectedOutsRecorded);
            sb.AppendFormat("<TR><TD>Projected Hits</TD><TD>{0}</TD></TR>", this.ProjectedHits);
            sb.AppendFormat("<TR><TD>Projected Walks</TD><TD>{0}</TD></TR>", this.ProjectedWalks);
            sb.AppendFormat("<TR><TD>Projected Earned Runs</TD><TD>{0}</TD></TR>", this.ProjectedER);
            sb.AppendFormat("<TR><TD>Season Outlook</TD><TD>{0}</TD></TR>", this.SeasonOutlook);
            sb.AppendLine("</TABLE></BODY></HTML>");
            return sb.ToString();
        }

        public static Pitcher Create(ESPNProjections.Pitcher pitcher)
        {
            Pitcher p = new Pitcher();
            p.AuctionPrice = 0;
            p.FantasyTeam = string.Empty;
            p.Update(pitcher);
            return p;
        }

        public void Update(ESPNProjections.Pitcher pitcher)
        {
            this.Name = pitcher.FullName;
            this.IsSP = pitcher.Positions.Contains(ESPNProjections.Constants.Positions.SP);
            this.IsRP = pitcher.Positions.Contains(ESPNProjections.Constants.Positions.RP);
            this.ProjectedOutsRecorded = GetStat(pitcher.Stats[ESPNProjections.Constants.Stats.Pitchers.OutsRecorded], 0);
            this.ProjectedW = GetStat(pitcher.Stats[ESPNProjections.Constants.Stats.Pitchers.W], 0);
            this.ProjectedSV = GetStat(pitcher.Stats[ESPNProjections.Constants.Stats.Pitchers.SV], 0);
            this.ProjectedK = GetStat(pitcher.Stats[ESPNProjections.Constants.Stats.Pitchers.K], 0);
            this.ProjectedHits = GetStat(pitcher.Stats[ESPNProjections.Constants.Stats.Pitchers.H], 0);
            this.ProjectedWalks = GetStat(pitcher.Stats[ESPNProjections.Constants.Stats.Pitchers.BB], 0);
            this.ProjectedER = GetStat(pitcher.Stats[ESPNProjections.Constants.Stats.Pitchers.ER], 0);
            this.ProjectedIP = pitcher.InningsPitched;
            this.ProjectedERA = pitcher.ERA;
            this.ProjectedWHIP = pitcher.WHIP;
            this.SeasonOutlook = pitcher.SeasonOutlook;
        }

        public static Pitcher[] Load(string serialized)
        {
            return JsonConvert.DeserializeObject<Pitcher[]>(serialized);
        }

        public static string Serialize(Pitcher[] pitchers)
        {
            return JsonConvert.SerializeObject(pitchers);
        }

        private static int GetStat(string stat, int defaultValue)
        {
            int val;
            if (!Int32.TryParse(stat, out val))
            {
                return defaultValue;
            }

            return val;
        }
    }
}
