using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wmcb.model.Data;
using wmcb.model.View;

namespace wmcb.repo
{
    public class ScheduleRepo
    {
        public List<ScheduleView> GetSchedule()
        {
            using (var context = new wmcbContext())
            {
                DateTime currentDate = DateTime.Now.Date;

                //var schedule = context.Schedules
                //                        .Include("HomeTeam")
                //                        .Include("AwayTeam")
                //                        .Include("Ground")
                //                        .Include("Tournament")
                //                        .Include("Division")
                //                        .Where(s => s.DateTime >= currentDate)
                //                        .OrderBy(s => s.DateTime)
                //                        .Select(s => new ScheduleView
                //                        {
                //                            ID = s.ID,
                //                            HomeId = s.HomeTeamID.HasValue ? s.HomeTeamID.Value : 0,
                //                            AwayId = s.AwayTeamID.HasValue ? s.AwayTeamID.Value : 0,
                //                            Week = "Week " + s.Week.ToString(),
                //                            Home = s.HomeTeam == null ? s.HomeTeamNote : s.HomeTeam.Name,
                //                            Away = s.AwayTeam == null ? s.AwayTeamNote : s.AwayTeam.Name,
                //                            DateTime = s.DateTime,
                //                            Tournament = s.Tournament.Name,
                //                            Division = s.Division.Name
                //                        });

                var schedule = from s in context.Schedules
                               join home in context.Teams on s.HomeTeamID equals home.ID into sch1
                               from s1 in sch1.DefaultIfEmpty()
                               join away in context.Teams on s.AwayTeamID equals away.ID into sch2
                               from s2 in sch2.DefaultIfEmpty()
                               join ground in context.Grounds on s.GroundID equals ground.ID into grnd
                               from g1 in grnd.DefaultIfEmpty()
                               join t1 in context.Tournament on s.TournamentID equals t1.ID into trn
                               from trn1 in trn.DefaultIfEmpty()
                               join d1 in context.Division on s.MatchType equals d1.ID
                               orderby s.DateTime
                               where s.DateTime >= currentDate
                               select new ScheduleView
                               {
                                   Week = "Week " + s.Week.ToString(),
                                   Home = (s1 == null ? s.HomeTeamNote : s1.Name),
                                   Away = (s2 == null ? s.AwayTeamNote : s2.Name),
                                   DateTime = s.DateTime,
                                   Field = (g1 == null ? "TBD" : g1.Name),
                                   Division = d1.Name,
                                   Tournament = trn1.Name
                               };

                return schedule.ToList();
            }
        }
        public List<ScheduleView> GetUpcomingGames(int numofdays)
        {
            DateTime currentDate = DateTime.Now.Date;
            DateTime endEndDate = currentDate.AddDays(numofdays);
            using (var context = new wmcbContext())
            {
                var schedule = from s in context.Schedules
                               join home in context.Teams on s.HomeTeamID equals home.ID into sch1
                               from s1 in sch1.DefaultIfEmpty()
                               join away in context.Teams on s.AwayTeamID equals away.ID into sch2
                               from s2 in sch2.DefaultIfEmpty()
                               join ground in context.Grounds on s.GroundID equals ground.ID into grnd
                               from g1 in grnd.DefaultIfEmpty()
                               join t1 in context.Tournament on s.TournamentID equals t1.ID into trn
                               from trn1 in trn.DefaultIfEmpty()
                               join d1 in context.Division on s.MatchType equals d1.ID
                               orderby s.DateTime
                               where s.DateTime >= currentDate && s.DateTime <= endEndDate
                               select new ScheduleView
                               {
                                   Week = "Week " + s.Week.ToString(),
                                   Home = (s1 == null ? s.HomeTeamNote : s1.Name),
                                   Away = (s2 == null ? s.AwayTeamNote : s2.Name),
                                   DateTime = s.DateTime,
                                   Field = (g1 == null ? "TBD" : g1.Name),
                                   Division = d1.Name,
                                   Tournament = trn1.Name
                               };

                //var schedule = context.Schedules
                //                        .Include("HomeTeam")
                //                        .Include("AwayTeam")
                //                        .Include("Ground")
                //                        .Include("Tournament")
                //                        .Include("Division")
                //                        .Where(s => s.DateTime >= currentDate && s.DateTime <= endEndDate)
                //                        .Select(s => new ScheduleView
                //                        {
                //                            ID = s.ID,
                //                            HomeId = s.HomeTeamID.HasValue ? s.HomeTeamID.Value : 0,
                //                            AwayId = s.AwayTeamID.HasValue ? s.AwayTeamID.Value : 0,
                //                            Week = "Week " + s.Week.ToString(),
                //                            Home = s.HomeTeam == null ? s.HomeTeamNote : s.HomeTeam.Name,
                //                            Away = s.AwayTeam == null ? s.AwayTeamNote : s.AwayTeam.Name,
                //                            DateTime = s.DateTime,
                //                            Tournament = s.Tournament.Name,
                //                            Division = s.Division.Name
                //                        });

                return schedule.ToList();
            }
        }
        public List<ScheduleView> GetMyMatches(int TeamID)
        {
            DateTime currentDate = DateTime.Now.Date;
            using (var context = new wmcbContext())
            {
                //var schedule = context.Schedules
                //                        .Include("HomeTeam")
                //                        .Include("AwayTeam")
                //                        .Include("Ground")
                //                        .Include("Tournament")
                //                        .Include("Division")
                //                        .GroupJoin(context.Matches, s => s.ID, m => m.ID, (s, m) => new { schedule = s, match = m })
                //                        .SelectMany(s => s.match.DefaultIfEmpty(), (s, m) => new { s, m })
                //                        .Where(s => s.s.schedule.DateTime <= currentDate
                //                                    && (s.s.schedule.HomeTeamID == TeamID || s.s.schedule.AwayTeamID == TeamID)
                //                                    && (s.m == null || !s.m.IsReviewed))
                //                        .OrderByDescending(s => s.s.schedule.DateTime)
                //                        .Select(s => new ScheduleView
                //                                        {
                //                                            ID = s.s.schedule.ID,
                //                                            HomeId = s.s.schedule.HomeTeamID.HasValue ? s.s.schedule.HomeTeamID.Value : 0,
                //                                            AwayId = s.s.schedule.AwayTeamID.HasValue ? s.s.schedule.AwayTeamID.Value : 0,
                //                                            Week = "Week " + s.s.schedule.Week.ToString(),
                //                                            Home = s.s.schedule.HomeTeam == null ? s.s.schedule.HomeTeamNote : s.s.schedule.HomeTeam.Name,
                //                                            Away = s.s.schedule.AwayTeam == null ? s.s.schedule.AwayTeamNote : s.s.schedule.AwayTeam.Name,
                //                                            DateTime = s.s.schedule.DateTime,
                //                                            Tournament = s.s.schedule.Tournament.Name,
                //                                            Division = s.s.schedule.Division.Name
                //                                        });
                var schedule = from s in context.Schedules
                               join home in context.Teams on s.HomeTeamID equals home.ID into sch1
                               from s1 in sch1.DefaultIfEmpty()
                               join away in context.Teams on s.AwayTeamID equals away.ID into sch2
                               from s2 in sch2.DefaultIfEmpty()
                               join m1 in context.Matches on s.ID equals m1.ID into m11
                               from match in m11.DefaultIfEmpty()
                               where s.DateTime <= currentDate && (s.HomeTeamID == TeamID || s.AwayTeamID == TeamID) && (match == null || match.IsReviewed == false)
                               orderby s.DateTime descending
                               select new ScheduleView
                               {
                                   ID = s.ID,
                                   HomeId = s.HomeTeamID.HasValue ? s.HomeTeamID.Value : 0,
                                   AwayId = s.AwayTeamID.HasValue ? s.AwayTeamID.Value : 0,
                                   Week = "Week " + s.Week.ToString(),
                                   Home = (s1 == null ? s.HomeTeamNote : s1.Name),
                                   Away = (s2 == null ? s.AwayTeamNote : s2.Name),
                                   DateTime = s.DateTime
                               };
                if (schedule != null)
                    return schedule.ToList();
                return null;
            }
        }
    }
}
