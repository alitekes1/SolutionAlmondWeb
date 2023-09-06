namespace AlmondWeb.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig34 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ListTables", "listName", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ListTables", "listName", c => c.String(nullable: false, maxLength: 60));
        }
    }
}
