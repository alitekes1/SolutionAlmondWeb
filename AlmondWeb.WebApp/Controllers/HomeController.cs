using AlmondWeb.BusinessLayer;
//using AlmondWeb.data.RepositoryPattern;
using AlmondWeb.BusinessLayer.ViewModels;
using AlmondWeb.Entities;
using Microsoft.Ajax.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

/* business katmanda oluşturduğumuz manager classlar crud işlemlerini yapacaklar.
 
 
 */
namespace AlmondWeb.WebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private UserManager userManager = new UserManager();
        private DataManager dataManager = new DataManager();
        private ListManager listManager = new ListManager();
        public int currentUserId = CacheHelper.CacheHelper.CurrentUserID();
        private static int i = -1;
        public ActionResult MainPage()
        {
            return View();
        }
        public ActionResult GetQuestionAnswer()
        {
            i++;
            List<AlmondDataTable> al = dataManager.ListwithExpression(x => x.Owner.Id == currentUserId && !x.isDeleted).OrderBy(x => x.puan).ToList();
            if (al.Count <= i)
            {
                return Content("<script type='text/javascript'>alert('Listedeki tüm verileri gözden geçirdiniz.Tebrikler.');</script>");
                //return RedirectToAction("Error");
            }
            else
            {
                return PartialView("Partials/_GetQuestionAnswerPartial", al[i]);
            }
        }
        [HttpGet]
        public ActionResult puanUpdate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult puanUpdate(int? dataId, int? puanValue)
        {
            if (dataId != null && puanValue != null)
            {
                AlmondDataTable data = dataManager.FindwithExpression(x => x.Id == dataId && !x.isDeleted);
                data.puan += (int)puanValue;//burdan sonra diğer soruya geçmeli
                int result = dataManager.Update(data);
                return RedirectToAction(nameof(MainPage));
            }
            else
            {
                return RedirectToAction(nameof(Error));
            }
        }
        [HttpGet, AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost, AllowAnonymous]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                ErrorResult<AlmondUserTable> errorList = userManager.RegisterUser(model);//kontrol işlemlerini çağıran manager sınıfındaki register methodunu çağırıyoruz.
                if (errorList.errorList.Count > 0)
                {
                    foreach (var item in errorList.errorList)
                    {
                        ModelState.AddModelError("", item);
                        return View(model);
                    }
                }
                else
                {
                    return RedirectToAction(nameof(RegisterSuccess));
                }
            }
            return View(model);
        }

        [HttpGet, AllowAnonymous]
        public ActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost, AllowAnonymous]
        public ActionResult ForgetPassword(RegisterModel newPassword)
        {
            if (ModelState.IsValid)
            {
                return View("RegisterSuccess");//TODO: şifremi unuttum işlemleri yapılacak
            }
            return View(newPassword);
        }

        [HttpGet, AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost, AllowAnonymous]
        public ActionResult Login(LoginModel modal)
        {
            if (ModelState.IsValid)
            {
                var errorresult = userManager.LoginUser(modal);
                if (errorresult.errorList.Count > 0)
                {
                    foreach (var item in errorresult.errorList)
                    {
                        ModelState.AddModelError("", item);
                    }
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(errorresult.resultModel.Name, false);
                    HttpContext.Session.Add("userId", errorresult.resultModel.Id);
                    Session["user"] = modal;
                    return RedirectToAction(nameof(MainPage));
                }
            }
            return View(modal);
        }
        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            Session.Clear();
            Request.Cookies.Clear();
            Response.Cookies.Clear();

            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult AddData()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddData(AlmondDataTable alm)
        {
            if (alm.question != null && alm.answer != null && alm.List.Id > 0)//almamız gerekn bilgileri alıp almadığımızı kontrol ediyoruz.
            {
                alm.puan = 0;
                alm.Owner = userManager.FindwithOwnerId(currentUserId);
                alm.List = listManager.FindwithOwnerId(alm.List.Id);
                dataManager.Insert(alm);
            }
            else
            {
                return View(alm);
            }
            return RedirectToAction(nameof(FillTablewithData));
        }

        [HttpGet]
        public ActionResult UpdateData()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpdateData(UserQueAnswListModel data)
        {
            if (ModelState.IsValid)
            {
                if (data.update_Id != null)
                {
                    AlmondDataTable updateData = dataManager.FindwithExpression(x => x.Id == data.update_Id && !x.isDeleted);
                    updateData.answer = data.answer;
                    updateData.question = data.question;
                    updateData.List.Id = data.list_Id;
                    int result = dataManager.Update(updateData);
                    if (result > -1)
                    {
                        return RedirectToAction(nameof(FillTableDataForUpdate));
                    }
                }
                return RedirectToAction(nameof(Error));
            }
            return View(data);
        }
        [HttpGet]
        public ActionResult UpdateList()
        {
            return View();
        }
        [HttpPost]
        public int UpdateList(string listName, int? id)
        {
            if (id != null && !listName.IsNullOrWhiteSpace())
            {
                ListTable list = listManager.FindwithExpression(x => x.Id == id && !x.isDeleted);
                if (list != null)
                {
                    list.listName = listName;
                    int result = listManager.Update(list);
                    return result;
                }
            }
            return -1;
        }
        [HttpGet]
        public ActionResult DeleteData()
        {
            return View();
        }
        [HttpPost]
        public int DeleteData(int? id)
        {
            int result;
            if (id != null)
            {
                var deletedata = dataManager.FindwithExpression(x => x.Id == id && !x.isDeleted);
                if (deletedata != null)
                {
                    result = dataManager.Delete(deletedata);
                    return result;
                }
            }
            return -1;
        }
        [HttpGet]
        public ActionResult DeleteList()
        {
            return View();
        }
        [HttpPost]
        public int DeleteList(int? id)
        {
            if (id.HasValue)
            {
                ListTable list = listManager.FindwithExpression(x => x.Id == id);//silinecek veriyi buluyoruz.
                ListTable deleteList1 = listManager.FindwithExpression(x => x.Id == 1);//silinenler listesini buluyoruz.
                if (list != null)
                {
                    List<AlmondDataTable> deleteList = dataManager.ListwithExpression(x => x.List.Id == id).ToList();
                    foreach (var item in deleteList)
                    {
                        item.List = deleteList1;//bulduğumuz verinin listesini silinenler listesi olarak değiştiriyoruz.
                        dataManager.Update(item);
                    }
                    int result = listManager.Delete(list);
                    return result;
                }
            }
            return -1;
        }
        [HttpGet]
        public ActionResult CreateList()
        {
            return View();
        }
        [HttpPost]
        public int CreateList(string listNm)
        {
            if (!listNm.IsNullOrWhiteSpace())
            {
                ListTable newList = new ListTable();
                newList.listName = listNm;
                newList.Owner = userManager.FindwithOwnerId(currentUserId);
                int result = listManager.Insert(newList);
                return result;
            }
            return -1;
        }
        public ActionResult AllData()
        {
            return View();
        }
        public ActionResult ListOperations()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Contact()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Error()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult RegisterSuccess()
        {
            return View();
        }
        public PartialViewResult SuggestNewFeature()
        {
            return PartialView("Partials/_SuggestNewFeaturePartial");
        }
        public PartialViewResult Shortcuts()
        {
            return PartialView("Partials/_ShortcutsPartial");
        }
        public PartialViewResult FillTablewithData()
        {
            return PartialView("Partials/_AddDataTablePartial");
        }
        public PartialViewResult FillTableDataForUpdate()
        {
            return PartialView("Partials/_UpdateTablePartial");
        }
        public PartialViewResult FillTableForDelete()
        {
            return PartialView("Partials/_DeleteandAllDataTable");
        }
        public PartialViewResult FillTableForListOperations()
        {
            return PartialView("Partials/_ListOperationsTablePartial");
        }
    }
}