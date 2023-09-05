using AlmondWeb.BusinessLayer;
using AlmondWeb.BusinessLayer.ViewModels;
using AlmondWeb.Entities;
using AlmondWeb.Filters;
using System;
using System.Collections.Generic;
using System.IO;
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
                    FormsAuthentication.SetAuthCookie(errorresult.resultModel.Username, false);
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
        public ActionResult PublicProfile(string userName)
        {
            if (userName != null)
            {
                ProfileTable searchingProfile = pm.FindwithExpression(x => x.Owner.Username == userName);
                if (searchingProfile != null)
                {
                    return View(searchingProfile);
                }
            }
            return RedirectToAction("../Hata");
        }
        [HttpGet]
        public ActionResult ProfileUpdate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ProfileUpdate(ProfileModal profile)
        {
            if (ModelState.IsValid)
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
                    if (Request.Files.Count > 0)
                    {
                        string fileName = Path.GetFileName(Guid.NewGuid() + "_" + profil.Owner.Id);
                        string fileExtension = Path.GetExtension(Request.Files[0].FileName);
                        string imagePath = "~/Images/UserProfile/" + fileName + fileExtension;
                        Request.Files[0].SaveAs(Server.MapPath(imagePath));
                        profil.profileImageUrl = imagePath;
                    }
                    int result = pm.Update(profil);
                    if (result > -1)
                    {
                        return View(nameof(PrivateProfile));
                    }
                }
            }
            return View(profile);
        }
        [HttpGet]
        public ActionResult PublicAllList()//TODO: liste paylaşma hakkında SSS hazırlanacak. acordion şeklinde
        {
            List<SharedListTable> list = slm.SharedAllList(currentUserID);//paylaşılan tüm listeleri gönderiyor.
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
                var sharedlist = slm.FindwithExpression(x => x.listId == listId);//paylaşılan listenin aslını buluyoruz.

                newlist.profileId = currentUserID;
                newlist.listId = sharedlist.List.Id;
                newlist.OwnerId = sharedlist.Owner.Id;

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
        {//TODO: Buraya bi bak
            SharedListTable i = slm.FindwithExpression(x => x.profileId == currentUserID && x.listId == ListId && x.OwnerId == ownerId);
            return i == null;
        }
        private void SaveDatafromList(int listId, SharedListTable newlist)//listenin içindeki tüm verileri kaydediyoruz.
        {
            List<AlmondDataTable> list = slm.FindwithExpression(x => x.List.Owner.Id == x.profile.Id).List.Owner.DataTables;
            for (int i = 0; i < list.Count; i++)
            {
                SharedDataTable data = new SharedDataTable
                {
                    question = list[i].question,
                    answer = list[i].answer,
                    SharedList = newlist
                };
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
        public int DeleteSharedData(int? id)//TODO: bi bak buraya
        {
            if (id != null)
            {
                SharedDataTable deleteData = sdm.FindwithExpression(x => x.Id == id);
                if (deleteData != null)
                {
                    int result = sdm.DeleteList(deleteData);
                    return result;
                }
            }
            return -1;
        }
        [HttpPost]
        public int UpdateSharedData(int? id, string que, string ans)
        {
            if (id != null)
            {
                SharedDataTable updateData = sdm.FindwithExpression(x => x.Id == id);
                if (updateData != null)
                {
                    updateData.question = que;
                    updateData.answer = ans;
                    int result = sdm.Update(updateData);
                    return result;
                }
            }
            return -1;
        }
        [AllowAnonymous]
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost, AllowAnonymous]
        public int Contact(ContactTable contact)
        {
            if (contact != null)
            {
                int result = cm.Insert(contact);
                return result;
            }
            else { return -1; }
        }
        [HttpGet]
        public ActionResult AllProfile()
        {
            List<ProfileTable> allProfileList = pm.List();
            return View(allProfileList);
        }
        [HttpGet]
        public ActionResult FrequentlyAskedQuestion()
        {
            return View();
        }
        [HttpPost]
        public PartialViewResult SearchUserPartial(string username)
        {
            List<ProfileTable> user = pm.ListwithExpression(x => x.Owner.Username == username);
            return PartialView("Partials/_AllProfilePartial", user);
        }
        public PartialViewResult AllProfilePartial()
        {
            return PartialView("Partials/_AllProfilePartial");
        }
    }
}