using AlmondWeb.BusinessLayer.RepositoryPattern;
using AlmondWeb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace AlmondWeb.BusinessLayer
{
    public class DataManager
    {
        Repository<AlmondDataTable> repo_data = new Repository<AlmondDataTable>();

        public AlmondDataTable getSingleData(Expression<Func<AlmondDataTable, bool>> expression)
        {
            return repo_data.List(expression);
        }
        //veri ekleme ve çekme işlemlerinin hepsi burada yapılır.kullanıcı db
        //ye direkt erişememesi için web katmanından alınanları data katmanına
        //etki etmesini sağlayan ara bir katman olarak düşün burayı.
    }
}
