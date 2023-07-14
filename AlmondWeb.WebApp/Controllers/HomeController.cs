using AlmondWeb.BusinessLayer;
//using AlmondWeb.data.RepositoryPattern;
using AlmondWeb.BusinessLayer.ViewModels;
using AlmondWeb.Entities;
using Antlr.Runtime.Tree;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.EnterpriseServices;
using System.Net;
using System.Net.Sockets;
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
        UserManager userManager = new UserManager();
        DataManager dataManager = new DataManager();
        [AllowAnonymous]
        public ActionResult MainPage()
        {
            return View();
        }
        public ActionResult GetQuestionAnswer()
        {
            var almondData = dataManager.Find(x => x.Id == 3);// TODO: bu işlem için bir manager yapılacak.
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
                    return RedirectToAction("RegisterSuccess");
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
                    return RedirectToAction("MainPage");
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
        public ActionResult AddData(AlmondDataTable data)
        {
            if (ModelState.IsValid)
            {
                if (data != null)
                {
                    if (dataManager.Insert(data) > 0)
                    {
                        return View(data);
                    }
                    else
                    {
                        return RedirectToAction(nameof(Error));
                    }
                }
                return View(data);
            }
            return View(data);
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult UpdateData()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult UpdateData(UserQueAnswListModel data)
        {
            if (ModelState.IsValid)
            {
                if (data != null)
                {
                    //int result = dataManager.Update(data);
                    //if (result != -1)
                    {
                        return View(data);//TODO:işlem başarılı toastr çıkacak
                    }
                    return RedirectToAction("Error");
                }
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
                var deletedata = dataManager.Find(x => x.Id == id);
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
            return RedirectToAction("Error", "Home");
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
        //public ActionResult FillTablewithData()
        //{
        //    return PartialView("Partials/GetAllDataPartial",);
        //}
    }
}