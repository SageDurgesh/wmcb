using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wmcb.model.View
{
    public class PlayerStatView
    {
        public Int64 ID { get; set; }
        public int TeamId { get; set; }
        public int MatchId { get; set; }
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public int BattingRuns { get; set; }
        public int BallsFaced { get; set; }
        public int Fours { get; set; }
        public int Sixes { get; set; }
        public int HowOut { get; set; }
        private string _howoutdesc;
        public string HowOutDesc
        {
            get
            {
                return _howoutdesc;
            }
            set {
                var desc = "";
                switch (Convert.ToInt32(value))
                {
                    case 1: desc = "b"; break;
                    case 2: desc = "c"; break;
                    case 3: desc = "st"; break;
                    case 4: desc = "run out"; break;
                    case 5: desc = "not out"; break;
                    case 6: desc = "lbw"; break;
                    case 7: desc = "dnb"; break;
                    default: desc = "unknown"; break;
                }
               _howoutdesc = desc;
            }
        }
        public int WicketNumber { get; set; }
        public int FOWRuns { get; set; }
        public int BowlerId { get; set; } 
        public string BowlerName { get; set; }
        public int FielderId { get; set; }
        public string FielderName { get; set; }
        public int BowlerNumber { get; set; }               
        public decimal OversBowled { get; set; }
        public int Wickets { get; set; }
        public int MaidenOvers { get; set; }
        public int BowlingRuns { get; set; }
        public int Wide { get; set; }
        public int NoBalls { get; set; }      
    }
}
