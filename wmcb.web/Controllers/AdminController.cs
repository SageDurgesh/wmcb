using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wmcb.model;
using wmcb.repo;
using wmcb.model.Security;
using wmcb.model.View;
using wmcb.model.Data;

namespace wmcb.web.Controllers
{
    
    public class AdminController : Controller
    {
        // GET: Admin
        [WMCBAdminAuthorize("Admin")]
        public ActionResult NewsFeed()
        {
            return View();
        }
        [WMCBAdminAuthorize("Admin")]
        public List<NewsView> GetLatestNewsFeed(int count)
        {
            return new NewsFeedRepo().getLastestNewsFeeds(count);
        }
        [WMCBAdminAuthorize("Admin", "League Official", "Team Official")]
        public ActionResult MatchScore()
        {
            WmcbPrincipal user = (WmcbPrincipal)HttpContext.User;
            var scoremodel = new MatchRepo().GetMatchScoreView(user);
            return View(scoremodel);
        }
        [WMCBAdminAuthorize("Admin", "League Official")]
        public ActionResult ReviewScore()
        {
            WmcbPrincipal user = (WmcbPrincipal)HttpContext.User;
            MatchScoreView scoremodel = new MatchScoreView();
            if (user != null && user.roles != null && user.IsInRole("League Official"))
            {
                scoremodel.hasPermission = true;
                scoremodel.TeamID = user.TeamId.HasValue ? user.TeamId.Value : 0;
                scoremodel.TeamName = user.TeamName;
            }            
            return View(scoremodel);
        }
        [WMCBAdminAuthorize("Admin")]
        public ActionResult CreateTeam()
        {
            return PartialView();
        }
        [WMCBAdminAuthorize("Admin", "League Official", "Team Official")]
        public ActionResult TeamManager()
        {
            var teamID = ((WmcbPrincipal)HttpContext.User).TeamId;
            if (teamID.HasValue)
            {
                var team = new TeamRepo().GetTeam(teamID.Value);
                return View(team);
            }
            return View();
        }

    }
}