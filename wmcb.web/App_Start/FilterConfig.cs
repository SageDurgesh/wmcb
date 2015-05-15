using System.Web;
using System.Web.Mvc;
using wmcb.web.Controllers;

namespace wmcb.web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            
            filters.Add(new AuthorizeAttribute());
            filters.Add(new HandleErrorAttribute());
        }


    }
}
