using AlmondWeb.BusinessLayer;
using AlmondWeb.BusinessLayer.ViewModels;
using AlmondWeb.Entities;
using AlmondWeb.Filters;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;

namespace AlmondWeb.WebApp.Controllers
{
    [Authorize, Exc]
    public class UserController : Controller
    {
        private ProfileManager pm = new ProfileManager();
        private SharedListManager slm = new SharedListManager();
        private SharedDataManager sdm = new SharedDataManager();
        private ListManager lm = new ListManager();
        private UserManager um = new UserManager();
        private ContactManager cm = new ContactManager();
        private int currentUserID = CacheHelper.CurrentUserID();

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
                ErrorResult<AlmondUserTable> errorList = um.RegisterUser(model);//crud işlemlerini çağıran manager sınıfındaki register methodunu çağırıyoruz.
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
                    return RedirectToAction(nameof(Login));
                }
            }
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult RegisterSuccess()
        {
            return View();
        }
        [HttpGet, AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost, AllowAnonymous]
        public ActionResult Login(LoginModel modal)
        {
            if (ModelState.IsValid)
            {
                var errorresult = um.LoginUser(modal);
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
                    return RedirectToAction("../Anasayfa");
                }
            }
            return View(modal);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            Session.Clear();
            Request.Cookies.Clear();
            Response.Cookies.Clear();

            return RedirectToAction(nameof(Login)); // Corrected this line
        }

        public ActionResult CreateProfile()
        {
            if (pm.CreateProfil(currentUserID) > 0)
            {
                return RedirectToAction(nameof(ProfileUpdate), new { ownerId = currentUserID }); // Corrected this line
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
        [HttpGet]
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
            return RedirectToAction("Error", "Home");

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
                if (result > 0)
                {
                    return View(nameof(PublicProfile));
                }
            }
            return RedirectToAction("Error", "Home");
        }
        [HttpGet]
        public ActionResult PublicAllList()//TODO: liste paylaşma hakkında SSS hazırlanacak. acordion şeklinde
        {
            List<SharedListTable> list = slm.RelationListAll(currentUserID);//paylaşılan tüm listeleri gönderiyor.
            return View(list);
        }
        public PartialViewResult SearchList(string searchText)
        {
            List<SharedListTable> list = slm.FindRelotionList(searchText, currentUserID);
            return PartialView("Partials/_SearchList", list);
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

                    SaveDatafromList(listId.Value, newlist);

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

        private void SaveDatafromList(int listId, SharedListTable newlist)//listenin içindeki tüm verileri kaydediyoruz.
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
        [AllowAnonymous, HttpGet]
        public PartialViewResult SuggestNewFeature()
        {
            return PartialView("Partials/_SuggestNewFeaturePartial");
        }
        [HttpPost]
        public int SuggestNewFeature(ContactTable suggest)
        {
            if (suggest != null)
            {
                suggest.contactType = "--Tavsiye--";
                int result = cm.Insert(suggest);
                return result;
            }
            else
            {
                return -1;
            }
        }
    }
}