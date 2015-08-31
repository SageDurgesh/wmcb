using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wmcb.repo;
using wmcb.model.Security;
using Facebook;

namespace wmcb.web.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {            
            return View();
        }
        public ActionResult Index1()
        {
            return View();
        }
        public ActionResult Fixtures()
        {
            return View();
        }
        public ActionResult Teams()
        {
            return View();
        }
        public ActionResult LeaderBoard()
        {
            return View();
        }
        public ActionResult MatchResult()
        {
            return View();
        }
        public ActionResult PlayersOfficials()
        {
            return View();
        }
        public ActionResult Playoff()
        {
            return View();
        }
        public ActionResult Stats(int i)
        {
            
            
            switch(i){
                case 1: return PartialView("~/Views/Home/Partial/MatchResult.cshtml ");
                case 2: return PartialView("~/Partial/LeaderBoard");
                case 3: return PartialView("~/Partial/PlayerOfficials");
                 default:
                    return PartialView("~/Views/Home/Partial/MatchResult.cshtml"); 
            }
        }
        public ActionResult Grounds()
        {
            return View();
        }
        public ActionResult ViewMatchScore()
        {
            ViewBag.TeamId = ((WmcbPrincipal)HttpContext.User).TeamId;
            ViewBag.IsLeagueOfficial = HttpContext.User.IsInRole("League Official");
            return View();
        }       
        public ActionResult Gallery()
        {
            //var client = new FacebookClient();
            //dynamic me = client.Get
            return View();
        }
        public ActionResult Downloads()
        {
            return View();
        }
        public ActionResult Newsletter()
        {
            //ViewBag.Message = "Your newsletter description page.";
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Washington Metropolitan Cricket Board";

            return View();
        }
        public ActionResult Points()
        {
            return View();
        }
        public ActionResult News()
        {
            return View();
        }
    }
}