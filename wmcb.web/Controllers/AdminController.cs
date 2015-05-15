using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wmcb.model;
using wmcb.repo;

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
    }
}