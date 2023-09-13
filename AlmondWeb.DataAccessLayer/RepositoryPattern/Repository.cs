using AlmondWeb.BusinessLayer;
using AlmondWeb.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace AlmondWeb.DataAccessLayer.RepositoryPattern
{
    public class Repository<T> where T : class
    {
        private DatabaseContext database;
        public Repository()
        {
            database = RepositoryBase.CreateContext();
        }
        private int Save() { return database.SaveChanges(); }
        public int Insert(T dataset)
        {
            if (dataset is AlmondDataTable)
            {
                AlmondDataTable o = dataset as AlmondDataTable;
                o.createdTime = getDate();
                o.isDeleted = false;
                o.puan = 0;
            }
            if (dataset is AlmondUserTable)
            {
                AlmondUserTable o = dataset as AlmondUserTable;
                o.createdTime = getDate();
                o.isDeleted = false;
            }
            if (dataset is ListTable)
            {
                ListTable list = dataset as ListTable;
                list.createdTime = getDate();
                list.isDeleted = false;
            }
            if (dataset is SharedDataTable)
            {
                SharedDataTable data = dataset as SharedDataTable;
                data.createdTime = getDate();
                data.isDeleted = false;
                data.puan = 0;
            }
            database.Set<T>().Add(dataset);

            return Save();
        }

        public int RemoveNullDatainSharedDataTable(List<SharedDataTable> datalist)
        {
            database.Set<SharedDataTable>().RemoveRange(datalist);//liste deki entityleri siler.
            return Save();
        }

        public int Update(T dataset)
        {
            return Save();
        }
        public int Delete(T dataset)
        {
            if (dataset is AlmondDataTable)
            {
                AlmondDataTable a = dataset as AlmondDataTable;
                a.isDeleted = true;
            }
            if (dataset is ListTable)
            {
                ListTable li = dataset as ListTable;
                li.isDeleted = true;
                li.isPublic = false;
            }
            if (dataset is SharedListTable)
            {
                SharedListTable li = dataset as SharedListTable;
                li.isDeleted = true;
                li.isPublic = false;
            }
            return Save();
        }
        public int DeleteList(T dataset)
        {
            database.Set<T>().Remove(dataset);
            return Save();
        }
        public int RemoveListRange(List<T> dataset)
        {
            database.Set<T>().RemoveRange(dataset);
            return Save();
        }
        public List<T> List()
        {
            return database.Set<T>().ToList();
        }
        public List<T> ListwithExpression(Expression<Func<T, bool>> expression)
        {
            return database.Set<T>().Where(expression).ToList();
        }
        public T FindwithExpression(Expression<Func<T, bool>> expression)
        {

            return database.Set<T>().FirstOrDefault(expression);
        }
        public T FindwithOwnerId(int ownerId)
        {
            return database.Set<T>().Find(ownerId);
        }
        public List<SharedListTable> SharedAllList(int currentUserId)//sadece o anki kullanıcıların dışındaki herkesin paylaştığı listeleri döner. public listte tüm listeler kısmında
        {
            List<SharedListTable> profileLists = database.SharedListTables
                .Include(pl => pl.List)
                .Where(pl => pl.profileId == pl.Owner.Id && pl.Owner.Id != currentUserId && pl.isPublic).ToList();
            return profileLists;
        }
        public List<SharedListTable> MySavedShareList(int curentuserid)//sadece o anki kullanıcının kaydettiği listeleri döner.profilimdeki mysaved da ve listoperations da saved tabloda 
        {
            var profileLists = database.SharedListTables
                .Include(pl => pl.List)
                .Where(pl => pl.profileId == curentuserid && pl.List.Owner.Id != curentuserid).ToList();//TODO:liste artık public değil hatası vereceğiz.

            return profileLists;
        }
        public List<SharedListTable> SharedListOnlyCurrentUser(int currentUserId)//sadece kullanıcın paylaştığı listelerin tamamını döner.profilimdeki mysharedda
        {
            List<SharedListTable> profileLists = database.SharedListTables
                .Include(pl => pl.List)
                .Where(pl => pl.profileId == pl.Owner.Id && pl.Owner.Id == currentUserId && pl.isPublic).ToList();
            return profileLists;
        }
        public List<SharedListTable> FindRelotionList(string text, int userid)//liste ismine veya username e göre arama yapar.
        {
            List<SharedListTable> profileLists = database.SharedListTables
                .Include(pl => pl.List)
                .Where(pl => pl.profileId == pl.List.Owner.Id && pl.List.Owner.Id != userid && pl.List.listName == text || pl.List.Owner.Username == text && pl.List.isPublic).ToList();

            return profileLists;
        }
        private DateTime getDate()
        {
            DateTime now = DateTime.Now.Date;
            return now;
        }
    }
}
