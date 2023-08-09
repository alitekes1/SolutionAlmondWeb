using AlmondWeb.BusinessLayer;
using System.Web.Mvc;
namespace AlmondWeb.WebApp.Filters
{
    public class isAdmin : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            UserManager userManager = new UserManager();
            int currentUserId = CacheHelper.CacheHelper.CurrentUserID();
            if (currentUserId > 0)
            {
                var a = userManager.FindwithExpression(x => x.Id == currentUserId);
                if (!a.isAdmin)//TODO: buraya bi bak
                {
                    filterContext.Result = new RedirectResult("/Admin/NoAdmin");
                }
                else
                {
                    new RedirectResult("/Admin/AllUser");//TODO:admin olmayan kişilerin yönlendirileceği sayfa oluşturulacak
                }
            }
            else
            {
                new RedirectResult("../Home/Login");
            }
        }
    }
}