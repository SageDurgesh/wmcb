using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wmcb.model.Data
{
    public class PlayerStatsDto
    {
        [Key]
        public Int64 ID { get; set; }
       // public virtual Team Team { get; set; }
       // [ForeignKey("Team")]
        public int TeamId { get; set; }        
       // [ForeignKey("Match")]
        public int MatchId { get; set; }
        public Player Player { get; set; }
        //[ForeignKey("Player")]
        //public int PlayerId { get; set; }
        public int? BattingRuns { get; set; }
        public int? BallsFaced { get; set; }
        public int WicketNumber { get; set; }
        public int FOW { get; set; }
        public int? HowOut { get; set; }
        public string HowOutDesc { get; set; }
        public int? BowlerNumber { get; set; }
        public Player Bowler { get; set; }
        public int? BowlerId { get; set; }
        public int? Fielder { get; set; }
        public decimal? OversBowled { get; set; }
        public int? Wickets { get; set; }
        public int? MaidenOvers { get; set; }
        public int? BowlingRuns { get; set; }
        public int? Wide { get; set; }
        public int? NoBalls { get; set; }
        public int? Fours { get; set; }
        public int? Sixes { get; set; }
    }
}
