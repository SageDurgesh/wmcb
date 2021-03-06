﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wmcb.model.Data
{
    public class WmcbUser
    {
        public WmcbUser()
        {
            Roles = new List<UserRoles>();
        }
        [Key]
        public int ID { get; set; }
        [Required]
        public string Email { get; set; }        
        public string Password { get; set; }
        [Required]
        public String FirstName { get; set; }
        [Required]
        public String LastName { get; set; }
        public String Phone { get; set; }
        public Boolean AllowLogin { get; set; }
        public DateTime? RegDate { get; set; }       
        [ForeignKey("Team")]
        public int? TeamId { get; set; }
        public virtual Team Team { get; set; }
        public virtual ICollection<UserRoles> Roles { get; private set; }
        
        [NotMapped]
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }
       
    }
}
