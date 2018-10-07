using System.Xml;

namespace YahooFantasySports.DataModel
{
    public class StatDefinition
    {
        public int Id { get; }
        public string Name { get; }
        public bool MoreIsBetter { get; }
        public string PositionType { get; }
        public bool CountsForPoints { get; }

        internal static StatDefinition Create(XmlNode node, NSMgr nsmgr)
        {
            string idString = nsmgr.GetValue(node, "stat_id");
            string name = nsmgr.GetValue(node, "name");
            string sortOrder = nsmgr.GetValue(node, "sort_order"); // 1 -> more is better
            string positionType = nsmgr.GetValue(node, "position_type");
            string displayOnly = nsmgr.GetValue(node, "is_only_display_stat"); // !1 -> counts for points

            int id = int.Parse(idString);
            bool moreIsBetter = sortOrder == "1";
            bool countsForPoints = (displayOnly != "1");
            return new StatDefinition(id, name, moreIsBetter, positionType, countsForPoints);
        }

        private StatDefinition(int id, string name, bool moreIsBetter, string positionType, bool countsForPoints)
        {
            this.Id = id;
            this.Name = name;
            this.MoreIsBetter = moreIsBetter;
            this.PositionType = positionType;
            this.CountsForPoints = countsForPoints;
        }
    }
}
