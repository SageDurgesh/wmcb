using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wmcb.model.Data
{
    public class UserTeam
    {
        [Column(Order = 0), Key, ForeignKey("User")]
        public int UserID { get; set; }
        [Column(Order = 1), Key, ForeignKey("Team")]
        public int TeamID { get; set; }
        public virtual WmcbUser User { get; set; }
        public virtual Team Team { get; set; }
    }
}
