using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wmcb.model.Data
{
    public class Schedule
    {
        public int ID { get; set; }
        public int Week { get; set; }
        public DateTime DateTime { get; set; }
        public string HomeTeamNote { get; set; }
        public string AwayTeamNote { get; set; }

        public virtual Team HomeTeam{ get; set; }
        [ForeignKey("HomeTeam")]
        public int? HomeTeamID { get; set; }

        public virtual Team AwayTeam { get; set; }
        [ForeignKey("AwayTeam")]
        public int? AwayTeamID { get; set; }

        public virtual Ground Ground { get; set; }
        [ForeignKey("Ground")]
        public int? GroundID { get; set; }

        //public virtual Tournament Tournament { get; set; }
        //[ForeignKey("Tournament")]
        public int TrophyID { get; set; }

        public virtual Tournament Tournament { get; set; }
        [ForeignKey("Tournament")]
        public int TournamentID { get; set; }

        public virtual Division Division { get; set; }
        [ForeignKey("Division")]
        public int MatchType { get; set; }
    }
}
