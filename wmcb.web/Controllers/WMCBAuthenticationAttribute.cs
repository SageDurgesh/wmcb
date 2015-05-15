using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace wmcb.web.Controllers
{
    public class WMCBAuthenticationAttribute: ActionFilterAttribute, IAuthenticationFilter
    {
        public  void OnAuthentication(AuthenticationContext filterContext)
        {
            //Logic for authenticating a user
        }
        //Runs after the OnAuthentication method
        public  void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            var user = filterContext.HttpContext.User;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}