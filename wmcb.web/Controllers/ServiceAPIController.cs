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
        [Route("wmcb/match")]
        public Match GetMatch(int matchId)
        {
            var match = new MatchRepo().GetMatch(matchId);
            return match;
        }

        [HttpGet]
        [Route("wmcb/matchStats")]
        public List<PlayerStats> GetMatchStats(int matchId)
        {
            var result = new StatsRepo().GetMatchStats(matchId);
            return result;
        }

        [HttpPost]
        [Route("wmcb/setPlayerStats")]
        public void SetPlayerStats(List<PlayerStats> players)
        {
            new StatsRepo().SetPlayerStats(players);
        }

        [HttpGet]
        [Route("wmcb/teamPlayers")]
        public List<WmcbUser> GetTeamPlayers(int teamId)
        {
            return new UsersRepo().GetTeamPlayers(teamId);
        }
    }
}
