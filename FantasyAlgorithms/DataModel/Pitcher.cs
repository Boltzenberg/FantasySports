﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FantasyAlgorithms.DataModel
{
    public class Pitcher : IPlayer
    {
        public string Name { get; set; }
        public int ESPNId { get; set; }
        public string YahooId { get; set; }
        public bool IsSP { get; set; }
        public bool IsRP { get; set; }

        public float AuctionPrice { get; set; }
        public string FantasyTeam { get; set; }
        public string AssumedFantasyTeam { get; set; }

        public DateTime ProjectionsLastUpdated { get; set; }
        public int ProjectedOutsRecorded { get; set; }
        public int ProjectedW { get; set; }
        public int ProjectedL { get; set; }
        public int ProjectedQS { get; set; }
        public int ProjectedHld { get; set; }
        public int ProjectedSV { get; set; }
        public int ProjectedK { get; set; }
        public int ProjectedHits { get; set; }
        public int ProjectedWalks { get; set; }
        public int ProjectedER { get; set; }
        public string SeasonOutlook { get; set; }
        public string Status { get; set; }
        public bool IsBatter { get { return false; } }
        public string ProfilePicture { get; set; }

        public float ProjectedIP { get; set; }
        public float ProjectedERA { get; set; }
        public float ProjectedWHIP { get; set; }

        public string Rank { get; set; }
        public string TotalRanking { get; set; }
        public string TotalRating { get; set; }

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
            sb.AppendLine("<HTML><BODY><TABLE BORDER='1'>");
            sb.AppendFormat("<TR><TD>Name</TD><TD>{0}</TD></TR>", this.Name);
            if (!string.IsNullOrEmpty(this.ProfilePicture))
            {
                sb.AppendFormat("<TR><TD>Picture</TD><TD><img src=\"data:image/png;base64,{0}\"/></TD></TR>", this.ProfilePicture);
            }
            sb.AppendFormat("<TR><TD>ESPN ID</TD><TD>{0}</TD></TR>", this.ESPNId);
            sb.AppendFormat("<TR><TD>Yahoo ID</TD><TD>{0}</TD></TR>", this.YahooId);
            sb.AppendFormat("<TR><TD>Rank</TD><TD>{0}</TD></TR>", this.Rank);
            sb.AppendFormat("<TR><TD>Total Ranking</TD><TD>{0}</TD></TR>", this.TotalRanking);
            sb.AppendFormat("<TR><TD>Total Rating</TD><TD>{0}</TD></TR>", this.TotalRating);
            sb.AppendFormat("<TR><TD>Is SP</TD><TD>{0}</TD></TR>", this.IsSP ? "Yes" : "No");
            sb.AppendFormat("<TR><TD>Is RP</TD><TD>{0}</TD></TR>", this.IsRP ? "Yes" : "No");
            sb.AppendFormat("<TR><TD>Projected Innings Pitched</TD><TD>{0}</TD></TR>", this.ProjectedIP);
            sb.AppendFormat("<TR><TD>Projected Wins</TD><TD>{0}</TD></TR>", this.ProjectedW);
            sb.AppendFormat("<TR><TD>Projected Losses</TD><TD>{0}</TD></TR>", this.ProjectedL);
            sb.AppendFormat("<TR><TD>Projected Quality Starts</TD><TD>{0}</TD></TR>", this.ProjectedQS);
            sb.AppendFormat("<TR><TD>Projected Saves</TD><TD>{0}</TD></TR>", this.ProjectedSV);
            sb.AppendFormat("<TR><TD>Projected Holds</TD><TD>{0}</TD></TR>", this.ProjectedHld);
            sb.AppendFormat("<TR><TD>Projected Strikeouts</TD><TD>{0}</TD></TR>", this.ProjectedK);
            sb.AppendFormat("<TR><TD>Projected ERA</TD><TD>{0}</TD></TR>", this.ProjectedERA);
            sb.AppendFormat("<TR><TD>Projected WHIP</TD><TD>{0}</TD></TR>", this.ProjectedWHIP);
            sb.AppendFormat("<TR><TD>Projected Outs Recorded</TD><TD>{0}</TD></TR>", this.ProjectedOutsRecorded);
            sb.AppendFormat("<TR><TD>Projected Hits</TD><TD>{0}</TD></TR>", this.ProjectedHits);
            sb.AppendFormat("<TR><TD>Projected Walks</TD><TD>{0}</TD></TR>", this.ProjectedWalks);
            sb.AppendFormat("<TR><TD>Projected Earned Runs</TD><TD>{0}</TD></TR>", this.ProjectedER);
            sb.AppendFormat("<TR><TD>Player Status</TD><TD>{0}</TD></TR>", this.Status);
            sb.AppendFormat("<TR><TD>Season Outlook</TD><TD>{0}</TD></TR>", this.SeasonOutlook);
            sb.AppendFormat("<TR><TD>Projections Updated</TD><TD>{0}</TD></TR>", this.ProjectionsLastUpdated);
            sb.AppendLine("</TABLE></BODY></HTML>");
            return sb.ToString();
        }

        public IEnumerable<Position> Positions
        {
            get
            {
                yield return Position.P;
                yield return Position.BN;
            }
        }

        public static Pitcher Create(ESPNProjections.IPlayer pitcher)
        {
            Pitcher p = new Pitcher();
            p.AuctionPrice = 0;
            p.FantasyTeam = string.Empty;
            p.Update(pitcher);
            return p;
        }

        public void Update(ESPNProjections.IPlayer pitcher)
        {
            this.Name = pitcher.FullName;
            this.ESPNId = pitcher.Id;
            this.Rank = pitcher.Rank;
            this.TotalRanking = pitcher.TotalRanking;
            this.TotalRating = pitcher.TotalRating;
            this.IsSP = pitcher.Positions.Contains(ESPNProjections.ESPNConstants.Positions.SP);
            this.IsRP = pitcher.Positions.Contains(ESPNProjections.ESPNConstants.Positions.RP);
            this.ProjectedOutsRecorded = GetStat(pitcher.Stats[ESPNProjections.ESPNConstants.Stats.Pitchers.OutsRecorded], 0);
            this.ProjectedW = GetStat(pitcher.Stats[ESPNProjections.ESPNConstants.Stats.Pitchers.W], 0);
            this.ProjectedL = GetStat(pitcher.Stats[ESPNProjections.ESPNConstants.Stats.Pitchers.L], 0);
            this.ProjectedQS = GetStat(pitcher.Stats[ESPNProjections.ESPNConstants.Stats.Pitchers.QS], 0);
            this.ProjectedSV = GetStat(pitcher.Stats[ESPNProjections.ESPNConstants.Stats.Pitchers.SV], 0);
            this.ProjectedHld = GetStat(pitcher.Stats[ESPNProjections.ESPNConstants.Stats.Pitchers.Hld], 0);
            this.ProjectedK = GetStat(pitcher.Stats[ESPNProjections.ESPNConstants.Stats.Pitchers.K], 0);
            this.ProjectedHits = GetStat(pitcher.Stats[ESPNProjections.ESPNConstants.Stats.Pitchers.H], 0);
            this.ProjectedWalks = GetStat(pitcher.Stats[ESPNProjections.ESPNConstants.Stats.Pitchers.BB], 0);
            this.ProjectedER = GetStat(pitcher.Stats[ESPNProjections.ESPNConstants.Stats.Pitchers.ER], 0);
            this.ProjectedIP = ((int)this.ProjectedOutsRecorded / 3) + (this.ProjectedOutsRecorded % 3) / 10;
            this.ProjectedERA = this.ProjectedER / ((float)this.ProjectedOutsRecorded / 27);
            this.ProjectedWHIP = (this.ProjectedHits + this.ProjectedWalks) / ((float)this.ProjectedOutsRecorded / 3);
            this.SeasonOutlook = pitcher.SeasonOutlook;
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
        }

        public void ClearYahooData()
        {
            this.YahooId = null;
            this.Status = null;
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
                float fVal;
                if (!float.TryParse(stat, out fVal))
                {
                    return defaultValue;
                }
                return (int)fVal;
            }

            return val;
        }
    }
}
