using AlmondWeb.WebApp.Filters;
using System.Web.Mvc;

namespace AlmondWeb.WebApp.Controllers
{
    [Authorize, Exc]
    public class AdminController : Controller
    {
        [isAdmin]
        public ActionResult AllUser()
        {
            return View();
        }
        [isAdmin]
        public ActionResult AllData()
        {
            return View();
        }
        [isAdmin]
        public ActionResult AllList()
        {
            return View();
        }
        public ActionResult Admins()
        {
            return View();
        }
        public ActionResult AllMessages()
        {
            return View();
        }
        public ActionResult AlmondWebStatistic()
        {
            return View();
        }
        public ActionResult NoAdminError()
        {
            return View();
        }
    }
}