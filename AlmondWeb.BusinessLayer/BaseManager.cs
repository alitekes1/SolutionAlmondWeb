using AlmondWeb.DataAccessLayer.RepositoryPattern;
using AlmondWeb.Core.DataAccess;
using AlmondWeb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AlmondWeb.BusinessLayer
{
    public abstract class BaseManager<T> : IDataAccess<T> where T : class
    {
        private Repository<T> repo = new Repository<T>();
        public int Delete(T dataset)
        {
            return repo.Delete(dataset);
        }

        public T Find(Expression<Func<T, bool>> expression)
        {
            return repo.Find(expression);
        }

        public int Insert(T dataset)
        {
            return repo.Insert(dataset);
        }

        public List<T> List()
        {
            return repo.List();
        }
        public List<T> List(int sayi)
        {
            return repo.List(sayi);
        }
        public List<T> List(Expression<Func<T, bool>> expression)
        {
            return repo.List(expression);
        }

        public int Update(T dataset)
        {
            return repo.Update(dataset);
        }
    }
}
