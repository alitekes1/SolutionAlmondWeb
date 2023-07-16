using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace AlmondWeb.WebApp.Filters
{
    public class isAuth : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (HttpContext.Current.Session == null)
            {
                filterContext.Result = new RedirectResult("/Home/Login");
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //mesela veri tabanına log lama yapılabilir.
        }
    }
}