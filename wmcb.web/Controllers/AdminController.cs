using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wmcb.model;
using wmcb.repo;
using wmcb.model.Security;
using wmcb.model.View;
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
             ViewBag.TeamId = ((WmcbPrincipal)HttpContext.User).TeamId;
             ViewBag.IsLeagueOfficial = HttpContext.User.IsInRole("League Official");
             return View();
         }
    }
}