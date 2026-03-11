using System;
using System.Collections.Generic;

namespace ClaudeExporter.DataModel
{
    public class BidItem
    {
        public string PlayerName { get; private set; }
        public string MLBTeam { get; private set; }
        public int Rank { get; private set; }
        public int PreseasonRank { get; private set; }
        public List<string> Positions { get; private set; }
        public string HighestBiddingTeam { get; private set; }
        public float CurrentBidPrice { get; private set; }
        public float OldPrice { get; private set; }
        public TimeSpan TimeLeft { get; private set; }
        public string PossibleTopper { get; private set; }
        public string YahooId { get; private set; }

        public BidItem(string playerName, string mLBTeam, int rank, int preseasonRank, string positions, string highestBiddingTeam, float currentBidPrice, float oldPrice, TimeSpan timeLeft, string possibleTopper, string yahooId)
        {
            this.PlayerName = playerName;
            this.MLBTeam = mLBTeam;
            this.Rank = rank;
            this.PreseasonRank = preseasonRank;
            this.Positions = new List<string>(positions.Split(','));
            this.HighestBiddingTeam = highestBiddingTeam;
            this.CurrentBidPrice = currentBidPrice;
            this.OldPrice = oldPrice;
            this.TimeLeft = timeLeft;
            this.PossibleTopper = possibleTopper;
            this.YahooId = yahooId;
        }
    }

    public class RawBidItem
    {
        public string name { get; set; }
        public string mlbTeam { get; set; }
        public int rank { get; set; }
        public int preseasonRank { get; set; }
        public string positions { get; set; }
        public string highestBidder { get; set; }
        public string currentBid { get; set; }
        public string oldPrice { get; set; }
        public string[] timeParts { get; set; }
        public string topper { get; set; }
        public string yahooId { get; set; }
    }
}
