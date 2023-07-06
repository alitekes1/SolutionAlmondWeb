using AlmondWeb.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
            database.Set<T>().Add(dataset);

            return Save();
        }
        public int Update() { return Save(); }
        public int Delete(T dataset)
        {
            database.Set<T>().Remove(dataset);
            return Save();
        }
        public List<T> List()
        {
            return database.Set<T>().ToList();
        }
        public T List(Expression<Func<T, bool>> expressions)
        {
            return database.Set<T>().FirstOrDefault(expressions);
        }
        public T Find(Expression<Func<T, bool>> expression)
        {
            return database.Set<T>().FirstOrDefault(expression);
        }

    }
}
