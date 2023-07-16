using AlmondWeb.WebApp.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlmondWeb.WebApp.Controllers
{
    [Authorize, Exc]
    public class AdminController : Controller
    {
        [isAdmin]
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
        public ActionResult NoAllow()
        {
            return View();
        }

        public ActionResult AlmondWebStatistic()
        {
            return View();
        }
        public ActionResult Logout()
        {
            return RedirectToAction("Login", "Home");
        }
    }
}