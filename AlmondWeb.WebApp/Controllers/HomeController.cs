using AlmondWeb.BusinessLayer;
using AlmondWeb.BusinessLayer.RepositoryPattern;
using AlmondWeb.BusinessLayer.ViewModels;
using AlmondWeb.Entities;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.EnterpriseServices;
using System.Web.Mvc;
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

        public ActionResult MainPage()
        {
            return View();
        }
        public ActionResult GetQuestionAnswer()
        {
            AlmondDataTable almondData = dataManager.getSingleData(x => x.Id == 3);// TODO: bu işlem için bir manager yapılacak.
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
                return View("RegisterSuccess");
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
            //UserManager userManager = new UserManager();//globale taşıdık.
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
            return RedirectToAction("Login");
        }
        public ActionResult AddData()
        {
            return View(dataManager.List());
        }

        public ActionResult UpdateData()
        {
            return View(dataManager.List());
        }
        public ActionResult DeleteData()
        {
            return View(dataManager.List());
        }
        public ActionResult AllData()
        {
            return View(dataManager.List(1));
        }
        public ActionResult ListOperations()
        {
            return View(dataManager.List());
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
        public ActionResult FillTablewithData()
        {
            var dataList = dataManager.List();
            return PartialView("Partials/GetAllDataPartial", dataList);
        }
    }
}