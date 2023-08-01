namespace AlmondWeb.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class createProfilListTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProfileTableListTables", "ProfileTable_Id", "dbo.ProfileTables");
            DropForeignKey("dbo.ProfileTableListTables", "ListTable_Id", "dbo.ListTables");
            DropIndex("dbo.ProfileTableListTables", new[] { "ProfileTable_Id" });
            DropIndex("dbo.ProfileTableListTables", new[] { "ListTable_Id" });
            CreateTable(
                "dbo.ProfileListTables",
                c => new
                {
                    profileId = c.Int(nullable: false),
                    listId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.profileId, t.listId })
                .ForeignKey("dbo.ListTables", t => t.listId, cascadeDelete: true)
                .ForeignKey("dbo.ProfileTables", t => t.profileId, cascadeDelete: true)
                .Index(t => t.profileId)
                .Index(t => t.listId);

        }

        public override void Down()
        {
            CreateTable(
                "dbo.ProfileTableListTables",
                c => new
                {
                    ProfileTable_Id = c.Int(nullable: false),
                    ListTable_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.ProfileTable_Id, t.ListTable_Id });

            DropForeignKey("dbo.ProfileListTables", "profileId", "dbo.ProfileTables");
            DropForeignKey("dbo.ProfileListTables", "listId", "dbo.ListTables");
            DropIndex("dbo.ProfileListTables", new[] { "listId" });
            DropIndex("dbo.ProfileListTables", new[] { "profileId" });
            DropTable("dbo.ProfileListTables");
            CreateIndex("dbo.ProfileTableListTables", "ListTable_Id");
            CreateIndex("dbo.ProfileTableListTables", "ProfileTable_Id");
            AddForeignKey("dbo.ProfileTableListTables", "ListTable_Id", "dbo.ListTables", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProfileTableListTables", "ProfileTable_Id", "dbo.ProfileTables", "Id", cascadeDelete: true);
        }
    }
}
