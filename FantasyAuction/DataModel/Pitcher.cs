using Newtonsoft.Json;
using System;
using System.Text;

namespace FantasyAuction.DataModel
{
    public class Pitcher : IPlayer
    {
        public string Name { get; set; }
        public bool IsSP { get; set; }
        public bool IsRP { get; set; }

        public float AuctionPrice { get; set; }
        public string FantasyTeam { get; set; }

        public float ProjectedIP { get; set; }
        public int ProjectedW { get; set; }
        public int ProjectedSV { get; set; }
        public int ProjectedK { get; set; }
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
            sb.AppendLine("</TABLE></BODY></HTML>");
            return sb.ToString();
        }

        public static Pitcher Create(ESPNProjections.Pitcher pitcher)
        {
            Pitcher p = new Pitcher();
            p.Name = pitcher.FullName;
            p.IsSP = pitcher.Positions.Contains(ESPNProjections.Constants.Positions.SP);
            p.IsRP = pitcher.Positions.Contains(ESPNProjections.Constants.Positions.RP);
            p.AuctionPrice = 0;
            p.FantasyTeam = string.Empty;
            p.ProjectedIP = pitcher.InningsPitched;
            p.ProjectedW = GetStat(pitcher.Stats[ESPNProjections.Constants.Stats.Pitchers.W], 0);
            p.ProjectedSV = GetStat(pitcher.Stats[ESPNProjections.Constants.Stats.Pitchers.SV], 0);
            p.ProjectedK = GetStat(pitcher.Stats[ESPNProjections.Constants.Stats.Pitchers.K], 0);
            p.ProjectedERA = pitcher.ERA;
            p.ProjectedWHIP = pitcher.WHIP;
            return p;
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
