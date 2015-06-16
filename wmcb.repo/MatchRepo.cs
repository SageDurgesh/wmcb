using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wmcb.model.Data;
using wmcb.model.Security;
using wmcb.model.View;

namespace wmcb.repo
{
    public class MatchRepo
    {
        //public Match GetMatch(int matchId)
        //{
        //    using (var context = new wmcbContext())
        //    {
        //        return context.Match.Include("HomeTeam").Include("AwayTeam").FirstOrDefault(m => m.ID == matchId);
        //    }
        //}

        //public void SetMatchComplete(Match match)
        //{
        //    using (var context = new wmcbContext())
        //    {
        //        var incompleteMatch = context.Match.FirstOrDefault(m => m.ID == match.ID);
        //        incompleteMatch.IsReviewed = true;
        //        context.SaveChanges();
        //    }
        //}

        public MatchScoreView GetMatchScoreView(WmcbPrincipal user)
        {
            MatchScoreView matchmodel = new MatchScoreView();
            if (user != null && user.roles != null && user.IsInRole("League Official"))
            {
                matchmodel.hasPermission = true;
                return context.Match.Include("HomeTeam").Include("AwayTeam").FirstOrDefault(m => m.ID == matchId);
                            .Include("HomeTeam")
                            .Include("AwayTeam")
                            .Include("Tournament")
                            .Include("Division")
                            .FirstOrDefault(m => m.ID == matchId);
            }
            return matchmodel;
        }
        public List<ReviewMatchView> GetMatches()
        {
            using (var context = new wmcbContext())
            {
                return (from m in context.Matches
                              join s in context.Schedules on m.ID equals s.ID
                              join home in context.Teams on s.HomeTeamID equals home.ID into sch1
                              from s1 in sch1.DefaultIfEmpty()
                              join away in context.Teams on s.AwayTeamID equals away.ID into sch2
                              from s2 in sch2.DefaultIfEmpty()
                              orderby s.DateTime
                              select new ReviewMatchView()
                              {
                                  Match = m,
                                  Schedule = new ScheduleView
                                  {
                                      Week = "Week " + s.Week.ToString(),
                                      Home = (s1 == null ? s.HomeTeamNote : s1.Name),
                                      Away = (s2 == null ? s.AwayTeamNote : s2.Name),
                                      DateTime = s.DateTime
                                  }
                              }).ToList();
               
            }
        }
    }
}
