using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wmcb.model.Data
{
    public class WmcbUser
    {
        public int ID { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public String FirstName { get; set; }
        [Required]
        public String LastName { get; set; }
        public String Phone { get; set; }
        public Boolean AllowLogin { get; set; }
        public DateTime RegDate { get; set; }
        public virtual Team Team { get; set; }
        public int TeamId { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
