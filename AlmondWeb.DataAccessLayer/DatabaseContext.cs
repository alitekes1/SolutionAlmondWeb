using AlmondWeb.Entities;
using System.Data.Entity;

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
        public DbSet<ProfileTable> ProfileTables { get; set; }
        public DbSet<ContactTable> ContactTables { get; set; }
        public DbSet<ProfListTable> profListTables { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProfListTable>()
                .HasKey(value => new { value.profileId, value.listId });
            modelBuilder.Entity<ListTable>()
                .HasKey(value => new { value.Id });
        }

    }
}
