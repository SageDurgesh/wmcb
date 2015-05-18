using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wmcb.model.Data
{
    public class Match
    {
        [Key]
        public int ID { get; set; }
        public string Description { get; set; }
        
        [Display(Name = "Home Team: ")]
        public virtual Team HomeTeam { get; set; }

        [ForeignKey("HomeTeam")]
        public int? HomeTeamId { get; set; }

        [Display(Name = "Away Team: ")]
        public virtual Team AwayTeam { get; set; }

        [ForeignKey("AwayTeam")]
        public int? AwayTeamId { get; set; }

        public bool IsReviewed { get; set; }
        public int? HomeTeamScore { get; set; }
        public int? AwayTeamScore { get; set; }
    }
}
