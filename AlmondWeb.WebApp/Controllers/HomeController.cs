using AlmondWeb.BusinessLayer;
using AlmondWeb.BusinessLayer.ViewModels;
using AlmondWeb.Entities;
using Microsoft.Ajax.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace AlmondWeb.WebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private UserManager userManager = new UserManager();
        private DataManager dataManager = new DataManager();
        private ListManager listManager = new ListManager();
        private ContactManager contactManager = new ContactManager();
        private ProfileManager profileManager = new ProfileManager();
        private SharedListManager sharedListManager = new SharedListManager();
        private int currentUserId = CacheHelper.CacheHelper.CurrentUserID();
        private static List<UserQueAnswListModel> dataJson = new List<UserQueAnswListModel>();

        public ActionResult MainPage()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetQuestionAnswerJson(int listId)
        {
            dataJson = getList(listId);
            return Json(dataJson, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdatePuan(int? dataId, int puan, int? listId)
        {
            if (dataJson == null)
            {
                return null;
            }
            else
            {
                if (puan > 0 || dataId > 0)
                {
                    AlmondDataTable data = dataManager.FindwithExpression(x => x.Id == 102 && !x.isDeleted);
                    data.puan += puan;//burdan sonra diğer soruya geçmeli
                    int result = dataManager.Update(data);
                    dataJson = getList(listId);
                    return Json(dataJson, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return null;
                }
            }
        }
        private List<UserQueAnswListModel> getList(int? listId)
        {
            List<AlmondDataTable> datalist = dataManager.ListwithExpression(x => x.List.Id == listId).OrderBy(x => x.puan).ToList();
            for (int i = 0; i < datalist.Count(); i++)
            {
                UserQueAnswListModel data = new UserQueAnswListModel();
                data.question = datalist[i].question;
                data.answer = datalist[i].answer;
                data.update_Id = datalist[i].Id;
                dataJson.Add(data);
            }
            return dataJson;
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
                ErrorResult<AlmondUserTable> errorList = userManager.RegisterUser(model);//crud işlemlerini çağıran manager sınıfındaki register methodunu çağırıyoruz.
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
                    return RedirectToAction(nameof(Login));
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
                    updateData.List = listManager.FindwithOwnerId(data.list_Id);
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
        public ActionResult CreateList()
        {
            return View();
        }
        [HttpPost]
        public int CreateList(string listNm, string listDesc, string listPub, string listPriv)
        {
            if (!listNm.IsNullOrWhiteSpace())
            {
                ListTable newList = new ListTable();
                newList.listName = listNm;
                newList.listDescription = listDesc;
                newList.isPublic = listPub == "1" ? true : false;
                newList.Owner = userManager.FindwithOwnerId(currentUserId);
                int result = listManager.Insert(newList);
                InsertToSharedListTable(newList.Id, newList.isPublic);
                return result;
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

                if (list != null)
                {
                    int result = listManager.DeleteList(list);
                    return result;
                }
            }
            return -1;
        }
        [HttpGet]
        public ActionResult UpdateList()
        {
            return View();
        }
        [HttpPost]
        public int UpdateList(string listName, int? id, string listDesc, string listPub, string listPriv)
        {
            if (id != null && !listName.IsNullOrWhiteSpace())
            {
                ListTable isThereList = listManager.FindwithExpression(x => x.Owner.Id == currentUserId && x.Id == id);
                SharedListTable updateSharedList = sharedListManager.FindwithExpression(x => x.listId == id && x.profileId == currentUserId);
                if (isThereList != null || updateSharedList != null)//eğer böyle bir liste varsa
                {
                    isThereList.listName = listName;
                    isThereList.listDescription = listDesc;

                    isThereList.isPublic = listPub == "1" ? true : false;

                    updateSharedList.isPublic = isThereList.isPublic;

                    int result = listManager.Update(isThereList);
                    int result2 = sharedListManager.Update(updateSharedList);
                    return result + result2;
                }
            }
            return -1;
        }
        public int InsertToSharedListTable(int? listId, bool ispublic)
        {
            if (listId != null)
            {
                SharedListTable profileList = new SharedListTable();
                profileList.profileId = currentUserId;
                profileList.listId = listId.Value;
                profileList.isPublic = ispublic;
                profileList.Owner = listManager.FindwithExpression(x => x.Id == listId).Owner;//liste sahibini verdik

                return sharedListManager.Insert(profileList);
            }
            else { return -1; }
        }

        public ActionResult AllData()
        {
            return View();
        }
        //TODO:kullanıcıların listelerim sayfasında kaydettiği listeler bulunmayacak. diğer yerlerda bulunacak.
        public ActionResult ListOperations()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost, AllowAnonymous]
        public int Contact(ContactTable contact)
        {
            if (contact != null)
            {
                int result = contactManager.Insert(contact);
                return result;
            }
            else { return -1; }
        }

        [AllowAnonymous]
        public ActionResult Error(int? id)
        {
            if (id == 1)
            {
                ViewData["errorCode"] = "403";
                ViewData["errorTitle"] = "Yetkiniz Bulunmamaktadır!";
                ViewData["errorMessage"] = "Bu alana sadece Adminler girebilir.Admin yetkiniz bulunmamaktadır.Daha da geç olmadan bu alanı terkedin!";
            }
            else
            {
                ViewData["errorMessage"] = "Bakmış olduğunuz sayfa kaldırılmış veya ismi değiştirilmiş olabilir.Dolayısıyla geçici bir süre kullanım dışıdır.Anasayfaya geri dönebilirsiniz veya bizimle iletişime geçebilirsiniz.";
                ViewData["errorTitle"] = "Sayfa bulunamadı!";
                ViewData["errorCode"] = "404";
            }
            return View();
        }
        [AllowAnonymous]
        public ActionResult RegisterSuccess()
        {
            return View();
        }
        [AllowAnonymous, HttpGet]
        public PartialViewResult SuggestNewFeature()
        {
            return PartialView("Partials/_SuggestNewFeaturePartial");
        }
        [HttpPost]
        public int SuggestNewFeature(ContactTable suggest)
        {
            if (suggest != null)
            {
                suggest.contactType = "--Tavsiye--";
                int result = contactManager.Insert(suggest);
                return result;
            }
            else
            {
                return -1;
            }

        }
        [AllowAnonymous]
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
            return PartialView("Partials/_DeleteDataTable");
        }
        public PartialViewResult FillTableForListOperations()
        {
            return PartialView("Partials/_ListOperationsTablePartial");
        }
        public PartialViewResult FillTableForAllData()
        {
            return PartialView("Partials/_AllDataTable");
        }
    }
}
