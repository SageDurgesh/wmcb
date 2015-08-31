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
                            email.Body = msg.Body.Replace("#name#", user.FirstName + " " + user.LastName).Replace("#username#", user.Email).Replace("#password#", user.Password);
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
        public UserView GetUserDetails(string email)
        {
            using (var context = new wmcbContext())
            {
                var user = from u in context.Users
                           join t in context.Teams on u.TeamId equals t.ID into tm
                           from tt in tm.DefaultIfEmpty()
                           where u.Email.Equals(email.Trim(), StringComparison.CurrentCultureIgnoreCase)
                           select new UserView()
                           {
                               FirstName = u.FirstName,
                               LastName = u.LastName,
                               Email = u.Email,
                               Phone = u.Phone,
                               TeamName = tt.Name
                           };

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
                        var pwd = Helper.RandomPasswordGenerator();
                        user.Password = Helpers.SHA1.Encode(pwd);
                        context.SaveChanges();
                        MessageTemplateDto msg = new MessageTemplateRepo().getMessageTemplate("PasswordReset");
                        if (msg != null)
                        {
                            EmailMessage emailmsg = new EmailMessage();
                            emailmsg.EmailAddress = user.Email;
                            emailmsg.Subject = msg.Subject;
                            emailmsg.Body = msg.Body.Replace("#name#", user.FirstName + " " + user.LastName).Replace("#username#", user.Email).Replace("#password#", pwd);
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

        public List<UserView> GetUsers()
        {
            using (var context = new wmcbContext())
            {
                var user = context.Users.Include("Team")
                    .Select(s => new UserView()
                    {
                        ID = s.ID,
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        Email = s.Email,
                        Phone = s.Phone,
                        TeamName = s.Team.Name
                    });

                return user.ToList();
            }
        }

        public Result UpdateUserProfile(UpdateProfile user)
        {
            using (var context = new wmcbContext())
            {
                if (String.IsNullOrEmpty(user.NewPassword))
                {
                    var usr = context.Users
                          .Where(u => u.Email.Equals(user.Email))
                          .Select(u => u).FirstOrDefault();
                    if (usr != null)
                    {
                        usr.FirstName = user.FirstName;
                        usr.LastName = user.LastName;
                        usr.Phone = user.Phone;
                        context.SaveChanges();
                        return new Result { Code = 0, Message = "Your profile has been updated successfully." };
                    }
                }
                else
                {
                    var encodedPwd = Helpers.SHA1.Encode(user.CurrentPassword);
                    var usr = context.Users
                               .Where(u => u.Email.Equals(user.Email) && u.Password.Equals(encodedPwd))
                               .Select(u => u).FirstOrDefault();
                    if (usr != null)
                    {
                        usr.FirstName = user.FirstName;
                        usr.LastName = user.LastName;
                        usr.Password = Helpers.SHA1.Encode(user.NewPassword);
                        usr.Phone = user.Phone;
                        context.SaveChanges();
                        return new Result { Code = 0, Message = "Your profile has been updated successfully." };
                    }
                }

                return new Result { Code = -1, Message = "An error occured while updating your profile." };
            }

        }
        public Result RegisterUser(NewUser user)
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
                    context.SaveChanges();
                    res.Code = 4;
                    res.Message = "User details have been updated.";
                }
                else
                {
                    newuser = context.Users
                           .Where(u => u.FirstName.Equals(user.FirstName, StringComparison.CurrentCultureIgnoreCase) &&
                               u.LastName.Equals(user.LastName, StringComparison.CurrentCultureIgnoreCase) &&
                               (u.TeamId.HasValue && u.TeamId.Value.Equals(user.TeamID)))
                           .Select(u => u).FirstOrDefault();
                    if (newuser != null)
                    {
                        newuser.TeamId = user.TeamID;
                        newuser.FirstName = user.FirstName;
                        newuser.LastName = user.LastName;
                        newuser.Phone = user.Phone;
                        newuser.Email = user.Email;
                        newuser.Password = "";
                        try
                        {
                            context.SaveChanges();
                        }
                        catch (System.Data.Entity.Validation.DbEntityValidationException e)
                        {
                            foreach (var eve in e.EntityValidationErrors)
                            {
                                Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                                foreach (var ve in eve.ValidationErrors)
                                {
                                    Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                        ve.PropertyName, ve.ErrorMessage);
                                }
                            }
                        }
                        res.Code = 4;
                        res.Message = "User details have been updated.";
                    }
                    else
                    {
                        newuser = new WmcbUser();
                        newuser.FirstName = user.FirstName;
                        newuser.LastName = user.LastName;
                        newuser.Email = user.Email;
                        newuser.Phone = user.Phone;
                        newuser.TeamId = user.TeamID;
                        newuser.RegDate = DateTime.Now;
                        try
                        {
                            context.Users.Add(newuser);
                            context.SaveChanges();

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
            }
            return res;
        }
    }
}
