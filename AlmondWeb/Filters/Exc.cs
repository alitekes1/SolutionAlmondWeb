using AlmondWeb.BusinessLayer;
using System.Web.Mvc;

namespace AlmondWeb.Filters
{
    public class Exc : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception.Message == "Nesne başvurusu bir nesnenin örneğine ayarlanmadı.")
            {
                filterContext.ExceptionHandled = true;
                filterContext.Result = new RedirectResult("Giris-Yap");
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