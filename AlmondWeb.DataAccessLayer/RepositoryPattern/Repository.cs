using AlmondWeb.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using AlmondWeb.BusinessLayer;

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
                o.deletedTime = getDate();
                o.isDeleted = false;
                o.puan = 0;
            }
            if (dataset is AlmondUserTable)
            {
                AlmondUserTable o = dataset as AlmondUserTable;
                o.createdTime = getDate();
                o.deletedTime = getDate();
                o.isDeleted = false;
            }
            if (dataset is ListTable)
            {
                ListTable list = dataset as ListTable;
                list.createdTime = getDate();
                list.deletedTime = getDate();
                list.isDeleted = false;
            }
            database.Set<T>().Add(dataset);

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
                a.deletedTime = getDate();
            }
            if (dataset is ListTable)
            {
                ListTable li = dataset as ListTable;
                li.isDeleted = true;
                li.deletedTime = getDate();
            }
            return Save();
        }
        public int DeleteList(ListTable dataset)
        {
            database.Set<ListTable>().Remove(dataset);
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
        private DateTime getDate()
        {
            DateTime now = DateTime.Now.Date;
            return now;
        }
    }
}
