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
            if (user != null && user.roles != null && user.IsInRole("Team Official"))
            {
                matchmodel.hasPermission = true;
                matchmodel.TeamID = user.TeamId.HasValue ? user.TeamId.Value : 0;
                matchmodel.TeamName = user.TeamName;
            }
            return matchmodel;
        }
        public List<ReviewMatchView> GetMatches()
        {
            using (var context = new wmcbContext())
            {
                //return context.Matches
                //                .Include("Schedule")
                //                .Include("Schedule.HomeTeam")
                //                .Include("Schedule.AwayTeam")
                //                .OrderBy(s => s.Schedule.DateTime)
                //                .Select(s => new ReviewMatchView
                //                            {
                //                                Match = s,
                //                                Schedule = new ScheduleView
                //                                    {
                //                                        Week = "Week " + s.Schedule.Week.ToString(),
                //                                        Home = s.Schedule.HomeTeamID == null ? s.Schedule.HomeTeamNote : s.Schedule.HomeTeam.Name,
                //                                        Away = s.Schedule.AwayTeamID == null ? s.Schedule.AwayTeamNote : s.Schedule.AwayTeam.Name,
                //                                        DateTime = s.Schedule.DateTime
                //                                    }
                //                            }).ToList();
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
                                HomeId = s1.ID,
                                Home = (s1 == null ? s.HomeTeamNote : s1.Name),
                                AwayId = s2.ID,
                                Away = (s2 == null ? s.AwayTeamNote : s2.Name),
                                DateTime = s.DateTime
                            }
                        }).ToList();

            }
        }

        public void ApproveMatchScore(int matchId)
        {
            using (var context = new wmcbContext())
            {
                var match = context.Matches.FirstOrDefault(m => m.ID == matchId);
                if (match != null)
                {
                    match.IsReviewed = true;
                    context.SaveChanges();
                }
            }
        }

        public void RejectMatchScore(int matchId)
        {
            using (var context = new wmcbContext())
            {
                var match = context.Matches.FirstOrDefault(m => m.ID == matchId);
                if (match != null)
                {
                    match.IsReviewed = false;
                    context.SaveChanges();
                }
            }
        }
    }
}
