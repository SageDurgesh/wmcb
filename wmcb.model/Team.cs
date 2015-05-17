using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wmcb.model
{
    public class Team
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? ContactID1 { get; set; }
        public int? ContactID2 { get; set; }
        public int? ContactID3 { get; set; }       
        public bool Active { get; set; }
        public int Points { get; set; }
    }
}
