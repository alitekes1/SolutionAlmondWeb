using AlmondWeb.DataAccessLayer.RepositoryPattern;
using AlmondWeb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmondWeb.BusinessLayer
{
    public class Deneme
    {
        public Deneme()
        {
            Repository<AlmondUserTable> user = new Repository<AlmondUserTable>();
            Repository<ListTable> list = new Repository<ListTable>();
            Repository<AlmondDataTable> data = new Repository<AlmondDataTable>();
            //List<AlmondUserTable> liste = user.List();
            //int value = user.Insert(new AlmondUserTable()
            //{
            //    Name = "Ali",
            //    Surname = "Tekeş", 
            //    Email = "alitekes123@gmail.com",
            //    Password = "123",
            //    isActive = true,
            //    isAdmin = true,
            //    isDeleted = false,
            //    createdTime = DateTime.Now,
            //    deletedTime = DateTime.Now.AddDays(2),
            //    ActivateGuid = Guid.NewGuid()
            //});
            #region silme işlemi
            //AlmondUserTable userdel = user.Find(x => x.Surname == "aa");
            //int value = user.Delete(userdel);
            #endregion

            #region update işlemi
            AlmondUserTable userupdate = user.FindwithExpression(x => x.Surname == "korkmaz");
            if (userupdate != null)
            {
                userupdate.Surname = "tekeş";
                user.Update(userupdate);
            }
            #endregion
        }
    }
}
