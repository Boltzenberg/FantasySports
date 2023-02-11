using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FantasyAlgorithms.DataModel
{
    public class Batter : IPlayer
    {
        public string Name { get; set; }
        public int ESPNId { get; set; }
        public string YahooId { get; set; }
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
        public string AssumedFantasyTeam { get; set; }

        public DateTime ProjectionsLastUpdated { get; set; }
        public int ProjectedAB { get; set; }
        public int ProjectedH { get; set; }
        public int ProjectedBB { get; set; }
        public int ProjectedR { get; set; }
        public int ProjectedHR { get; set; }
        public int ProjectedRBI { get; set; }
        public int ProjectedSB { get; set; }
        public string SeasonOutlook { get; set; }
        public string Status { get; set; }
        public string ProfilePicture { get; set; }

        public override string ToString()
        {
            List<string> positions = new List<string>();
            foreach (Position p in this.Positions)
            {
                if (DataModel.Positions.DisplayPositions.Contains(p))
                {
                    positions.Add(DataModel.Positions.ToShortString(p));
                }
            }

            return string.Format("{0} ({1})", this.Name, string.Join(", ", positions));
        }

        public string GetHTML()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<TABLE BORDER='1'>");
            sb.AppendFormat("<TR><TD>Name</TD><TD>{0}</TD></TR>", this.Name);
            if (!string.IsNullOrEmpty(this.ProfilePicture))
            {
                sb.AppendFormat("<TR><TD>Picture</TD><TD><img src=\"data:image/png;base64,{0}\"/></TD></TR>", this.ProfilePicture);
            }
            sb.AppendFormat("<TR><TD>ESPN ID</TD><TD>{0}</TD></TR>", this.ESPNId);
            sb.AppendFormat("<TR><TD>Yahoo ID</TD><TD>{0}</TD></TR>", this.YahooId);
            sb.AppendFormat("<TR><TD>Is C</TD><TD>{0}</TD></TR>", this.IsC ? "Yes" : "No");
            sb.AppendFormat("<TR><TD>Is 1B</TD><TD>{0}</TD></TR>", this.Is1B ? "Yes" : "No");
            sb.AppendFormat("<TR><TD>Is 2B</TD><TD>{0}</TD></TR>", this.Is2B ? "Yes" : "No");
            sb.AppendFormat("<TR><TD>Is SS</TD><TD>{0}</TD></TR>", this.IsSS ? "Yes" : "No");
            sb.AppendFormat("<TR><TD>Is 3B</TD><TD>{0}</TD></TR>", this.Is3B ? "Yes" : "No");
            sb.AppendFormat("<TR><TD>Is OF</TD><TD>{0}</TD></TR>", this.IsOF ? "Yes" : "No");
            sb.AppendFormat("<TR><TD>Projected At Bats</TD><TD>{0}</TD></TR>", this.ProjectedAB);
            sb.AppendFormat("<TR><TD>Projected Hits</TD><TD>{0}</TD></TR>", this.ProjectedH);
            sb.AppendFormat("<TR><TD>Projected Walks</TD><TD>{0}</TD></TR>", this.ProjectedBB);
            sb.AppendFormat("<TR><TD>Projected Runs</TD><TD>{0}</TD></TR>", this.ProjectedR);
            sb.AppendFormat("<TR><TD>Projected Home Runs</TD><TD>{0}</TD></TR>", this.ProjectedHR);
            sb.AppendFormat("<TR><TD>Projected RBIs</TD><TD>{0}</TD></TR>", this.ProjectedRBI);
            sb.AppendFormat("<TR><TD>Projected Steals</TD><TD>{0}</TD></TR>", this.ProjectedSB);
            sb.AppendFormat("<TR><TD>Player Status</TD><TD>{0}</TD></TR>", this.Status);
            sb.AppendFormat("<TR><TD>Season Outlook</TD><TD>{0}</TD></TR>", this.SeasonOutlook);
            sb.AppendFormat("<TR><TD>Projections Updated</TD><TD>{0}</TD></TR>", this.ProjectionsLastUpdated);
            sb.AppendLine("</TABLE>");
            return sb.ToString();
        }

        public IEnumerable<Position> Positions
        {
            get
            {
                if (this.IsC) yield return Position.C;
                if (this.Is1B) yield return Position.B1;
                if (this.Is2B) yield return Position.B2;
                if (this.IsSS) yield return Position.SS;
                if (this.Is3B) yield return Position.B3;
                if (this.Is3B || this.Is1B) yield return Position.CI;
                if (this.Is2B || this.IsSS) yield return Position.MI;
                if (this.IsOF) yield return Position.OF;
                yield return Position.Util;
                yield return Position.BN;
            }
        }

        public static Batter Create(ESPNProjections.IPlayer batter)
        {
            Batter b = new Batter();
            b.AuctionPrice = 0;
            b.FantasyTeam = string.Empty;
            b.Update(batter);
            return b;
        }

        public void Update(ESPNProjections.IPlayer batter)
        {
            this.Name = batter.FullName;
            this.ESPNId = batter.Id;
            this.IsC |= batter.Positions.Contains(ESPNProjections.ESPNConstants.Positions.C);
            this.Is1B |= batter.Positions.Contains(ESPNProjections.ESPNConstants.Positions.B1);
            this.Is2B |= batter.Positions.Contains(ESPNProjections.ESPNConstants.Positions.B2);
            this.IsSS |= batter.Positions.Contains(ESPNProjections.ESPNConstants.Positions.SS);
            this.Is3B |= batter.Positions.Contains(ESPNProjections.ESPNConstants.Positions.B3);
            this.IsOF |= batter.Positions.Contains(ESPNProjections.ESPNConstants.Positions.OF);
            this.ProjectedAB = GetStat(batter.Stats[ESPNProjections.ESPNConstants.Stats.Batters.AB], 0);
            this.ProjectedR = GetStat(batter.Stats[ESPNProjections.ESPNConstants.Stats.Batters.R], 0);
            this.ProjectedH = GetStat(batter.Stats[ESPNProjections.ESPNConstants.Stats.Batters.H], 0);
            this.ProjectedBB = GetStat(batter.Stats[ESPNProjections.ESPNConstants.Stats.Batters.BB], 0);
            this.ProjectedHR = GetStat(batter.Stats[ESPNProjections.ESPNConstants.Stats.Batters.HR], 0);
            this.ProjectedRBI = GetStat(batter.Stats[ESPNProjections.ESPNConstants.Stats.Batters.RBI], 0);
            this.ProjectedSB = GetStat(batter.Stats[ESPNProjections.ESPNConstants.Stats.Batters.SB], 0);
            this.SeasonOutlook = batter.SeasonOutlook;
            this.ProjectionsLastUpdated = DateTime.Now;
            if (string.IsNullOrEmpty(this.ProfilePicture))
            {
                this.ProfilePicture = ESPNProjections.ESPNAPI.GetPlayerImage(this.ESPNId);
            }
        }

        public void Update(YahooFantasySports.DataModel.Player player)
        {
            this.YahooId = player.Id;
            this.Status = player.Status;
            this.IsC |= player.Positions.Contains(YahooFantasySports.Constants.Positions.Catcher);
            this.Is1B |= player.Positions.Contains(YahooFantasySports.Constants.Positions.FirstBase);
            this.Is2B |= player.Positions.Contains(YahooFantasySports.Constants.Positions.SecondBase);
            this.IsSS |= player.Positions.Contains(YahooFantasySports.Constants.Positions.Shortstop);
            this.Is3B |= player.Positions.Contains(YahooFantasySports.Constants.Positions.ThridBase);
            this.IsOF |= player.Positions.Contains(YahooFantasySports.Constants.Positions.Outfield);
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
