using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AlmondWeb.Core.DataAccess
{
    public interface IDataAccess<T>
    {
        List<T> List();

        List<T> ListwithExpression(Expression<Func<T, bool>> expression);

        T FindwithExpression(Expression<Func<T, bool>> expression);
        T FindwithOwnerId(int ownerId);
        int Insert(T dataset);
        int Update(T dataset);
        int Delete(T dataset);

    }
}
