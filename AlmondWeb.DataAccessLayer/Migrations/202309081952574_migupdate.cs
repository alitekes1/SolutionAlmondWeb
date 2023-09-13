namespace AlmondWeb.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migupdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProfileTables", "job", c => c.String(nullable: false, maxLength: 35));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProfileTables", "job", c => c.String(maxLength: 35));
        }
    }
}
