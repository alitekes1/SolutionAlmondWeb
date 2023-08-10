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
        [isAdmin]
        public ActionResult Admins()
        {
            return View();
        }
        [isAdmin]
        public ActionResult AllMessages()
        {
            return View();
        }
        [isAdmin]
        public ActionResult AlmondWebStatistic()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult NoAdminError()
        {
            return RedirectToAction("Error", "Home", new { id = 1 });
        }
    }
}