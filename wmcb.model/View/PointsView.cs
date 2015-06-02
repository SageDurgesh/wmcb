using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wmcb.model.View
{
    public class PointsView
    {
        public int ID { get; set; }
        public int Matchday { get; set; }
        public string HomeTeamID { get; set; }
        public string AwayTeamID { get; set; }
        public string Type { get; set; }
        public string Result { get; set; }
        public int innings { get; set; }
        public int RunScored { get; set; }
        public int OversPlayed { get; set; }
        public int WicketLosst { get; set; }
        public int AllOut { get; set; }
        public int MaxOvers { get; set; }
        public int RunsAgainst { get; set; }
        public int OversBowled { get; set; }
        public int WicketsTaken { get; set; }
        public int BowledOut { get; set; }
        public int NRROversFor { get; set; }
        public int NRROversAgainst { get; set; }
        public int GamePlayed { get; set; }
        public int ResultWin { get; set; }
        public int ResultLoss { get; set; }
        public int ResultAban { get; set; }
        public int ResultTie { get; set; }
        public int GamePoints { get; set; }
    }
}
