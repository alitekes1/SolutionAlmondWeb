using AlmondWeb.BusinessLayer;
using AlmondWeb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace AlmondWeb.WebApp.CacheHelper
{
    public class CacheHelper
    {
        public static List<ListTable> GetListDataformCache(int ownerId)
        {
            var list = WebCache.Get("MyList");
            if (list == null)
            {
                ListManager listManager = new ListManager();
                list = listManager.List(ownerId);
                WebCache.Set("MyList", list, 60, true);
            }
            return list;
        }
        public static void ClearCacheforList()
        {
            WebCache.Remove("MyList");
        }
        public static int CurrentUserID()
        {
            var myData = HttpContext.Current.Session["userId"];
            int currentUserID = Convert.ToInt32(myData);

            return currentUserID;
        }
    }
}