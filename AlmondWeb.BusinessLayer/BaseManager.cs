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
        public int DeleteList(ListTable dataset)
        {
            return repo.DeleteList(dataset);
        }
        public T FindwithOwnerId(int ownerid)
        {
            return repo.FindwithOwnerId(ownerid);
        }
        public int Insert(T dataset)
        {
            return repo.Insert(dataset);
        }

        public List<T> List()
        {
            return repo.List();
        }

        public List<T> ListwithExpression(Expression<Func<T, bool>> expression)
        {
            return repo.ListwithExpression(expression);
        }

        public int Update(T dataset)
        {
            return repo.Update(dataset);
        }
        public T FindwithExpression(Expression<Func<T, bool>> expression)
        {
            return repo.FindwithExpression(expression);
        }

    }
}
