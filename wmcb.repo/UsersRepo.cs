using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wmcb.model.Data;
using wmcb.model.View;

namespace wmcb.repo
{
    public class UsersRepo
    {
        public bool AddUser(NewUser user)
        {
            if (!String.IsNullOrEmpty(user.Email) && !doesUserExist(user.Email))
            {
                using (var context = new wmcbContext())
                {
                    WmcbUser newuser = new WmcbUser();
                    newuser.FirstName = user.FirstName;
                    newuser.LastName = user.LastName;
                    newuser.Email = user.Email;
                    newuser.Password = Helpers.SHA1.Encode(user.Password);
                    newuser.AllowLogin = true;
                    newuser.Phone = user.Phone;
                    newuser.RegDate = DateTime.Now;
                    try
                    {
                        context.Users.Add(newuser);
                        context.SaveChanges();
                    }
                    catch
                    {
                        return false;
                    }
                    return true;
                }
            }
            return false;
        }
        public WmcbUser GetUserDetails(string email)
        {
            using (var context = new wmcbContext())
            {
                var user = context.Users.Include("Team").Include("Roles").Include("Roles.Role")
                    .Where(u => u.Email.Equals(email))
                    .Select(s => s);

                return user.FirstOrDefault();
            }
        }
        private bool doesUserExist(string email)
        {
            using (var context = new wmcbContext())
            {
                var user = context.Users
                           .Where(u => u.Email.Equals(email))
                           .Select(u => u).FirstOrDefault();

                return user != null;
            }
        }
        public WmcbUser getLoggedInUser(string _username, string _password)
        {
            using (var context = new wmcbContext())
            {
                var encodedPwd = Helpers.SHA1.Encode(_password);
                var user = context.Users.Include("Team").Include("Roles").Include("Roles.Role")
                           .Where(u => u.Email.Equals(_username) && u.Password.Equals(encodedPwd))
                           .Select(u => u).FirstOrDefault();             

                return user;
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

                return (user != null && user.Count() > 0);
            }
        }
        public List<Player> GetTeamPlayers(int teamId)
        {
            using (var context = new wmcbContext())
            {
                var teamPlayers = context.Users.Include("Team")
                                                .Where(p => teamId == p.TeamId.Value)
                                                .Select(p => new Player
                                                                    {
                                                                        ID = p.ID,
                                                                        FirstName = p.FirstName,
                                                                        LastName = p.LastName,
                                                                        //Team = p.Team,
                                                                        TeamId = p.TeamId,
                                                                        Email = p.Email
                                                                    })
                                                .OrderBy(p => p.LastName)
                                                .ThenBy(p => p.FirstName);

                return teamPlayers.ToList();
            }
        }
    }
}
