using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AlmondWeb.Core.DataAccess
{
    public interface IDataAccess<T>
    {
        List<T> List();

        List<T> List(Expression<Func<T, bool>> expression);

        T Find(Expression<Func<T, bool>> expression);
        int Insert(T dataset);
        int Update(T dataset);
        int Delete(T dataset);

    }
}
