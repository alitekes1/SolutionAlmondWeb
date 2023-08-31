using System;
using System.Web;

namespace AlmondWeb.BusinessLayer
{
    public static class CacheHelper
    {
        public static int CurrentUserID()
        {
            var myData = HttpContext.Current.Session["userId"];
            int currentUserID = Convert.ToInt32(myData);

            return currentUserID;
        }
    }
}
