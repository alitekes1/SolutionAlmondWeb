using AlmondWeb.BusinessLayer;
using AlmondWeb.BusinessLayer.ViewModels;
using AlmondWeb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace AlmondWeb.WebApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private ProfileManager pm = new ProfileManager();
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
        [HttpGet]
        public ActionResult ProfileUpdate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ProfileUpdate(ProfileModal profile)
        {
            ProfileTable profil = pm.FindwithOwnerId(profile.ownerId);
            if (profil != null)
            {
                profil.aboutmeText = profile.aboutmeText;
                profil.school = profile.school;
                profil.githubUrl = profile.githubUrl;
                profil.linkedinUrl = profile.linkedinUrl;
                profil.websiteUrl = profile.websiteUrl;
                profil.job = profile.job;
                profil.profileImageUrl = profile.profileImageUrl;
                int result = pm.Update(profil);
            }
            return RedirectToAction(nameof(PrivateProfile));
        }
        [HttpGet]
        public ActionResult PublicList()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PublicList(ProfileListTable list)
        {
            
            return View();
        }
    }
}