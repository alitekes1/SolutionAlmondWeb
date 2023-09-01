using AlmondWeb.BusinessLayer;
using System;
using System.Web.Mvc;

namespace AlmondWeb.Filters
{
    public class isAdmin : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            UserManager userManager = new UserManager();
            int currentUserId = CacheHelper.CurrentUserID();
            if (currentUserId > 0)
            {
                var a = userManager.FindwithExpression(x => x.Id == currentUserId);
                if (!a.isAdmin)
                {
                    filterContext.Result = new RedirectResult("/Admin/NoAdminError");
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