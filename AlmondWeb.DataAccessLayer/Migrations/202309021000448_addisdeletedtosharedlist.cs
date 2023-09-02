namespace AlmondWeb.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addisdeletedtosharedlist : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SharedListTables", "isDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SharedListTables", "isDeleted");
        }
    }
}
