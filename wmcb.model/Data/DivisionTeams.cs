using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wmcb.model.Data
{
    public class DivisionTeams
    {
        public virtual Team Team { get; set; }
        [ForeignKey("Team")]
        public int TeamId { get; set; }

        public virtual Division Division { get; set; }
        [ForeignKey("Division")]
        public int DivisionId { get; set; }
    }
}
