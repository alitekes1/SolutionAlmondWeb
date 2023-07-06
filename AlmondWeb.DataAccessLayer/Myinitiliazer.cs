using AlmondWeb.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmondWeb.DataAccessLayer
{
    public class Myinitiliazer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            DatabaseContext db = new DatabaseContext();
            AlmondUserTable admin = new AlmondUserTable();
            AlmondUserTable user = new AlmondUserTable();


            admin.Name = "Ali";
            admin.Surname = "Tekeş";
            admin.Email = "alitekes123@gmail.com";
            admin.Password = "123";
            admin.isActive = true;
            admin.isAdmin = true;
            admin.isDeleted = false;
            admin.createdTime = DateTime.Now;
            admin.deletedTime = DateTime.Now.AddDays(2);
            admin.ActivateGuid = Guid.NewGuid();


            user.Name = "Damla";
            user.Surname = "Korkmaz";
            user.Email = "damlakorkmaz@gmail.com";
            user.Password = "123";
            user.isActive = true;
            user.isAdmin = false;
            user.isDeleted = false;
            user.createdTime = DateTime.Now;
            user.deletedTime = DateTime.Now.AddDays(2);
            user.ActivateGuid = Guid.NewGuid();

            db.AlmondUserTables.Add(admin);
            db.AlmondUserTables.Add(user);
            db.SaveChanges();

            ListTable list = new ListTable()
            {
                isDeleted = false,
                createdTime = DateTime.Now,
                listName = FakeData.TextData.GetSentence(),
                deletedTime = DateTime.Now.AddDays(2),
                Owner = admin
            };
            db.ListTables.Add(list);
            db.SaveChanges();

            for (int i = 0; i < 5; i++)
            {

                AlmondDataTable dataTable = new AlmondDataTable()
                {
                    question = FakeData.TextData.GetSentence(),
                    answer = FakeData.TextData.GetSentence(),
                    puan = FakeData.NumberData.GetNumber(),
                    isDeleted = false,
                    createdTime = DateTime.Now,
                    deletedTime = DateTime.Now.AddDays(2),
                    Owner = user,
                    List = list
                };
                db.AlmondDataTables.Add(dataTable);
                db.SaveChanges();
            }


        }

    }
}
