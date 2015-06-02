using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wmcb.model.Data
{
    public class Schedule
    {
        public int ID { get; set; }
        public String Tournament { get; set; }
        public String Week { get; set; }
        public String Day { get; set; }
        public DateTime Date { get; set; }
        public String Division { get; set; }
        public String Home { get; set; }
        public String Away { get; set; }
        public String Field { get; set; }
        public String Time { get; set; }
    }
}
