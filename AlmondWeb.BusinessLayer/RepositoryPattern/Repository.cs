using AlmondWeb.DataAccessLayer;
using AlmondWeb.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AlmondWeb.BusinessLayer.RepositoryPattern
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
            if (dataset is MyEntityBase)
            {
                MyEntityBase o = dataset as MyEntityBase;
                o.createdTime = getDate();
                o.deletedTime = getDate();
                o.isDeleted = false;

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
            if (dataset is MyEntityBase)
            {
                MyEntityBase o = new MyEntityBase();

                o.deletedTime = DateTime.Now;
                o.isDeleted = true;
            }
            //database.Set<T>().Remove(dataset);
            return Save();
        }
        public List<T> List()
        {
            return database.Set<T>().ToList();
        }
        public List<T> List(Expression<Func<T, bool>> expression)
        {
            return database.Set<T>().Where(expression).ToList();
        }
        public T Find(Expression<Func<T, bool>> expression)
        {
            return database.Set<T>().SingleOrDefault(expression);
        }
        private DateTime getDate()
        {
            DateTime now = DateTime.Now.Date;
            return now;
        }
    }
}
