using AlmondWeb.BusinessLayer;
using AlmondWeb.Entities;
using AlmondWeb.Filters;
using System.Collections.Generic;
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
        private ContactManager cm = new ContactManager();
        private ListManager lm = new ListManager();
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
        public int RemoveSharedList(int? id)//bu liste admin tarafından listeyi kaldırmak için
        {
            if (id != null)
            {
                SharedListTable list = slm.FindwithExpression(x => x.listId == id);
                if (list != null)
                {
                    result = slm.DeleteList(list);
                    return result;
                }
            }
            return -1;
        }
        [HttpPost, isAdmin]
        public int DeleteMessage(int? id)
        {
            if (id != null)
            {
                ContactTable message = cm.FindwithExpression(x => x.Id == id);
                if (message != null)
                {
                    result = cm.DeleteList(message);
                    return result;
                }
            }
            return -1;
        }
        [HttpPost, isAdmin]
        public int PermanentDeleteData(int? dataId)
        {
            if (dataId != null)
            {
                AlmondDataTable data = dm.FindwithOwnerId(dataId.Value);
                if (data != null)
                {
                    int result = dm.DeleteList(data);
                    return result;
                }
            }
            return -1;
        }
        [HttpPost, isAdmin]
        public int PermanentDeleteList(int? id)
        {
            if (id != null)
            {
                List<AlmondDataTable> data = dm.ListwithExpression(x => x.List.Id == id);
                List<SharedDataTable> sdata = sdm.ListwithExpression(x => x.SharedList.Id == id);
                ListTable list = lm.FindwithExpression(x => x.Id == id);
                if (list != null && data != null)
                {
                    int res0 = sdm.RemoveListRange(sdata);//paylaşılan data table dan siliyoruz
                    int res = dm.RemoveListRange(data);//almonddata dan siliyoruz
                    int res2 = RemoveSharedList(id);//sharedlisttable dan siliyoruz
                    int res3 = lm.DeleteList(list);//list table dan siliyoruz
                    return res0 > 0 || res > 0 || res2 > 0 || res3 > 0 ? 1 : 0;
                }
            }
            return -1;
        }
        [HttpPost, isAdmin]
        public int AddDatatoAliList(string question, string answer)
        {
            if (question != null && answer != null)
            {
                ListTable list = lm.FindwithExpression(x => x.Owner.Username == "alitekes");
                AlmondUserTable ali = um.FindwithExpression(x => x.Username == "alitekes");
                AlmondDataTable data = new AlmondDataTable
                {
                    question = question,
                    answer = answer,
                    List = list,
                    Owner = ali
                };
                int result = dm.Insert(data);
                return result;
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