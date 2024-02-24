using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NirSiteLib.DataModel
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
}
