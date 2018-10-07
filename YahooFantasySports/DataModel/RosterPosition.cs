using System.Xml;

namespace YahooFantasySports.DataModel
{
    public class RosterPosition
    {
        public string Position { get; }
        public string PositionType { get; }
        public int Count { get; }
        public bool AccumulatesStats { get; }

        internal static RosterPosition Create(XmlNode node, NSMgr nsmgr)
        {
            string position = nsmgr.GetValue(node, "position");
            string positionType = nsmgr.GetValue(node, "position_type");
            string countString = nsmgr.GetValue(node, "count");

            int count = int.Parse(countString);
            bool accumulatesStats = (position != "BN" && position != "DL" && position != "NA");
            return new RosterPosition(position, positionType, count, accumulatesStats);
        }

        private RosterPosition(string position, string positionType, int count, bool accumulatesStats)
        {
            this.Position = position;
            this.PositionType = positionType;
            this.Count = count;
            this.AccumulatesStats = accumulatesStats;
        }
    }
}
