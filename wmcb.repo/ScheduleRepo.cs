using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
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
                DateTime currentDate = DateTime.Now.Date;

                var schedule = from s in context.Schedules
                               orderby s.Date
                               where s.Date >= currentDate
                               select s;

                return schedule.ToList();
            }
        }
        public List<Schedule> GetUpcomingGames(int numofdays)
        {
            DateTime currentDate = DateTime.Now.Date;
            DateTime endEndDate = currentDate.AddDays(numofdays);
            using (var context = new wmcbContext())
            {
                var schedule = from s in context.Schedules
                               orderby s.Date
                               where s.Date >= currentDate && s.Date <= endEndDate
                               select s;

                return schedule.ToList();
            }
        }
    }
}
