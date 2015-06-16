using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wmcb.model.Data;

namespace wmcb.repo
{
    public class ScheduleRepo
    {
        public List<Schedule> GetSchedule()
        {
            using (var context = new wmcbContext())
            {
                var schedule = context.Schedules
                                        .Include("Match")
                                        .Include("Match.HomeTeam")
                                        .Include("Match.AwayTeam")
                                        .Include("Match.Tournament")
                                        .Include("Match.Division")
                                        //.Where(s => s.Match.TournamentId != null || s.Match.DivisionId != null )
                                        .Select(s => s)
                                        .OrderBy(s => s.ID);
                return schedule.ToList();
            }
        }
    }
}
