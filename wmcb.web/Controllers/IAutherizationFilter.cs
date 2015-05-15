using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace wmcb.web.Controllers
{
    public interface IAutherizationFilter
    {
        void OnAuthorization(AuthorizationContext filterContext);
    }
}