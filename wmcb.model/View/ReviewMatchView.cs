using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wmcb.model.Data;

namespace wmcb.model.View
{
    public class ReviewMatchView
    {
        public Match Match { get; set; }
        public ScheduleView Schedule { get; set; }
        //public List<PlayerStatsDto> HomeTeam { get; set; }
        //public List<PlayerStatsDto> AwayTeam { get; set; }
    }
}
