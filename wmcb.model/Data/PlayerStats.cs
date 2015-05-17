using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wmcb.model.Data
{
    public class PlayerStats
    {
        [Key]
        public int ID { get; set; }

        public virtual Team Team { get; set; }
        [ForeignKey("Team")]
        public int TeamId { get; set; }

        public virtual Match Match { get; set; }
        [ForeignKey("Match")]
        public int MatchId { get; set; }

        public virtual WmcbUser Player { get; set; }
        [ForeignKey("Player")]
        public int UserId { get; set; }

        public int? BattingRuns { get; set; }
        public int? BallsFaced { get; set; }
        public int? HowOut { get; set; }
        public int? BowlerNumber { get; set; }
        public int? Bowler { get; set; }
        public int? Fielder { get; set; }
        public decimal? OversBowled { get; set; }
        public int? Wickets { get; set; }
        public int? MaidenOvers { get; set; }
        public int? BowlingRuns { get; set; }
    }
}
