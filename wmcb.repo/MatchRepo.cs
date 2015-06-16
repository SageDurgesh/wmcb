using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wmcb.model.Data;

namespace wmcb.repo
{
    public class MatchRepo
    {
        public Match GetMatch(int matchId)
        {
            using (var context = new wmcbContext())
            {
                return context.Match
                            .Include("HomeTeam")
                            .Include("AwayTeam")
                            .Include("Tournament")
                            .Include("Division")
                            .FirstOrDefault(m => m.ID == matchId);
            }
        }

        public void SetMatchComplete(Match match)
        {
            using (var context = new wmcbContext())
            {
                var incompleteMatch = context.Match.FirstOrDefault(m => m.ID == match.ID);
                incompleteMatch.IsReviewed = true;
                context.SaveChanges();
            }
        }
    }
}
