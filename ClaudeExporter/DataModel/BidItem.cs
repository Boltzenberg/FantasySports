using System;
using System.Collections.Generic;

namespace ClaudeExporter.DataModel
{
    public class BidItem
    {
        public string Name { get; set; }
        public string MlbTeam { get; set; }
        public int Rank { get; set; }
        public int PreseasonRank { get; set; }
        public string Positions { get; set; }
        public string HighestBidder { get; set; }
        public double CurrentBid { get; set; }
        public double OldPrice { get; set; }
        public TimeRemaining TimeRemaining { get; set; }
        public string Topper { get; set; }
        public string YahooId { get; set; }
    }

    public class TimeRemaining
    {
        public int Days { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
    }
}
