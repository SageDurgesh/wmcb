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
        public List<Schedule> GetSchedule()
        {
            return new ScheduleRepo().GetSchedule();
        }
        [HttpGet]
        [Route("wmcb/upcominggames/{numofdays}")]
        public List<Schedule> GetUpcomingGames(int numofdays)
        {
            return new ScheduleRepo().GetUpcomingGames(numofdays);
        }
        
        [HttpGet]
        [Route("wmcb/points/{type}")]
        public List<TeamPoint> GetPoints(int type)
        {
            return new PointsRepo().GetPoints(type);
        }
    }
}
