using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using wmcb.model.View;
using wmcb.model.View;
using wmcb.model.Data;
using wmcb.model;
using wmcb.repo;

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
        public List<Schedule> GetSchedule()
        {
            var result = new ScheduleRepo().GetSchedule();
            return result;
        }

        [HttpGet]
        [Route("wmcb/teamPlayers")]
        public List<Player> GetTeamPlayers(int teamId)
        {
            var teamPlayers = new UsersRepo().GetTeamPlayers(teamId);
            return teamPlayers;
        }

        [WMCBAdminAuthorize("Admin", "League Official", "Team Official")]
        [HttpGet]
        [Route("wmcb/match")]
        public Match GetMatch(int matchId)
        {
            var match = new MatchRepo().GetMatch(matchId);
            return match;
        }

        [WMCBAdminAuthorize("Admin", "League Official", "Team Official")]
        [HttpGet]
        [Route("wmcb/matchPlayerStats")]
        public List<PlayerStatsDto> GetMatchPlayerStats(int matchId)
        {
            var result = new StatsRepo().GetMatchPlayerStats(matchId);
            return result;
        }

        [WMCBAdminAuthorize("Admin", "League Official", "Team Official")]
        [HttpGet]
        [Route("wmcb/matchTeamStats")]
        public List<TeamStats> GetMatchTeamsStats(int matchId)
        {
            var result = new StatsRepo().GetMatchTeamStats(matchId);

            return result;
        }

        [WMCBAdminAuthorize("League Official", "Team Official")]
        [HttpPost]
        [Route("wmcb/setPlayerStats")]
        public void SetPlayerStats(List<PlayerStats> players)
        {
            new StatsRepo().SetPlayerStats(players);
        }

        [WMCBAdminAuthorize("League Official", "Team Official")]
        [HttpPost]
        [Route("wmcb/setTeamStats")]
        public void SetPlayerStats(List<TeamStats> teamStats)
        {
            new StatsRepo().SetTeamStats(teamStats);
        }

        [WMCBAdminAuthorize("Admin", "League Official")]
        [HttpPost]
        [Route("wmcb/completeMatchScore")]
        public void CompleteMatchScore(Match match)
        {
            new MatchRepo().SetMatchComplete(match);
        }
    }
}
