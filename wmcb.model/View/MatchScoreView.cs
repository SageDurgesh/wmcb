using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wmcb.model.Data;

namespace wmcb.model.View
{
    public class MatchScoreView
    {
        public bool hasPermission { get; set; }
        public int TeamID { get; set; }
        [Display(Name="Select the Match : ")]
        public int SelectedMatch { get; set; }
        public List<Player> HomePlayers { get; set; }
        public List<Player> AwayPlayers { get; set; }
        public List<ScheduleView> Matches { get; set; }

    }
}
