using AlmondWeb.BusinessLayer;
using AlmondWeb.Entities;
using AlmondWeb.WebApp.Filters;
using System.Web.Mvc;

namespace AlmondWeb.WebApp.Controllers
{

    [Authorize, Exc]
    public class AdminController : Controller
    {

        private UserManager um = new UserManager();
        [isAdmin, HttpGet]
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
        [HttpGet]
        public int DeleteUser(int? id)
        {
            if (id != null)
            {
                AlmondUserTable user = um.FindwithExpression(x => x.Id == id);
                user.isActive = false;
                int result = um.Update(user);
                return result;
            }
            else
            {
                return -1;
            }
        }
    }
}