using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wmcb.model.View
{
    public class TeamPoint
    {
        public string  Team { get; set; }
        public int GamePlayed { get; set; }
        public int Win { get; set; }
        public int Loss { get; set; }
        public int Abandon { get; set; }
        public int Tie { get; set; }
        public Decimal WinPercentage { get; set; }
        public int Points { get; set; }
        public Decimal NRR { get; set; }
        public int Weight { get; set; }
    }
}
