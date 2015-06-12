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
        [Key]
        public int ID { get; set; }
        public int Week { get; set; }
        public DateTime DateTime { get; set; }
        public int? HomeTeamID { get; set; }
        public int? AwayTeamID { get; set; }
        public int? GroundID { get; set; }
        public int TrophyID { get; set; }
        public int TournamentID { get; set; }
        public int MatchType { get; set; }
        public string HomeTeamNote { get; set; }
        public string AwayTeamNote { get; set; }
       // public virtual Match Match { get; set; }
    }
}
