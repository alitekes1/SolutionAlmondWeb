using AlmondWeb.BusinessLayer;
using AlmondWeb.Entities;
using AlmondWeb.Filters;
using System.Web.Mvc;

namespace AlmondWeb.WebApp.Controllers
{
    [Authorize, Exc]
    public class AdminController : Controller
    {
        private UserManager um = new UserManager();
        private DataManager dm = new DataManager();
        private SharedListManager slm = new SharedListManager();
        private SharedDataManager sdm = new SharedDataManager();
        public int result = 0;
        [HttpGet, isAdmin]
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
        [AllowAnonymous]
        public ActionResult NoAdminError()
        {
            return View();
        }
        [HttpPost, isAdmin]
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
        [HttpPost, isAdmin]
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
        [HttpPost, isAdmin]
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
        [HttpPost, isAdmin]
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
        [HttpPost, isAdmin]
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
        [HttpPost, isAdmin]
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

        [HttpGet, isAdmin]
        public ActionResult RemoveNullDatainSharedDataTable()
        {
            return View(0);
        }
        [HttpPost, isAdmin]
        public ActionResult RemoveNullDatainSharedDataTable(int? a)
        {
            var datalist = sdm.ListwithExpression(x => x.SharedList == null);
            int result = sdm.RemoveNullDatainSharedDataTable(datalist);
            return View(result);
        }
        [isAdmin]
        public PartialViewResult AllUserTablePartial()
        {
            return PartialView("TablePartial/_AllUserTablePartial");
        }
        [isAdmin]
        public PartialViewResult AdminTablePartial()
        {
            return PartialView("TablePartial/_AdminTablePartial");
        }
    }
}