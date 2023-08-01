namespace AlmondWeb.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class manytomanyrelation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProfileTableListTables",
                c => new
                    {
                        ProfileTable_Id = c.Int(nullable: false),
                        ListTable_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProfileTable_Id, t.ListTable_Id })
                .ForeignKey("dbo.ProfileTables", t => t.ProfileTable_Id, cascadeDelete: true)
                .ForeignKey("dbo.ListTables", t => t.ListTable_Id, cascadeDelete: true)
                .Index(t => t.ProfileTable_Id)
                .Index(t => t.ListTable_Id);
            
            AddColumn("dbo.ListTables", "isPublic", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProfileTableListTables", "ListTable_Id", "dbo.ListTables");
            DropForeignKey("dbo.ProfileTableListTables", "ProfileTable_Id", "dbo.ProfileTables");
            DropIndex("dbo.ProfileTableListTables", new[] { "ListTable_Id" });
            DropIndex("dbo.ProfileTableListTables", new[] { "ProfileTable_Id" });
            DropColumn("dbo.ListTables", "isPublic");
            DropTable("dbo.ProfileTableListTables");
        }
    }
}
