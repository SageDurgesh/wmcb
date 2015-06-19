using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wmcb.model.Data
{
    public class TeamStats
    {
        [Key]
        public int ID { get; set; }

        public virtual Team Team { get; set; }
        [ForeignKey("Team")]
        public int TeamId { get; set; }

        public virtual Match Match { get; set; }
        [ForeignKey("Match")]
        public int MatchId { get; set; }
        public int Byes { get; set; }
        public int LegByes { get; set; }
        public int Wides { get; set; }
        public int NoBalls { get; set; }
        public int PenaltyRuns { get; set; }
        public int TeamScore { get; set; }
        [NotMapped]
        public int SubmittedByUserID { get; set; }
    }
}
