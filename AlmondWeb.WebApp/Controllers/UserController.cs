﻿using AlmondWeb.BusinessLayer;
using AlmondWeb.BusinessLayer.ViewModels;
using AlmondWeb.Entities;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.UI.WebControls;

namespace AlmondWeb.WebApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private ProfileManager pm = new ProfileManager();
        private ProfileListManager plm = new ProfileListManager();
        private UserManager um = new UserManager();
        private ListManager lm = new ListManager();
        private int currentUserID = CacheHelper.CacheHelper.CurrentUserID();

        public ActionResult CreateProfile()
        {
            if (pm.CreateProfil(currentUserID) > 0)
            {
                return RedirectToAction(nameof(ProfileUpdate), currentUserID);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult PrivateProfile()
        {
            var ishaveprofil = pm.FindwithExpression(x => x.Id == currentUserID);
            if (ishaveprofil != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction(nameof(CreateProfile), currentUserID);
            }
        }
        public ActionResult PublicProfile()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ProfileUpdate()
        {
            ProfileTable profil = pm.FindwithExpression(x => x.Owner.Id == currentUserID);
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
        public ActionResult PublicAllList()//TODO: liste paylaşma hakkında SSS hazırlanacak. acordion şeklinde
        {
            List<ProfListTable> list = plm.RelationListAll(currentUserID);//current user a ait dataları listeliyoruz.
            return View(list);
        }
        public PartialViewResult SearchList(string searchText)
        {
            List<ProfListTable> list = plm.FindRelotionList(searchText, currentUserID);
            return PartialView("Partials/_PublicListPartial", list);
        }
        [HttpPost]
        public int SaveList(int? listId)
        {
            if (listId != null)
            {
                ProfListTable data = new ProfListTable();
                data.listId = listId.Value;
                data.profileId = currentUserID;
                int result = plm.Insert(data);
                return result;
            }
            return -1;
        }
    }
}