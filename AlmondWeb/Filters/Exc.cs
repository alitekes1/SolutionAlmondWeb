using System.Web;
using System.Web.Mvc;

namespace AlmondWeb.Filters
{
    public class Exc : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                filterContext.ExceptionHandled = true;
                filterContext.Result = new RedirectResult("https://almondweb.com.tr/Giris-Yap");
            }
            else
            {
                filterContext.ExceptionHandled = true;
                filterContext.Result = new RedirectToRouteResult(
                        new System.Web.Routing.RouteValueDictionary(
                            new
                            {
                                action = "Error",
                                contraller = "Home"
                            }));
            }
        }
    }
}