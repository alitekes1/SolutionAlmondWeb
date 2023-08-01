namespace AlmondWeb.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migListdescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ListTables", "listDescription", c => c.String(maxLength: 60));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ListTables", "listDescription");
        }
    }
}
