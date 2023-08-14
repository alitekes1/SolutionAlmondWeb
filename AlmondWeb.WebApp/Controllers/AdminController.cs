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
        private DataManager dm = new DataManager();
        private SharedListManager slm = new SharedListManager();
        public int result = 0;
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
        [HttpPost]
        public int DeleteUser(int? id)
        {
            if (id != null)
            {
                AlmondUserTable user = um.FindwithExpression(x => x.Id == id);
                user.isActive = false;
                result = um.Update(user);
                return result;
            }
            else
            {
                return -1;
            }
        }
        [HttpPost]
        public int DeleteData(int? id)
        {
            if (id != null)
            {
                AlmondDataTable data = dm.FindwithExpression(x => x.Id == id);
                if (data != null)
                {
                    result = dm.Delete(data);
                    return result;
                }
            }
            return -1;
        }
        [HttpPost]
        public int RemoveSharedList(int? id)
        {
            if (id != null)
            {
                SharedListTable list = slm.FindwithExpression(x => x.Id == id);
                if (list != null)
                {
                    result = slm.Delete(list);
                    return result;
                }
            }
            return -1;
        }
    }
}