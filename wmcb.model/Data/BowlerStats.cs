using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wmcb.model.Data
{
    public class BowlerStats
    {
        public int ID { get; set; }
        public int PlayerID { get; set; }
        public int MatchID { get; set; }
        public decimal Overs { get; set; }
        public int Maiden { get; set; }
        public int Wickets { get; set; }
        public int Wides { get; set; }
        public int NoBalls { get; set; }
        public int Stumps { get; set; }
        public int Catches { get; set; }
    }
}
