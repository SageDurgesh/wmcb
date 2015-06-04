using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wmcb.model.Data
{
    public class Player
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public Team Team { get; set; }
        [ForeignKey("Team")]
        public int? TeamId { get; set; }

        [NotMapped]
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }
    }
}
