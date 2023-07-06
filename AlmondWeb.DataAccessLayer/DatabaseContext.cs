using AlmondWeb.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace AlmondWeb.DataAccessLayer
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
            Database.SetInitializer(new Myinitiliazer());
        }
        public DbSet<AlmondUserTable> AlmondUserTables { get; set; }
        public DbSet<AlmondDataTable> AlmondDataTables { get; set; }
        public DbSet<ListTable> ListTables { get; set; }
    }
}
