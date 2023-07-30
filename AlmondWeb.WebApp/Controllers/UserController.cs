using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlmondWeb.WebApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        public ActionResult PrivateProfile()
        {
            return View();
        }
        public ActionResult ProfileEdit()
        {
            return View();
        }
        public ActionResult PublicProfile()
        {
            return View();
        }
    }
}