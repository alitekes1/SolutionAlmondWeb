using AlmondWeb.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmondWeb.BusinessLayer
{
    public class RepositoryBase
    {
        private static DatabaseContext _db;
        protected RepositoryBase()//sadece miras alan sınfıtan new leme işleme yapılabilir.
        {

        }
        public static DatabaseContext CreateContext()/*biz databaseContext işlemleri sırasında üretilen  nesne üzerinde işlem yapıyyoruz.
                                                     //Her tablo için bu işlemi yaparsak hata alırız. Bu yüzden tek bir databaseContext
                                                     //nesnesi üzerinden işlem yapmak zorundayız. Singelton design pattern ile bu sorunu ortadan kaldırıyoruz.*/
        {
            if (_db == null)
            {
                _db = new DatabaseContext();
            }
            return _db;//eğer onceden oluşmamaışsa yukarıdaki if te oluşanı döndür. eğer if e girmezse önceden oluşruduğumuz db nesnesini döndür.
        }
    }
}
