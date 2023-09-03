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
        private SharedDataManager sdm = new SharedDataManager();
        public int result = 0;
        [HttpGet]
        public ActionResult AllUser()
        {
            return View();
        }
        public ActionResult AllData()
        {
            return View();
        }
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
        [AllowAnonymous]
        public ActionResult NoAdminError()
        {
            return View();
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

        [HttpGet]
        public ActionResult RemoveNullDatainSharedDataTable()
        {
            return View(0);
        }
        [HttpPost]
        public ActionResult RemoveNullDatainSharedDataTable(int? a)
        {
            var datalist = sdm.ListwithExpression(x => x.SharedList == null);
            int result = sdm.RemoveNullDatainSharedDataTable(datalist);
            return View(result);
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