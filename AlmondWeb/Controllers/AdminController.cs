using AlmondWeb.BusinessLayer;
using AlmondWeb.Entities;
using AlmondWeb.Filters;
using System.Web.Mvc;

namespace AlmondWeb.WebApp.Controllers
{
    [Authorize, Exc]
    public class AdminController : Controller
    {
        private ContactManager cm = new ContactManager();
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
        public int DeactiveAccount(int? id)
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
        public int ActiveAccount(int? id)
        {
            if (id != null)
            {
                AlmondUserTable user = um.FindwithExpression(x => x.Id == id);
                user.isActive = true;
                result = um.Update(user);
                return result;
            }
            else
            {
                return -1;
            }
        }
        [HttpPost]
        public int AssignAdmin(int? id)
        {
            if (id != null)
            {
                AlmondUserTable user = um.FindwithExpression(x => x.Id == id);
                user.isAdmin = true;
                result = um.Update(user);
                return result;
            }
            else
            {
                return -1;
            }
        }
        [HttpPost]
        public int RemoveAdmin(int? id)
        {
            if (id != null)
            {
                AlmondUserTable user = um.FindwithExpression(x => x.Id == id);
                user.isAdmin = false;
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
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost, AllowAnonymous]
        public int Contact(ContactTable contact)
        {
            if (contact != null)
            {
                int result = cm.Insert(contact);
                return result;
            }
            else { return -1; }
        }
        public PartialViewResult AllUserTablePartial()
        {
            return PartialView("TablePartial/_AllUserTablePartial");
        }
        public PartialViewResult AdminTablePartial()
        {
            return PartialView("TablePartial/_AdminTablePartial");
        }
    }
}