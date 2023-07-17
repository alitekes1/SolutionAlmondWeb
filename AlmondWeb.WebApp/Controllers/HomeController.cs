using AlmondWeb.BusinessLayer;
//using AlmondWeb.data.RepositoryPattern;
using AlmondWeb.BusinessLayer.ViewModels;
using AlmondWeb.Entities;
using AlmondWeb.WebApp.CacheHelper;
using AlmondWeb.WebApp.Filters;
using Antlr.Runtime.Tree;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.EnterpriseServices;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Generator;
using System.Web.Security;
using System.Web.SessionState;
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
        private int currentUserId = CacheHelper.CacheHelper.CurrentUserID();

        public ActionResult MainPage()
        {
            return View();
        }
        public ActionResult GetQuestionAnswer()
        {
            AlmondDataTable almondData = dataManager.FindwithOwnerId(currentUserId);// TODO: bu işlem için bir manager yapılacak.
            return PartialView("Partials/_GetQuestionAnswerPartial", almondData);
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
        public ActionResult Login(LoginModal modal)
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
        public ActionResult CreateData()
        {
            return PartialView();
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
                    AlmondDataTable updateData = dataManager.FindwithExpression(x => x.Id == data.update_Id);
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
        public ActionResult DeleteData()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DeleteData(int? id)
        {
            if (id != null)
            {
                var deletedata = dataManager.FindwithExpression(x => x.Id == id);
                if (deletedata != null)
                {
                    if (dataManager.Delete(deletedata) > 0)
                    {
                        return View();//işlem başarılı
                    }
                    else
                    {
                        //işlem başarısız
                    }
                }
            }
            return RedirectToAction(nameof(Error));
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
        public ActionResult SuggestFeature()
        {
            return View();
        }
        public PartialViewResult FillTablewithData()
        {
            return PartialView("Partials/_DataTablePartial");
        }
        public PartialViewResult FillTableDataForUpdate()
        {
            return PartialView("Partials/_UpdateDeleteDataTable");
        }
    }
}