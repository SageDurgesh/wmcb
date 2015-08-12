using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wmcb.model;
using wmcb.model.Data;
using wmcb.model.View;
using wmcb.repo.Helpers;

namespace wmcb.repo
{
    public class UsersRepo
    {
        public Result AddUser(NewUser user)
        {
            Result res = new Result();
            res = ValidateUser(user);
            if (res.Code != 0)
                return res;
            WmcbUser newuser = new WmcbUser();
            using (var context = new wmcbContext())
            {
                newuser = context.Users
                           .Where(u => u.Email.Equals(user.Email))
                           .Select(u => u).FirstOrDefault();
                if (newuser != null)
                {
                    newuser.TeamId = user.TeamID;
                    newuser.FirstName = user.FirstName;
                    newuser.LastName = user.LastName;
                    newuser.Phone = user.Phone;
                    if (String.IsNullOrEmpty(newuser.Password))
                    {
                        newuser.Password = Helpers.SHA1.Encode(user.Password);
                        MessageTemplateDto msg = new MessageTemplateRepo().getMessageTemplate("UserProfileUpdate");
                        if (msg != null)
                        {
                            EmailMessage email = new EmailMessage();
                            email.EmailAddress = user.Email;
                            email.Subject = msg.Subject;
                            email.Body = msg.Body.Replace("#name#", user.FirstName + " " + user.LastName).Replace("#username#", user.Email).Replace("#password#", user.Password);
                            Email.SendEmail(email);
                        }
                    }
                    context.SaveChanges();
                    res.Code = 4;
                    res.Message = "User details have been updated.";                   
                }
                else
                {
                    newuser = new WmcbUser();
                    newuser.FirstName = user.FirstName;
                    newuser.LastName = user.LastName;
                    newuser.Email = user.Email;
                    newuser.Password = Helpers.SHA1.Encode(user.Password);
                    newuser.AllowLogin = true;
                    newuser.Phone = user.Phone;
                    newuser.TeamId = user.TeamID;
                    newuser.RegDate = DateTime.Now;
                    try
                    {
                        context.Users.Add(newuser);
                        context.SaveChanges();
                        //send email to user
                        MessageTemplateDto msg = new MessageTemplateRepo().getMessageTemplate("NewUserRegistration");
                        if (msg != null)
                        {
                            EmailMessage email = new EmailMessage();
                            email.EmailAddress = user.Email;
                            email.Subject = msg.Subject;
                            email.Body = msg.Body.Replace("#name#", user.FirstName + " " + user.LastName).Replace("#Username#", user.Email).Replace("#Password#", user.Password);
                            Email.SendEmail(email);
                        }
                        res.Code = 0;
                        res.Message = "";
                    }
                    catch
                    {
                        res.Code = -1;
                        res.Message = "An Error Occured while adding the new user.";
                    }
                }
            }
            return res;
        }

        private Result ValidateUser(NewUser user)
        {
            Result res = new Result();
            if (String.IsNullOrEmpty(user.Email))
            {
                res.Code = 1;
                res.Message = "Email address is required.";
            }
            else if (String.IsNullOrEmpty(user.FirstName))
            {
                res.Code = 2;
                res.Message = "Fist Name is required";
            }
            else if (String.IsNullOrEmpty(user.LastName))
            {
                res.Code = 3;
                res.Message = "Last Name is required";
            }
            else
            {
                res.Code = 0;
                res.Message = "";
            }
            return res;
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
        private WmcbUser GetUser(string email)
        {
            using (var context = new wmcbContext())
            {
                var user = context.Users
                           .Where(u => u.Email.Equals(email))
                           .Select(u => u).FirstOrDefault();

                return user;
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

        public Result ResetPassword(string email)
        {
            Result result = new Result();
            if (!String.IsNullOrEmpty(email))
            {
                using (var context = new wmcbContext())
                {
                    WmcbUser user = context.Users
                                  .Where(u => u.Email.Equals(email))
                                  .Select(u => u).FirstOrDefault();
                    if (user != null)
                    {
                        user.Password = Helper.RandomPasswordGenerator();
                        context.SaveChanges();
                        MessageTemplateDto msg = new MessageTemplateRepo().getMessageTemplate("PasswordReset");
                        if (msg != null)
                        {
                            EmailMessage emailmsg = new EmailMessage();
                            emailmsg.EmailAddress = user.Email;
                            emailmsg.Subject = emailmsg.Subject;
                            emailmsg.Body = emailmsg.Body.Replace("#name#", user.FirstName + " " + user.LastName).Replace("#username#", user.Email).Replace("#password#", user.Password);
                            Email.SendEmail(emailmsg);
                        }
                        result.Code = 0;
                    }
                    else
                    {
                        result.Code = 1;
                        result.Message = "User doesn't exist.";
                    }
                }
            }
            else
            {
                result.Code = 2;
                result.Message = "Invalid Email address.";
            }
            return result;
        }
    }
}
