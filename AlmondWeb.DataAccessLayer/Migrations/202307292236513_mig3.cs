namespace AlmondWeb.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class mig3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlmondDataTables",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    question = c.String(nullable: false, maxLength: 250),
                    answer = c.String(nullable: false, maxLength: 250),
                    puan = c.Int(nullable: false),
                    isDeleted = c.Boolean(nullable: false),
                    createdTime = c.DateTime(nullable: false),
                    deletedTime = c.DateTime(nullable: false),
                    List_Id = c.Int(),
                    Owner_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ListTables", t => t.List_Id)
                .ForeignKey("dbo.AlmondUserTables", t => t.Owner_Id)
                .Index(t => t.List_Id)
                .Index(t => t.Owner_Id);

            CreateTable(
                "dbo.ListTables",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    listName = c.String(nullable: false, maxLength: 60),
                    isDeleted = c.Boolean(nullable: false),
                    createdTime = c.DateTime(nullable: false),
                    deletedTime = c.DateTime(nullable: false),
                    Owner_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AlmondUserTables", t => t.Owner_Id)
                .Index(t => t.Owner_Id);

            CreateTable(
                "dbo.AlmondUserTables",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Username = c.String(nullable: false, maxLength: 15),
                    Name = c.String(nullable: false, maxLength: 25),
                    Surname = c.String(nullable: false, maxLength: 25),
                    Email = c.String(nullable: false, maxLength: 50),
                    Password = c.String(nullable: false, maxLength: 16),
                    isActive = c.Boolean(nullable: false),
                    isAdmin = c.Boolean(nullable: false),
                    ActivateGuid = c.Guid(nullable: false),
                    isDeleted = c.Boolean(nullable: false),
                    createdTime = c.DateTime(nullable: false),
                    deletedTime = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.ContactTables",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    contactMail = c.String(nullable: false, maxLength: 50),
                    contactType = c.String(nullable: false, maxLength: 50),
                    message = c.String(nullable: false, maxLength: 250),
                    Owner_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AlmondUserTables", t => t.Owner_Id)
                .Index(t => t.Owner_Id);

            CreateTable(
                "dbo.ProfileTables",
                c => new
                {
                    Id = c.Int(nullable: false),
                    school = c.String(maxLength: 50),
                    job = c.String(nullable: false, maxLength: 50),
                    githubUrl = c.String(),
                    almondUrl = c.String(),
                    linkedinUrl = c.String(),
                    aboutmeText = c.String(nullable: false, maxLength: 250),
                    profileImageUrl = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AlmondUserTables", t => t.Id)
                .Index(t => t.Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.ProfileTables", "Id", "dbo.AlmondUserTables");
            DropForeignKey("dbo.ListTables", "Owner_Id", "dbo.AlmondUserTables");
            DropForeignKey("dbo.AlmondDataTables", "Owner_Id", "dbo.AlmondUserTables");
            DropForeignKey("dbo.ContactTables", "Owner_Id", "dbo.AlmondUserTables");
            DropForeignKey("dbo.AlmondDataTables", "List_Id", "dbo.ListTables");
            DropIndex("dbo.ProfileTables", new[] { "Id" });
            DropIndex("dbo.ContactTables", new[] { "Owner_Id" });
            DropIndex("dbo.ListTables", new[] { "Owner_Id" });
            DropIndex("dbo.AlmondDataTables", new[] { "Owner_Id" });
            DropIndex("dbo.AlmondDataTables", new[] { "List_Id" });
            DropTable("dbo.ProfileTables");
            DropTable("dbo.ContactTables");
            DropTable("dbo.AlmondUserTables");
            DropTable("dbo.ListTables");
            DropTable("dbo.AlmondDataTables");
        }
    }
}
