using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace wmcb.model.Security
{
    public class WmcbPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role)
        {
            if (roles.Any(r => role.Contains(r)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public WmcbPrincipal(string Username)
        {
            this.Identity = new GenericIdentity(Username);
        }
        public int ID { get; set; }
        public int? TeamId { get; set; }
        public string TeamName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string[] roles { get; set; }
    }
}
