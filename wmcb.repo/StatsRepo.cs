using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wmcb.model.Data;
using wmcb.model.Security;
using wmcb.model.View;
//using System.Web.Mvc;

namespace wmcb.repo
{
    public class StatsRepo
    {
        public List<PlayerStatView> GetMatchPlayerStats(int matchId)
        {
            using (var context = new wmcbContext())
            {
                var players = (from p in context.PlayerStats
                               join u1 in context.Users on p.PlayerId equals u1.ID
                               join u2 in context.Users on p.Bowler equals u2.ID into b
                               from bowler in b.DefaultIfEmpty()
                               join u3 in context.Users on p.Fielder equals u3.ID into f
                               from fielder in f.DefaultIfEmpty()
                               where p.MatchId == matchId
                               select new PlayerStatView()
                                 {
                                     ID = p.ID,
                                     PlayerId = p.PlayerId,
                                     PlayerName = u1.LastName + " " + u1.FirstName.Substring(0, 1),
                                     TeamId = p.TeamId,
                                     MatchId = p.MatchId,
                                     BattingRuns = p.BattingRuns.HasValue ? p.BattingRuns.Value : 0,
                                     BallsFaced = p.BallsFaced.HasValue ? p.BallsFaced.Value : 0,
                                     HowOut = p.HowOut.HasValue ? p.HowOut.Value : 0,
                                     HowOutDesc = p.HowOut.HasValue ? p.HowOut.Value.ToString() : "0",
                                     BowlerId = p.Bowler.HasValue?p.Bowler.Value:0,
                                     BowlerName = bowler == null ? "" : bowler.LastName + " " + bowler.FirstName.Substring(0, 1),
                                     FielderId = p.Fielder.HasValue ? p.Fielder.Value : 0,
                                     FielderName = fielder == null ? "" : fielder.LastName + " " + bowler.FirstName.Substring(0, 1),
                                     Fours = p.Fours.HasValue ? p.Fours.Value : 0,
                                     Sixes = p.Sixes.HasValue ? p.Sixes.Value : 0,
                                     FOWRuns = p.FOWRuns.HasValue ? p.FOWRuns.Value : 0,
                                     WicketNumber = p.WicketNumber.HasValue ? p.WicketNumber.Value : 0,
                                     BowlerNumber = p.BowlerNumber.HasValue ? p.BowlerNumber.Value : 0,
                                     OversBowled = p.OversBowled.HasValue ? p.OversBowled.Value : 0,
                                     BowlingRuns = p.BowlingRuns.HasValue ? p.BowlingRuns.Value : 0,
                                     MaidenOvers = p.MaidenOvers.HasValue ? p.MaidenOvers.Value : 0,
                                     Wickets = p.Wickets.HasValue ? p.Wickets.Value : 0,
                                     NoBalls = p.NoBalls.HasValue ? p.NoBalls.Value : 0,
                                     Wide = p.Wide.HasValue ? p.Wide.Value : 0

                                 });
                return players.ToList();
            }
        }

        private string GetHowOutDesc(int? howout)
        {
            switch (howout)
            {
                case 1: return "Bowled";
                case 2: return "Caught";
                case 3: return "Run Out";
                case 4: return "Not Out";
                case 5: return "Did not play";
                default: return "Unknown";
            }
        }

        public List<TeamStats> GetMatchTeamStats(int matchId)
        {
            using (var context = new wmcbContext())
            {
                //var teamStats = context.TeamStats
                //    .Include("Team")
                //    .Include("Match")
                //    .Where(p => p.MatchId == matchId)
                //    .Select(p => p);
                //return teamStats.AsEnumerable().ToList();
                var teamstats = context.TeamStats.Where(p => p.MatchId == matchId).Select(p => p);
                return teamstats.ToList();
            }
        }

        public void SetPlayerStats(List<PlayerStats> players)
        {
            using (var context = new wmcbContext())
            {
                players.ForEach(p =>
                {
                    PlayerStats player;
                    using (var getcontext = new wmcbContext())
                    {
                        player = getcontext.PlayerStats.FirstOrDefault(ps => ps.PlayerId == p.PlayerId && ps.MatchId == p.MatchId);
                    }

                    if (player != null)
                    {
                        if (p.IsDeleted)
                            context.Entry(player).State = System.Data.Entity.EntityState.Deleted;
                        else
                        {
                            player.BattingRuns = p.BattingRuns;
                            player.BallsFaced = p.BallsFaced;
                            player.HowOut = p.HowOut;
                            player.Bowler = p.Bowler;
                            player.Fielder = p.Fielder;
                            player.OversBowled = p.OversBowled;
                            player.MaidenOvers = p.MaidenOvers;
                            player.Wickets = p.Wickets;
                            player.BowlingRuns = p.BowlingRuns;
                            player.Fours = p.Fours;
                            player.Sixes = p.Sixes;
                            context.Entry(player).State = System.Data.Entity.EntityState.Modified;
                        }
                    }
                    else
                        context.PlayerStats.Add(p);
                });

                context.SaveChanges();
            }
        }

        //public void SetTeamStats(List<TeamStats> teamStats)
        //{
        //teamStats.ForEach(ts => {
        //    TeamStats stat;
        //    Match match;

        //    using (var getcontext = new wmcbContext())
        //    {
        //        stat = getcontext.TeamStats.FirstOrDefault(t => ts.TeamId == t.TeamId && ts.MatchId == t.MatchId);
        //        match = getcontext.Match.FirstOrDefault(m => m.ID == ts.MatchId);
        //    }

        //    using (var context = new wmcbContext())
        //    {        
        //        if (stat != null)
        //        {
        //            stat.Wides = ts.Wides;
        //            stat.NoBalls = ts.NoBalls;
        //            stat.PenaltyRuns = ts.PenaltyRuns;
        //            stat.Byes = ts.Byes;
        //            stat.LegByes = ts.LegByes;
        //            stat.TeamScores = ts.TeamScores;
        //            context.Entry(stat).State = System.Data.Entity.EntityState.Modified;
        //        }
        //        else
        //            context.TeamStats.Add(ts);

        //        if (match.HomeTeamId == ts.TeamId)
        //        {
        //            match.HomeTeamScore = ts.TeamScores;
        //            context.Entry(match).State = System.Data.Entity.EntityState.Modified;
        //        }
        //        else if (match.AwayTeamId == ts.TeamId)
        //        {
        //            match.AwayTeamScore = ts.TeamScores;
        //            context.Entry(match).State = System.Data.Entity.EntityState.Modified;
        //        }
        //        context.SaveChanges();
        //    }
        //});

        //}

        public void SavePlayerStats(List<PlayerStats> stats)
        {
            using (var context = new wmcbContext())
            {
                var st = stats.FirstOrDefault();
                if (st != null)
                {
                    var stat = context.PlayerStats.Where(t => ((t.TeamId == st.TeamId) && (t.MatchId == st.MatchId))).Select(t => t);
                    if (stat != null)
                    {
                        foreach (var s in stat)
                        {
                            context.Entry(s).State = System.Data.Entity.EntityState.Deleted;

                        }
                        context.SaveChanges();
                    }
                    context.PlayerStats.AddRange(stats);
                    context.SaveChanges();
                }
            }
        }
        public void SetTeamStats(TeamStats teamstat)//,WmcbPrincipal user)
        {
            using (var context = new wmcbContext())
            {
                var stat = context.TeamStats.Where(t => (t.TeamId == teamstat.TeamId && t.MatchId == teamstat.TeamId));
                if (stat != null)
                {
                    foreach (var s in stat)
                    {
                        context.Entry(s).State = System.Data.Entity.EntityState.Deleted;

                    }
                    context.SaveChanges();
                }
                context.TeamStats.Add(teamstat);
                var sch = context.Schedules.Where(s => s.ID == teamstat.MatchId).Select(s => s).FirstOrDefault();
                var match = context.Matches.Where(m => m.ID == teamstat.MatchId).Select(s => s).FirstOrDefault();
                if (sch.HomeTeamID == teamstat.TeamId)
                {
                    if (match != null)
                    {
                        match.HomeTeamScore = teamstat.TeamScore;
                        match.TeamWonToss = teamstat.TeamWonToss;
                        match.TeamBattedFirst = teamstat.TeamBattedFirst;
                    }
                    else
                    {
                        match = new Match();
                        match.ID = teamstat.MatchId;
                        match.HomeTeamScore = teamstat.TeamScore;
                        match.TeamBattedFirst = teamstat.TeamBattedFirst;
                        match.TeamWonToss = teamstat.TeamWonToss;
                        //SubmittedBy = user.ID;
                        match.DateSubmitted = DateTime.Now;
                        match.IsReviewed = null;
                        context.Matches.Add(match);
                    }
                }
                else if (sch.AwayTeamID == teamstat.TeamId)
                {
                    if (match != null)
                    {
                        match.AwayTeamScore = teamstat.TeamScore;
                        match.TeamWonToss = teamstat.TeamWonToss;
                        match.TeamBattedFirst = teamstat.TeamBattedFirst;
                    }
                    else
                    {
                        match = new Match();
                        match.ID = teamstat.MatchId;
                        match.AwayTeamScore = teamstat.TeamScore;
                        match.TeamBattedFirst = teamstat.TeamBattedFirst;
                        match.TeamWonToss = teamstat.TeamWonToss;
                        //SubmittedBy = user.ID;
                        match.DateSubmitted = DateTime.Now;
                        match.IsReviewed = null;
                        context.Matches.Add(match);
                    }
                }
                context.SaveChanges();
            }

        }


    }
}
