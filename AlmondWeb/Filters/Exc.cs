using AlmondWeb.BusinessLayer;
using System.Web.Mvc;

namespace AlmondWeb.Filters
{
    public class Exc : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            int currentUserId = CacheHelper.CurrentUserID();
            if (currentUserId == 0)
            {
                filterContext.Result = new RedirectResult("Hata");
            }
            else
            {

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