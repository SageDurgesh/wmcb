using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wmcb.model.Data
{
    public class UserRoles
    {
        [Column(Order = 0), Key]
        public int UserID { get; set; }
        [Column(Order = 1), Key]
        public int RoleID { get; set; }
    }
}
