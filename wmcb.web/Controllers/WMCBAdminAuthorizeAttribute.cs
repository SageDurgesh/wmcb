using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace wmcb.web.Controllers
{
    public class WMCBAdminAuthorizeAttribute : AuthorizeAttribute
    {
        private string[] allowedroles;
        public WMCBAdminAuthorizeAttribute(params string[] roles)
        {
            this.allowedroles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            foreach (var role in allowedroles)
            {
                if (httpContext.User.IsInRole(role))
                    return true;
            }               
            return false;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}