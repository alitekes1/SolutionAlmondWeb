using AlmondWeb.Core.DataAccess;
using AlmondWeb.DataAccessLayer.RepositoryPattern;
using AlmondWeb.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

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
        public List<ProfListTable> RelationList(int userId)
        {
            return repo.RelationList(userId);
        }
        public List<ProfListTable> RelationListAll(int userId)
        {
            return repo.RelationListAll(userId);
        }
        public List<ProfListTable> FindRelotionList(string text, int userid)
        {
            return repo.FindRelotionList(text, userid);
        }
    }
}
