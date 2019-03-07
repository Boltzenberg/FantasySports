using System;
using System.Collections;
using System.Windows.Forms;

namespace FantasyAuctionUI
{
    internal class StatColumnSorter : IComparer
    {
        private int col;
        private bool moreIsBetter;

        public StatColumnSorter()
        {
            col = 0;
        }

        public StatColumnSorter(int column, bool moreIsBetter)
        {
            this.col = column;
            this.moreIsBetter = moreIsBetter;
        }

        public int Compare(object x, object y)
        {
            string xStr = ((ListViewItem)x).SubItems[col].Text;
            string yStr = ((ListViewItem)y).SubItems[col].Text;

            float xVal, yVal;
            if (float.TryParse(xStr, out xVal) &&
                float.TryParse(yStr, out yVal))
            {
                if (this.moreIsBetter)
                {
                    return yVal.CompareTo(xVal);
                }
                else
                {
                    return xVal.CompareTo(yVal);
                }
            }

            if (this.moreIsBetter)
            {
                return String.Compare(yStr, xStr);
            }
            else
            {
                return String.Compare(xStr, yStr);
            }
        }
    }
}
