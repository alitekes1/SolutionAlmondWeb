using AlmondWeb.BusinessLayer;
using AlmondWeb.BusinessLayer.ViewModels;
using AlmondWeb.Entities;
using AlmondWeb.Filters;
using System;
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
        private DataManager dm = new DataManager();
        private ListManager lm = new ListManager();
        private UserManager um = new UserManager();
        private ContactManager cm = new ContactManager();
        private int currentUserID = CacheHelper.CurrentUserID();
        private Random random = new Random();
        private int code = 0;
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
                    return RedirectToAction("MainPage", "Home");
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

        [HttpGet, AllowAnonymous]
        public ActionResult ForgetPassword()
        {
            Session.Clear();
            Request.Cookies.Clear();
            Response.Cookies.Clear();
            return View();
        }
        [HttpPost, AllowAnonymous]
        public ActionResult ForgetPassword(string Email)
        {
            AlmondUserTable user = um.FindwithExpression(x => x.Email == Email);
            if (user != null)
            {
                EmailHelper.SendEmail(Email, user);
                return JavaScript("");//TODO: hesabın olamdığı uyarısı verilecek
                //return View();
            }
            else
            {
                return JavaScript("");//TODO: hesabın olamdığı uyarısı verilecek
            }
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
            return View();
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
                else
                {
                    return RedirectToAction("Error", "User");
                }
            }
            return RedirectToAction("MainPage", "Home");
        }
        [HttpGet]
        public ActionResult ProfileUpdate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ProfileUpdate(ProfileModal profilemodal)
        {
            if (ModelState.IsValid)
            {
                ProfileTable savedProfile = pm.FindwithOwnerId(profilemodal.ownerId);
                if (savedProfile != null)
                {
                    savedProfile.aboutmeText = profilemodal.aboutmeText;
                    savedProfile.school = profilemodal.school;
                    savedProfile.githubUrl = profilemodal.githubUrl;
                    savedProfile.linkedinUrl = profilemodal.linkedinUrl;
                    savedProfile.websiteUrl = profilemodal.websiteUrl;
                    savedProfile.job = profilemodal.job;
                    if (profilemodal.profileImageUrl != null)//herhangi bir resim yüklenmediyse
                    {
                        savedProfile.profileImageUrl = profilemodal.profileImageUrl;
                    }
                    else
                    {
                        //savedProfile.profileImageUrl = savedProfile.profileImageUrl; //"~/Images/defaultUserImage.jpg";
                        savedProfile.profileImageUrl = savedProfile.profileImageUrl; //"~/Images/defaultUserImage.jpg";
                    }
                    int result = pm.Update(savedProfile);
                    if (result > -1)
                    {
                        return View(nameof(PrivateProfile));
                    }
                }
            }
            return View(profilemodal);
        }
        [HttpPost]
        public int RemoveProfilImage()
        {
            ProfileTable profile = pm.FindwithOwnerId(currentUserID);
            profile.profileImageUrl = "https://i.imgur.com/iZhMGsd.jpg";
            int result = pm.Update(profile);
            return result;
        }
        [HttpGet]
        public ActionResult PublicAllList()
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
                var sharedlist = slm.FindwithExpression(x => x.listId == listId);//paylaşılan listenin aslını buluyoruz.

                SharedListTable newlist = new SharedListTable();
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
            List<AlmondDataTable> list = dm.ListwithExpression(x => x.Owner.Id == newlist.OwnerId && x.List.Id == listId);
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