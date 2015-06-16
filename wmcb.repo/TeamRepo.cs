using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wmcb.model.View;
using wmcb.model;

namespace wmcb.repo
{
    public class TeamRepo
    {
        public List<TeamView> GetTeams()
        {
            using (var context = new wmcbContext())
            {
                var teams = from t in context.Teams 
                            from u1 in context.Users.Where(u1 => u1.ID == t.ContactID1).DefaultIfEmpty()
                            from u2 in context.Users.Where(u2 => u2.ID == t.ContactID2).DefaultIfEmpty()
                            from u3 in context.Users.Where(u3 => u3.ID == t.ContactID3).DefaultIfEmpty()
                            select new TeamView()
                            {
                                ID = t.ID,
                                Name = t.Name,
                                Contact1 = new UserView { ID = u1.ID, FirstName = u1.FirstName, LastName = u1.LastName, Email = u1.Email, Phone = u1.Phone, PhotoLink = "" },
                                Contact2 = new UserView { ID = u2.ID, FirstName = u2.FirstName, LastName = u2.LastName, Email = u2.Email, Phone = u2.Phone, PhotoLink = "" },
                                Contact3 = new UserView { ID = u3.ID, FirstName = u3.FirstName, LastName = u3.LastName, Email = u2.Email, Phone = u3.Phone, PhotoLink = "" }
                            };

                if (teams!=null)    
                    return teams.ToList();
                return null;
            }
        }
        public TeamView GetTeam(int teamID)
        {
            using (var context = new wmcbContext())
            {
                var team = (from t in context.Teams
                            from u1 in context.Users.Where(u1 => u1.ID == t.ContactID1).DefaultIfEmpty()
                            from u2 in context.Users.Where(u2 => u2.ID == t.ContactID2).DefaultIfEmpty()
                            from u3 in context.Users.Where(u3 => u3.ID == t.ContactID3).DefaultIfEmpty()
                            where t.ID == teamID
                            select new TeamView()
                            {
                                ID = t.ID,
                                Name = t.Name,
                                Contact1 = new UserView { ID = u1.ID, FirstName = u1.FirstName, LastName = u1.LastName, Email = u1.Email, Phone = u1.Phone, PhotoLink = "" },
                                Contact2 = new UserView { ID = u2.ID, FirstName = u2.FirstName, LastName = u2.LastName, Email = u2.Email, Phone = u2.Phone, PhotoLink = "" },
                                Contact3 = new UserView { ID = u3.ID, FirstName = u3.FirstName, LastName = u3.LastName, Email = u2.Email, Phone = u3.Phone, PhotoLink = "" }
                            }).FirstOrDefault();

                return team;
            }
        }
    }
}
