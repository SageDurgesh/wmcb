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
        public List<PlayerStatsDto> GetMatchPlayerStats(int matchId)
        {
            using (var context = new wmcbContext())
            {
                var players = (from p in context.PlayerStats
                              join u1 in context.Users on p.PlayerId equals u1.ID
                              join u2 in context.Users on p.Bowler equals u2.ID
                              where p.MatchId == matchId
                              select new PlayerStatsDto{
                              ID = p.ID,
                        TeamId = p.TeamId,
                        MatchId = p.MatchId,
                        //  Team = p.p1.Team,
                        BattingRuns = p.BattingRuns,
                        BallsFaced = p.BallsFaced,
                        HowOut = p.HowOut,
                        HowOutDesc = GetHowOutDesc(p.HowOut),
                        WicketNumber = p.Wickets.HasValue?p.Wickets.Value:0,
                        BowlerNumber = p.BowlerNumber,
                        BowlerId = p.Bowler,
                        Fielder = p.Fielder,
                        OversBowled = p.OversBowled,
                        BowlingRuns = p.BowlingRuns,
                        MaidenOvers = p.MaidenOvers,
                        Wickets = p.Wickets,
                        Player = new Player
                        {
                            ID = p.PlayerId,
                            FirstName = u1.FirstName,
                            LastName = u1.LastName,
                            //Team = u1.Team,
                            TeamId = u1.TeamId,
                            Email = u1.Email
                        },
                        Bowler = new Player{
                            ID = u2.ID,
                            FirstName = u2.FirstName,
                            LastName = u2.LastName,
                            Email = u2.Email
                        }                          
                    // .Include("Team")
                    //.Include("Match")
                    //.Include("Match.AwayTeam")
                    // .Include("Match.HomeTeam")
                    
                    //.Where(p => p.p2.p1.MatchId == matchId)
                    //.Select(p => new PlayerStatsDto
                    //{
                    //    ID = p.p2.p1.ID,
                    //    TeamId = p.p2.p1.TeamId,
                    //    MatchId = p.p2.p1.MatchId,
                    //    //  Team = p.p1.Team,
                    //    BattingRuns = p.p2.p1.BattingRuns,
                    //    BallsFaced = p.p2.p1.BallsFaced,
                    //    HowOut = p.p2.p1.HowOut,
                    //    BowlerNumber = p.p2.p1.BowlerNumber,
                    //    BowlerId = p.p2.p1.Bowler,

                    //    Fielder = p.p2.p1.Fielder,
                    //    OversBowled = p.p2.p1.OversBowled,
                    //    BowlingRuns = p.p2.p1.BowlingRuns,
                    //    MaidenOvers = p.p2.p1.MaidenOvers,
                    //    Wickets = p.p2.p1.Wickets,
                    //    Player = new Player
                    //    {
                    //        ID = p.p2.p1.PlayerId,
                    //        FirstName = p.p2.u.FirstName,
                    //        LastName = p..LastName,
                    //        //Team = p.u.Team,
                    //        TeamId = p.u.TeamId,
                    //        Email = p.u.Email
                    //    }
                    }).OrderBy(p => p.Player.LastName).ThenBy(p => p.Player.FirstName);
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

        //public List<TeamStats> GetMatchTeamStats(int matchId)
        //{
        //    using (var context = new wmcbContext())
        //    {
        //        var teamStats = context.TeamStats
        //            .Include("Team")
        //            .Include("Match")
        //            .Where(p => p.MatchId == matchId)
        //            .Select(p => p);

        //        return teamStats.AsEnumerable().ToList();
        //    }
        //}

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
                        context.PlayerStats.RemoveRange(stat);
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
                    context.TeamStats.RemoveRange(stat);
                }
                context.TeamStats.Add(teamstat);
                var sch = context.Schedules.Where(s => s.ID == teamstat.MatchId).Select(s => s).FirstOrDefault();
                var match = context.Matches.Where(m => m.ID == teamstat.MatchId).Select(s => s).FirstOrDefault();
                if (sch.HomeTeamID == teamstat.TeamId)
                {
                    if (match != null)
                        match.HomeTeamScore = teamstat.TeamScore;
                    else
                    {
                        match = new Match();
                        match.ID = teamstat.MatchId;
                        match.HomeTeamScore = teamstat.TeamScore;
                        //SubmittedBy = user.ID;
                        match.DateSubmitted = DateTime.Now;
                        match.IsReviewed = false;
                        context.Matches.Add(match);
                    }
                }
                else if (sch.AwayTeamID == teamstat.TeamId)
                {
                    if (match != null)
                        match.AwayTeamScore = teamstat.TeamScore;
                    else
                    {
                        match = new Match();
                        match.ID = teamstat.MatchId;
                        match.AwayTeamScore = teamstat.TeamScore;
                        //SubmittedBy = user.ID;
                        match.DateSubmitted = DateTime.Now;
                        match.IsReviewed = false;
                        context.Matches.Add(match);
                    }
                }
                context.SaveChanges();
            }

        }


    }
}
