using AlmondWeb.BusinessLayer;
using AlmondWeb.BusinessLayer.ViewModels;
using AlmondWeb.Entities;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace AlmondWeb.WebApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private ProfileManager pm = new ProfileManager();
        private SharedListManager slm = new SharedListManager();
        private SharedDataManager sdm = new SharedDataManager();
        private ListManager lm = new ListManager();
        private UserManager um = new UserManager();
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
            if (ishaveprofil != null)//profil varsa
            {
                return View();
            }
            else//profil yoksa
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
            List<SharedListTable> list = slm.RelationListAll(currentUserID);//current user a ait dataları listeliyoruz.
            return View(list);
        }
        public PartialViewResult SearchList(string searchText)
        {
            List<SharedListTable> list = slm.FindRelotionList(searchText, currentUserID);
            return PartialView("Partials/_PublicListPartial", list);
        }
        [HttpPost]
        public int SaveList(int? listId)
        {
            if (listId != null)
            {
                SharedListTable newlist = new SharedListTable();
                var shlist = slm.FindwithExpression(x => x.listId == listId);

                newlist.profileId = currentUserID;
                newlist.listId = shlist.List.Id;
                newlist.OwnerId = shlist.Owner.Id;

                if (isNotSavedList(newlist.OwnerId, listId.Value))
                {
                    int result = slm.Insert(newlist);

                    SaveDataToList(listId.Value, newlist);

                    return result;
                }
            }
            return -1;
        }

        private bool isNotSavedList(int ownerId, int ListId)
        {
            SharedListTable i = slm.FindwithExpression(x => x.profileId == currentUserID && x.listId == ListId && x.OwnerId == ownerId);
            return i == null ? true : false;
        }

        private void SaveDataToList(int listId, SharedListTable newlist)//listenin içindeki tüm verileri kaydediyoruz.
        {
            var list = slm.FindwithExpression(x => x.listId == listId).List.DataTables;
            for (int i = 0; i < list.Count; i++)
            {
                SharedDataTable data = new SharedDataTable();
                data.question = list[i].question;
                data.answer = list[i].answer;
                data.SharedList = newlist;
                data.SharedList.profileId = currentUserID;
                sdm.Insert(data);
            }
        }
    }
}