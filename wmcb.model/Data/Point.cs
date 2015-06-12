using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wmcb.model.Data
{
    public class Point
    {
        public int ID { get; set; }
        public int Matchday { get; set; }
        public int HomeTeamID { get; set; }
        public int AwayTeamID { get; set; }
        public int Type { get; set; }
        public int Result { get; set; }
        public int innings { get; set; }
        public int RunScored { get; set; }
        public decimal OversPlayed { get; set; }
        public int WicketLost{ get; set; }
        public bool AllOut { get; set; }
        public int MaxOvers { get; set; }
        public int RunsAgainst { get; set; }
        public decimal OversBowled { get; set; }
        public int WicketsTaken { get; set; }
        public bool BowledOut { get; set; }
        public decimal NRROversFor { get; set; }
        public decimal NRROversAgainst { get; set; }
        public int GamePlayed { get; set; }
        public int ResultWin { get; set; }
        public int ResultLoss { get; set; }
        public int ResultAban { get; set; }
        public int ResultTie { get; set; }
        public int GamePoints { get; set; }
    }
}
