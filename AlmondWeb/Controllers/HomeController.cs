﻿using AlmondWeb.BusinessLayer;
using AlmondWeb.BusinessLayer.ViewModels;
using AlmondWeb.Entities;
using AlmondWeb.Filters;
using Microsoft.Ajax.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace AlmondWeb.WebApp.Controllers
{
    [Authorize, Exc]
    public class
        HomeController : Controller
    {
        private DataManager dataManager = new DataManager();
        private ListManager listManager = new ListManager();
        private ContactManager contactManager = new ContactManager();
        private ProfileManager profileManager = new ProfileManager();
        private SharedListManager sharedListManager = new SharedListManager();
        private SharedDataManager sharedDataManager = new SharedDataManager();
        private int currentUserId = CacheHelper.CurrentUserID();
        private List<UserQueAnswListModel> dataJson = new List<UserQueAnswListModel>();
        private UserManager userManager = new UserManager();

        public ActionResult MainPage()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetQuestionAnswerJson(int listId)
        {
            dataJson = getList(listId);
            dataJson = dataJson.OrderBy(x => x.puan).ToList();
            return Json(dataJson, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public int UpdatePuan(int? dataId, int puan, int? listIdPost)
        {
            if (dataJson == null)
            {
                return -1;
            }

            if (puan > 0 && dataId != null)
            {
                AlmondDataTable data = dataManager.FindwithExpression(x => x.Id == dataId && x.Owner.Id == currentUserId);//güncellenecek olan veriyi buluyoruz.
                SharedDataTable shareddata = sharedDataManager.FindwithExpression(x => x.Id == dataId && x.SharedList.OwnerId != currentUserId);
                if (data != null)
                {
                    data.puan += puan;//puan güncellemesi yapılıyor
                    int result = dataManager.Update(data);
                    return result;
                }
                else if (shareddata != null)
                {
                    shareddata.puan += puan;
                    int result = sharedDataManager.Update(shareddata);
                    return result;
                }
            }
            return -1;
        }
        private List<UserQueAnswListModel> getList(int? listId)
        {
            List<AlmondDataTable> mydatalist = dataManager.ListwithExpression(x => x.List.Id == listId && !x.isDeleted && x.Owner.Id == currentUserId).OrderBy(x => x.puan).ToList();
            List<SharedDataTable> shareddatalist = sharedDataManager.ListwithExpression(x => x.SharedList.listId == listId && x.SharedList.profileId == currentUserId && !x.isDeleted);
            if (shareddatalist.Count() != 0)
            {
                for (int i = 0; i < shareddatalist.Count(); i++)
                {
                    UserQueAnswListModel data = new UserQueAnswListModel
                    {
                        question = shareddatalist[i].question,
                        answer = shareddatalist[i].answer,
                        update_Id = shareddatalist[i].Id,
                        puan = shareddatalist[i].puan
                    };
                    dataJson.Add(data);
                }
            }
            if (mydatalist.Count() != 0)
            {
                for (int i = shareddatalist.Count(), j = 0; j < mydatalist.Count(); i++, j++)
                {
                    UserQueAnswListModel data = new UserQueAnswListModel
                    {
                        question = mydatalist[j].question,
                        answer = mydatalist[j].answer,
                        update_Id = mydatalist[j].Id,
                        puan = mydatalist[j].puan
                    };
                    dataJson.Add(data);
                }
            }
            return dataJson;
        }
        [HttpGet]
        public ActionResult AddData()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddData(AlmondDataTable alm)
        {
            if (alm.question != null && alm.answer != null && alm.List.Id > 0)//almamız gerekn bilgileri alıp almadığımızı kontrol ediyoruz.
            {
                alm.puan = 0;
                alm.Owner = userManager.FindwithOwnerId(currentUserId);
                alm.List = listManager.FindwithOwnerId(alm.List.Id);
                dataManager.Insert(alm);
            }
            else
            {
                return View(alm);
            }
            return RedirectToAction(nameof(FillTablewithData));
        }

        [HttpGet]
        public ActionResult UpdateData()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult UpdateData(UserQueAnswListModel data)
        {
            if (data.update_Id == null)
            {
                return RedirectToAction(nameof(Error));
            }

            int result = 0;

            AlmondDataTable updateData = dataManager.FindwithExpression(x => x.Id == data.update_Id && x.Owner.Id == currentUserId);
            SharedDataTable sharedUpdateData = sharedDataManager.FindwithExpression(x => x.Id == data.update_Id && x.SharedList.profileId == currentUserId);

            if (updateData != null)
            {
                updateData.answer = data.answer;
                updateData.question = data.question;
                updateData.List = listManager.FindwithOwnerId(data.list_Id);
                result = dataManager.Update(updateData);
            }
            else if (sharedUpdateData != null)
            {
                sharedUpdateData.answer = data.answer;
                sharedUpdateData.question = data.question;
                result = sharedDataManager.Update(sharedUpdateData);
            }

            if (result > -1)
            {
                return RedirectToAction(nameof(FillTableDataForUpdate));
            }
            else
            {
                return RedirectToAction(nameof(Error));
            }
        }
        [HttpGet]
        public ActionResult DeleteData()
        {
            return View();
        }
        [HttpPost]
        public int DeleteData(int? id)
        {
            int result;
            if (id != null)
            {
                var deletedata = dataManager.FindwithExpression(x => x.Id == id && !x.isDeleted);
                if (deletedata != null)
                {
                    result = dataManager.Delete(deletedata);
                    return result;
                }
            }
            return -1;
        }
        [HttpGet]
        public ActionResult CreateList()
        {
            return View();
        }
        [HttpPost]
        public int CreateList(string listNm, string listDesc, int listTypeCreate)
        {
            if (!listNm.IsNullOrWhiteSpace())
            {
                ListTable newList = new ListTable
                {
                    listName = listNm,
                    listDescription = listDesc,
                    isDeleted = false,
                    isPublic = listTypeCreate == 1,
                    Owner = userManager.FindwithOwnerId(currentUserId)
                };
                int result = listManager.Insert(newList);
                InsertToSharedListTable(newList.Id, newList.isPublic);
                return result;
            }
            return -1;
        }
        [HttpPost]
        public int RemoveMyList(int? id)//kendi listemi kaldırırken kullanılır.
        {
            if (id.HasValue)
            {
                ListTable list = listManager.FindwithExpression(x => x.Id == id);//silinecek veriyi buluyoruz.
                SharedListTable sharedlist = sharedListManager.FindwithExpression(x => x.listId == id && x.OwnerId == currentUserId);//benim sahip olduğum listeyi buluyoruz.
                if (list != null && sharedlist != null)
                {
                    int result = listManager.Delete(list);
                    int result2 = sharedListManager.Delete(sharedlist);
                    return result;
                }
            }
            return -1;
        }
        [HttpPost]
        public int RemoveSavedList(int? listid, int? profileId)//liste keşfet te kaydettiğim listeyi kaldırıken kullanılır.
        {
            if (listid != null && profileId != null)
            {
                List<SharedDataTable> deletedDataList = sharedDataManager.ListwithExpression(x => x.SharedList.listId == listid && x.SharedList.profileId == profileId);
                if (deletedDataList != null)
                {
                    List<AlmondDataTable> data = dataManager.ListwithExpression(x => x.List.Id == listid && x.Owner.Id == profileId);
                    int result2 = sharedDataManager.RemoveNullDatainSharedDataTable(deletedDataList);//kaldırılan listeden sonra ilgili listedeki veriler siliniyor.
                    SharedListTable sharedlist = sharedListManager.FindwithExpression(x => x.listId == listid && x.profileId == profileId);//bu kod "yelkenler biçilecek" marşını dinlerken yazılmıştır.5/9/23 18.03
                    int result = sharedListManager.DeleteList(sharedlist);
                    int result3 = dataManager.RemoveListRange(data);
                    return result;
                }
            }
            return -1;
        }
        [HttpPost]
        public int UpdateList(string listName, int? id, string listDesc, int? listType)
        {
            if (id != null)
            {
                ListTable list = listManager.FindwithExpression(x => x.Id == id);
                if (list != null)
                {
                    list.listName = listName;
                    list.listDescription = listDesc;
                    list.isPublic = listType == 1;
                    int result = listManager.Update(list);
                    if (list.isPublic)
                    {
                        SharedListTable updateSharedList = sharedListManager.FindwithExpression(x => x.listId == id && x.profileId == currentUserId);
                        updateSharedList.isPublic = true;
                        sharedListManager.Update(updateSharedList);
                    }
                    return result;
                }
            }
            return -1;
        }
        public int InsertToSharedListTable(int? listId, bool ispublic)
        {
            if (listId != null)
            {
                SharedListTable profileList = new SharedListTable
                {
                    profileId = currentUserId,
                    listId = listId.Value,
                    isPublic = ispublic,
                    Owner = listManager.FindwithExpression(x => x.Id == listId).Owner//liste sahibini verdik
                };

                return sharedListManager.Insert(profileList);
            }
            else { return -1; }
        }
        public ActionResult AllData()
        {
            return View();
        }
        public ActionResult ListOperations()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Error()
        {
            ViewData["errorMessage"] = "Bakmış olduğunuz sayfa kaldırılmış veya ismi değiştirilmiş olabilir.Dolayısıyla geçici bir süre kullanım dışıdır.Anasayfaya geri dönebilir,tekrardan giriş yapabilirsiniz veya bizimle iletişime geçebilirsiniz.";
            ViewData["errorTitle"] = "Sayfa bulunamadı!";
            ViewData["errorCode"] = "404";
            return View();
        }

        [AllowAnonymous]
        public PartialViewResult Shortcuts()
        {
            return PartialView("Partials/_ShortcutsPartial");
        }
        public PartialViewResult FillTablewithData()
        {
            return PartialView("TablePartial/_AddDataTablePartial");
        }
        public PartialViewResult FillTableDataForUpdate()
        {
            return PartialView("TablePartial/_UpdateTablePartial");
        }
        public PartialViewResult FillTableForDelete()
        {
            return PartialView("TablePartial/_DeleteDataTable");
        }
        public PartialViewResult FillTableForAllData()
        {
            return PartialView("TablePartial/_AllDataTable");
        }
        public PartialViewResult MyAllListTable()
        {
            return PartialView("TablePartial/_MyAllListTablePartial");
        }
        public PartialViewResult SavedListTablePartial()
        {
            return PartialView("TablePartial/_SavedListTablePartial");
        }
        [HttpGet]
        public ActionResult MainPageQuestionAnswerPartial()
        {
            return PartialView("Partials/_MainPageQuestionAnswerPartial");
        }
    }
}