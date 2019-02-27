using Newtonsoft.Json;
using System;
using System.Text;

namespace FantasyAuction.DataModel
{
    public class Batter : IPlayer
    {
        public string Name { get; set; }
        public bool IsC { get; set; }
        public bool Is1B { get; set; }
        public bool Is2B { get; set; }
        public bool IsSS { get; set; }
        public bool Is3B { get; set; }
        public bool IsOF { get; set; }

        public bool IsCI { get { return this.Is1B || this.Is3B; } }
        public bool IsMI { get { return this.Is2B || this.IsSS; } }
        public bool IsUtil { get { return true; } }

        public float AuctionPrice { get; set; }
        public string FantasyTeam { get; set; }

        public int ProjectedAB { get; set; }
        public int ProjectedR { get; set; }
        public int ProjectedHR { get; set; }
        public int ProjectedRBI { get; set; }
        public int ProjectedSB { get; set; }
        public float ProjectedOBP { get; set; }

        public override string ToString()
        {
            return this.Name;
        }

        public string GetHTML()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<HTML><BODY><TABLE BORDER='1'>");
            sb.AppendFormat("<TR><TD>Name</TD><TD>{0}</TD></TR>", this.Name);
            sb.AppendFormat("<TR><TD>Is C</TD><TD>{0}</TD></TR>", this.IsC ? "Yes" : "No");
            sb.AppendFormat("<TR><TD>Is 1B</TD><TD>{0}</TD></TR>", this.Is1B ? "Yes" : "No");
            sb.AppendFormat("<TR><TD>Is 2B</TD><TD>{0}</TD></TR>", this.Is2B ? "Yes" : "No");
            sb.AppendFormat("<TR><TD>Is SS</TD><TD>{0}</TD></TR>", this.IsSS ? "Yes" : "No");
            sb.AppendFormat("<TR><TD>Is 3B</TD><TD>{0}</TD></TR>", this.Is3B ? "Yes" : "No");
            sb.AppendFormat("<TR><TD>Is OF</TD><TD>{0}</TD></TR>", this.IsOF ? "Yes" : "No");
            sb.AppendFormat("<TR><TD>Projected At Bats</TD><TD>{0}</TD></TR>", this.ProjectedAB);
            sb.AppendFormat("<TR><TD>Projected Runs</TD><TD>{0}</TD></TR>", this.ProjectedR);
            sb.AppendFormat("<TR><TD>Projected Home Runs</TD><TD>{0}</TD></TR>", this.ProjectedHR);
            sb.AppendFormat("<TR><TD>Projected RBIs</TD><TD>{0}</TD></TR>", this.ProjectedRBI);
            sb.AppendFormat("<TR><TD>Projected Steals</TD><TD>{0}</TD></TR>", this.ProjectedSB);
            sb.AppendFormat("<TR><TD>Projected OBP</TD><TD>{0}</TD></TR>", this.ProjectedOBP);
            sb.AppendLine("</TABLE></BODY></HTML>");
            return sb.ToString();
        }

        public static Batter Create(ESPNProjections.Batter batter)
        {
            Batter b = new Batter();
            b.Name = batter.FullName;
            b.IsC = batter.Positions.Contains(ESPNProjections.Constants.Positions.C);
            b.Is1B = batter.Positions.Contains(ESPNProjections.Constants.Positions.B1);
            b.Is2B = batter.Positions.Contains(ESPNProjections.Constants.Positions.B2);
            b.IsSS = batter.Positions.Contains(ESPNProjections.Constants.Positions.SS);
            b.Is3B = batter.Positions.Contains(ESPNProjections.Constants.Positions.B3);
            b.IsOF = batter.Positions.Contains(ESPNProjections.Constants.Positions.OF);
            b.AuctionPrice = 0;
            b.FantasyTeam = string.Empty;
            b.ProjectedAB = GetStat(batter.Stats[ESPNProjections.Constants.Stats.Batters.AB], 0);
            b.ProjectedR = GetStat(batter.Stats[ESPNProjections.Constants.Stats.Batters.R], 0);
            b.ProjectedHR = GetStat(batter.Stats[ESPNProjections.Constants.Stats.Batters.HR], 0);
            b.ProjectedRBI = GetStat(batter.Stats[ESPNProjections.Constants.Stats.Batters.RBI], 0);
            b.ProjectedSB = GetStat(batter.Stats[ESPNProjections.Constants.Stats.Batters.SB], 0);
            b.ProjectedOBP = GetStat(batter.Stats[ESPNProjections.Constants.Stats.Batters.OBP], 0.0f);
            return b;
        }
        
        public static Batter[] Load(string serialized)
        {
            return JsonConvert.DeserializeObject<Batter[]>(serialized);
        }

        public static string Serialize(Batter[] batters)
        {
            return JsonConvert.SerializeObject(batters);
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

        private static float GetStat(string stat, float defaultValue)
        {
            float val;
            if (!float.TryParse(stat, out val))
            {
                return defaultValue;
            }

            return val;
        }
    }
}
