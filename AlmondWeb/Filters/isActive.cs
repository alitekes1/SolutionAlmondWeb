using AlmondWeb.BusinessLayer;
using System.Web.Mvc;

namespace AlmondWeb.Filters
{
    public class isAdmin : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext) { }
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            int currentUserId = CacheHelper.CurrentUserID();
            if (currentUserId > 0)
            {
                UserManager userManager = new UserManager();
                var a = userManager.FindwithExpression(x => x.Id == currentUserId);
                if (!a.isAdmin)
                {
                    filterContext.Result = new RedirectResult("YasakliBolge");
                }
                else
                {
                    new RedirectResult("/Admin/AllUser");
                }
            }
            else
            {
                new RedirectResult("../Home/Login");
            }
        }
    }
}
