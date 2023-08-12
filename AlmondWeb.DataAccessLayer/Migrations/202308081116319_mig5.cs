namespace AlmondWeb.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig5 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AlmondDataTables", "deletedTime");
            DropColumn("dbo.ListTables", "deletedTime");
            DropColumn("dbo.AlmondUserTables", "deletedTime");
            DropColumn("dbo.SharedDataTables", "deletedTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SharedDataTables", "deletedTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.AlmondUserTables", "deletedTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.ListTables", "deletedTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.AlmondDataTables", "deletedTime", c => c.DateTime(nullable: false));
        }
    }
}
