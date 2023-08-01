namespace AlmondWeb.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProfileTables", "websiteUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProfileTables", "websiteUrl");
        }
    }
}
