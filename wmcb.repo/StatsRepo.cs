using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wmcb.model.Data;

namespace wmcb.repo
{
    public class StatsRepo
    {
        public List<PlayerStats> GetMatchStats(int matchId)
        {
            using (var context = new wmcbContext())
            {
                var players = context.PlayerStats
                    .Include("Team")
                    .Include("Player")
                    .Include("Match")
                    .Include("Match.AwayTeam")
                    .Include("Match.HomeTeam")
                    .Where(p => p.MatchId == matchId)
                    .Select(p => p).OrderBy(p => p.Player.LastName).ThenBy(p => p.Player.FirstName);
                return players.ToList();
            }
        }

        public void SetPlayerStats(List<PlayerStats> players)
        {
            using (var context = new wmcbContext())
            {
                players.ForEach(p => context.PlayerStats.Add(p));
                context.SaveChanges();
            }   
        }
    }
}
