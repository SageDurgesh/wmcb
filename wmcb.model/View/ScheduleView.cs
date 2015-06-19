using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wmcb.model.View
{
    public class ScheduleView
    {
        public int ID { get; set; }
        public int HomeId { get; set; }
        public int AwayId { get; set; }
        public String Week { get; set; }
        public DateTime DateTime { get; set; }
        public string Home { get; set; }
        public string Away { get; set; }
        public string Field { get; set; }
        public string Trophy { get; set; }
        public string Tournament { get; set; }
        public string Division { get; set; }        
    }
}
