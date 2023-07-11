using AlmondWeb.BusinessLayer;
using AlmondWeb.BusinessLayer.RepositoryPattern;
using AlmondWeb.BusinessLayer.ViewModels;
using AlmondWeb.Entities;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;


/* business katmanda oluşturduğumuz manager classlar crud işlemlerini yapacaklar.
 
 
 */
namespace AlmondWeb.WebApp.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult MainPage()
        {
            ////BusinessLayer.Deneme db = new BusinessLayer.Deneme();
            //DataManager dm = new DataManager();

            return View();//sayfaya model gönderiyor
        }
        public ActionResult GetQuestionAnswer()
        {
            Repository<AlmondDataTable> repo_data = new Repository<AlmondDataTable>();
            List<AlmondDataTable> almondData = repo_data.List(x => x.Id == 3);
            return PartialView("Partials/_GetQuestionAnswerPartial", almondData);
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        UserManager userManager = new UserManager();
        [HttpPost]
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


        [HttpGet]
        public ActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgetPassword(RegisterModel newPassword)
        {
            if (ModelState.IsValid)
            {
                return View("RegisterSuccess");
            }
            return View(newPassword);
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
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
                    return RedirectToAction("MainPage");
                }
            }
            return View(modal);
        }


        public ActionResult AddData()
        {
            return View();
        }
        public ActionResult UpdateData()
        {
            return View();
        }
        public ActionResult DeleteData()
        {
            return View();
        }
        public ActionResult AllData()
        {
            return View();
        }
        public ActionResult ListOperations()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }

    }
}