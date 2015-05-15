using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wmcb.model.Data;

namespace wmcb.repo
{
    public class UsersRepo
    {
        public WmcbUser GetUserDetails( string email)
        {
            using (var context = new wmcbContext())
            {
                var user = context.Users
                    .Where(u => u.Email.Equals(email))
                    .Take(1)
                    .Select(s => new WmcbUser() {
                        ID = s.ID,
                     FirstName =  s.FirstName,
                     LastName = s.LastName,
                     AllowLogin = s.AllowLogin,
                     Email = s.Email,
                     Phone = s.Phone
                  });
                return user.FirstOrDefault();
            }
        }
        public WmcbUser getLoggedInUser(string _username, string _password)
        {
            using (var context = new wmcbContext())
            {
                var encodedPwd = Helpers.SHA1.Encode(_password);
                var user = from u in context.Users
                           where u.Email.Equals(_username) && u.Password.Equals(encodedPwd)
                           select new WmcbUser()
                           {
                               ID = u.ID,
                               FirstName = u.FirstName,
                               LastName = u.LastName,
                               AllowLogin = u.AllowLogin,
                               Email = u.Email,
                               Phone = u.Phone,
                               Roles = (from ur in context.UserRoles
                                        join r in context.Roles on ur.RoleID equals r.ID
                                        select r).ToList()

                           };
                return user.FirstOrDefault();
            }
        }
        /// <summary>
        /// Checks if user with given password exists in the database
        /// </summary>
        /// <param name="_username">User name</param>
        /// <param name="_password">User password</param>
        /// <returns>True if user exist and password is correct</returns>
        public bool IsLoginValid(string _username, string _password)
        {
            using (var context = new wmcbContext())
            {
                var encodedPwd = Helpers.SHA1.Encode(_password); 
                var user = context.Users
                    .Where(u => u.Email.Equals(_username) && u.Password.Equals(encodedPwd))
                    .Take(1);
                
                return (user!=null && user.Count()>0);
            }
        }
    }
}
