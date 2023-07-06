using AlmondWeb.BusinessLayer;
using AlmondWeb.BusinessLayer.RepositoryPattern;
using AlmondWeb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


/* business katmanda oluşturduğumuz manager classlar crud işlemlerini yapacaklar.
 
 
 */
namespace AlmondWeb.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult MainPage()
        {
            //BusinessLayer.Deneme db = new BusinessLayer.Deneme();
            Repository<ListTable> repo_list = new Repository<ListTable>();
            SelectList listItems = new SelectList(repo_list.List(), "id", "listName");//value ve text in ne olduğunu belirliyoruz.
            ViewData["list"] = listItems;//liste işlemleri

            DataManager dm = new DataManager();
            return View(dm.getSingleData(x => x.isDeleted ==false));
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
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