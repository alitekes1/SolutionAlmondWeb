namespace AlmondWeb.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig41 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProfileTables", "githubUrl", c => c.String(maxLength: 50));
            AlterColumn("dbo.ProfileTables", "linkedinUrl", c => c.String(maxLength: 50));
            AlterColumn("dbo.ProfileTables", "websiteUrl", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProfileTables", "websiteUrl", c => c.String());
            AlterColumn("dbo.ProfileTables", "linkedinUrl", c => c.String());
            AlterColumn("dbo.ProfileTables", "githubUrl", c => c.String());
        }
    }
}
