using AlmondWeb.DataAccessLayer.RepositoryPattern;
using AlmondWeb.BusinessLayer.ViewModels;
using AlmondWeb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.AccessControl;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace AlmondWeb.BusinessLayer
{
    public class DataManager : BaseManager<AlmondDataTable>
    {
/* buradaki tüm kodları core katmanı sayesinde */
        //public AlmondDataTable getSingleData(Expression<Func<AlmondDataTable, bool>> expression)
        //{
        //    return repo_data.Find(expression);
        //}
        //veri ekleme ve çekme işlemlerinin hepsi burada yapılır.kullanıcı db
        //ye direkt erişememesi için web katmanından alınanları data katmanına
        //etki etmesini sağlayan ara bir katman olarak düşün burayı.
        //public int Insert(AlmondDataTable data)
        //{
        //    if (data != null)
        //    {
        //        AlmondDataTable newdata = new AlmondDataTable();
        //        newdata.answer = data.answer;
        //        newdata.question = data.question;
        //        newdata.puan = 0;
        //        newdata.List = data.List;
        //        newdata.Owner = data.Owner;
        //        return repo_data.Insert(data);
        //    }
        //    return -1;
        //}

        //public List<AlmondDataTable> List(int ownerId)
        //{
        //    return repo_data.List(x => x.Owner.Id == ownerId && x.isDeleted == false);
        //}
        //public int Update(UserQueAnswListModel data)
        //{
        //    AlmondDataTable updatedata = repo_data.Find(x => x.Id == data.updateDataId);
        //    if (updatedata != null)
        //    {
        //        updatedata.question = data.question;
        //        updatedata.answer = data.answer;
        //        //updatedata.List.Id = data.listValue;
        //    }
        //    return repo_data.Update(updatedata);
        //}
        //public int Delete(AlmondDataTable deletedata)
        //{
        //    if (deletedata != null)
        //    {
        //        return repo_data.Delete(deletedata);
        //    }
        //    else
        //    {
        //        return -1;
        //    }
        //}
    }
}
