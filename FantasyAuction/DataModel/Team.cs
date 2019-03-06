using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyAuction.DataModel
{
    public class Team
    {
        public string Name { get; set; }
        public string Owner { get; set; }
        public float Budget { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
