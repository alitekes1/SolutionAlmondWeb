using AlmondWeb.BusinessLayer;
using AlmondWeb.BusinessLayer.ViewModels;
using AlmondWeb.Entities;
using System.Collections.Specialized;
using System.Web.Mvc;

namespace AlmondWeb.WebApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private ProfileManager pm = new ProfileManager();
        private UserManager um = new UserManager();
        public ActionResult CreateProfile(int? id)
        {
            if (id != null)
            {
                if (pm.CreateProfil(id) > 0)
                {
                    return RedirectToAction(nameof(ProfileUpdate), id);
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult PrivateProfile()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PrivateProfile(int id)
        {
            var ishaveprofil = pm.FindwithExpression(x => x.Id == id);
            if (ishaveprofil != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction(nameof(CreateProfile), id);
            }
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
        public ActionResult ProfileUpdate(int? id)
        {
            ProfileTable profil = pm.FindwithExpression(x => x.Owner.Id == id);
            if (profil != null)
            {
                return View(profil);
            }
            return RedirectToAction(nameof(PrivateProfile));
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