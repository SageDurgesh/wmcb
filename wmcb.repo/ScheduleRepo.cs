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
                var schedule = context.Schedules.Include("Match").Select(s=>s).OrderBy(s=>s.ID);
                return schedule.ToList();
            }
        }
    }
}
