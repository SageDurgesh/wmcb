using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using wmcb.model.View;
using wmcb.model.Data;
using wmcb.model;
using wmcb.repo;
using wmcb.model.Security;

namespace wmcb.web.Controllers
{
    public class ServiceAPIController : ApiController
    {
        [HttpGet]
        public List<NewsView> GetLatestNewsFeed(int count)
        {
            return new NewsFeedRepo().getLastestNewsFeeds(count);
        }

        [HttpGet]
        public List<NewsView> News()
        {
            return new NewsFeedRepo().getAllNewsFeeds();
        }
        [HttpGet]
        [Route("wmcb/teams")]
        public List<TeamView> GetTeams()
        {
            return new TeamRepo().GetTeams();
        }
        [HttpGet]
        [Route("wmcb/grounds")]
        public List<Ground> GetGrounds()
        {
            return new GroundRepo().GetGrounds();
        }
        [HttpGet]
        [Route("wmcb/schedule")]
        public List<ScheduleView> GetSchedule()
        {
            var result = new ScheduleRepo().GetSchedule();
            return result;
        }
        [HttpGet]
        [Route("wmcb/upcominggames/{numofdays}")]
        public List<ScheduleView> GetUpcomingGames(int numofdays)
        {
            return new ScheduleRepo().GetUpcomingGames(numofdays);
        }
        [HttpGet]
        [Route("wmcb/points/lastweek")]
        public DateTime GetLastWeek()
        {
            return new PointsRepo().GetLastWeek();
        }
        [HttpGet]
        [Route("wmcb/teamPlayers/{teamId}")]
        public List<Player> GetTeamPlayers(int teamId)
        {
            var teamPlayers = new UsersRepo().GetTeamPlayers(teamId);
            return teamPlayers;
        }
        [HttpGet]
        [Route("wmcb/myMatches/{teamId}")]
        public List<ScheduleView> GetMyMatches(int teamId)
        {
            var macthes = new ScheduleRepo().GetMyMatches(teamId);
            return macthes;
        }
        [HttpGet]
        [Route("wmcb/MatchesWithNoScore")]
        public List<ScheduleView> GetMatchesWithNoScore()
        {
            var macthes = new ScheduleRepo().GetMatchesWithNoScore();
            return macthes;
        }

        [WMCBAdminAuthorize("Admin", "League Official", "Team Official")]
        [HttpGet]
        [Route("wmcb/Matches")]
        public List<ReviewMatchView> GetMatches()
        {
            var match = new MatchRepo().GetMatches();
            return match;
        }
        //[WMCBAdminAuthorize("Admin", "League Official", "Team Official")]
        //[HttpGet]
        //[Route("wmcb/match")]
        //public Match GetMatch(int matchId)
        //{
        //    var match = new MatchRepo().GetMatch(matchId);
        //    return match;
        //}

        [WMCBAdminAuthorize("Admin", "League Official", "Team Official")]
        [HttpGet]
        [Route("wmcb/matchPlayerStats/{matchId}")]
        public List<PlayerStatsDto> GetMatchPlayerStats(int matchId)
        {
            var result = new StatsRepo().GetMatchPlayerStats(matchId);
            return result;
        }

        [WMCBAdminAuthorize("Admin", "League Official", "Team Official")]
        [HttpGet]
        [Route("wmcb/matchTeamStats/{matchId}")]
        public List<TeamStats> GetMatchTeamsStats(int matchId)
        {
            var result = new StatsRepo().GetMatchTeamStats(matchId);

            return result;
        }
        

        [HttpGet]
        [Route("wmcb/points/{type}")]
        public List<TeamPoint> GetPoints(int type)
        {
            return new PointsRepo().GetDivisionPoints(type);
        }
        [HttpGet]
        [Route("wmcb/points/conf/{id}")]
        public List<TeamPoint> GetConfPoints(int id)
        {
            return new PointsRepo().GetConferencePoints(id);
        }
        [HttpPost]
        [Route("wmcb/user/add")]
        public Result AddUser(NewUser user)
        {
           return new UsersRepo().AddUser(user);
        }
        [WMCBAdminAuthorize("League Official", "Team Official")]
        [HttpPost]
        [Route("wmcb/setPlayerStats")]
        public void SetPlayerStats(List<PlayerStats> players)
        {
            //new StatsRepo().SetPlayerStats(players);
        }

        //[WMCBAdminAuthorize("League Official", "Team Official")]
        //[HttpPost]
        //[Route("wmcb/setPlayerStats")]
        //public void SetPlayerStats(List<PlayerStats> players)
        //{
        //    new StatsRepo().SetPlayerStats(players);
        //}

        [WMCBAdminAuthorize("League Official", "Team Official")]
        [HttpPost]
        [Route("wmcb/SavePlayerStats")]
        public void SavePlayerStats(List<PlayerStats> stats)
        {
            new StatsRepo().SavePlayerStats(stats);
        }
        [WMCBAdminAuthorize("League Official", "Team Official")]
        [HttpPost]
        [Route("wmcb/SetTeamStats")]
        public void SetTeamStats(TeamStats stats)
        {
            new StatsRepo().SetTeamStats(stats);//, ((WmcbPrincipal) HttpContext.User));
        }

        [WMCBAdminAuthorize("Admin", "League Official")]
        [HttpPost]
        [Route("wmcb/ApproveMatchScore")]
        public void ApproveMatchScore(Match match)
        {
           new MatchRepo().ApproveMatchScore(match.ID);
        }
        [WMCBAdminAuthorize("Admin", "League Official")]
        [HttpPost]
        [Route("wmcb/RejectMatchScore")]
        public void RejectMatchScore(Match match)
        {
            new MatchRepo().RejectMatchScore(match.ID);
        }
        
    }
}
