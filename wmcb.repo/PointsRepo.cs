using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wmcb.model.Data;
using wmcb.model.View;

namespace wmcb.repo
{
    public class PointsRepo
    {
        public List<TeamPoint> GetPoints(int type)
        {
            using (var context = new wmcbContext())
            {
                var pnt = (from p in context.Points
                           where p.Type.Equals(type)
                           group p by p.HomeTeamID into points                          
                          select new { 
                            team = points.Key,
                            gameplayed = points.Sum(p=> p.GamePlayed),
                            win = points.Sum(p=>p.ResultWin),
                            loss = points.Sum(p=>p.ResultLoss),
                            abandon = points.Sum(p=>p.ResultAban),
                            tie = points.Sum(p=>p.ResultTie),
                            points= points.Sum(p=>p.GamePoints),
                            runscored = points.Sum(p=>p.RunScored),
                            runsagainst = points.Sum(p=>p.RunsAgainst),
                            nrroversfor = points.Sum(p=>p.NRROversFor),
                            nrroversagainst = points.Sum(p=>p.NRROversAgainst)
                          }).ToList();
                var res = from p in pnt
                          join t in context.Teams on p.team equals t.ID                                                   
                          select new TeamPoint{
                              Team = t.Name,
                              GamePlayed = p.gameplayed,
                              Win = p.win,
                              Loss = p.loss,
                              Abandon = p.abandon,
                              Tie = p.tie,
                              WinPercentage = (Decimal)p.win/(Decimal)(p.gameplayed-p.abandon),
                              Points = p.points,
                              NRR = ((decimal)p.runscored/(decimal)(p.nrroversfor))-((decimal)p.runsagainst/(decimal)p.nrroversagainst)
                          }
                          ;
                return res.OrderByDescending(p =>  p.Points).ThenByDescending(p=>p.NRR).ToList();
            }
        }
    }
}
