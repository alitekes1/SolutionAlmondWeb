namespace AlmondWeb.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rebuildDatabase : DbMigration
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
                        listDescription = c.String(maxLength: 100),
                        isPublic = c.Boolean(nullable: false),
                        isDeleted = c.Boolean(nullable: false),
                        createdTime = c.DateTime(nullable: false),
                        Owner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AlmondUserTables", t => t.Owner_Id)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.SharedListTables",
                c => new
                    {
                        profileId = c.Int(nullable: false),
                        listId = c.Int(nullable: false),
                        Id = c.Int(nullable: false, identity: true),
                        isPublic = c.Boolean(nullable: false),
                        isDeleted = c.Boolean(nullable: false),
                        OwnerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.profileId, t.listId })
                .ForeignKey("dbo.ListTables", t => t.listId, cascadeDelete: true)
                .ForeignKey("dbo.AlmondUserTables", t => t.OwnerId, cascadeDelete: true)
                .ForeignKey("dbo.ProfileTables", t => t.profileId, cascadeDelete: true)
                .Index(t => t.profileId)
                .Index(t => t.listId)
                .Index(t => t.OwnerId);
            
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
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProfileTables",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        school = c.String(maxLength: 50),
                        job = c.String(maxLength: 23),
                        githubUrl = c.String(maxLength: 50),
                        almondUrl = c.String(),
                        linkedinUrl = c.String(maxLength: 75),
                        websiteUrl = c.String(maxLength: 75),
                        aboutmeText = c.String(maxLength: 250),
                        profileImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AlmondUserTables", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.SharedDataTables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        question = c.String(nullable: false, maxLength: 250),
                        answer = c.String(nullable: false, maxLength: 250),
                        puan = c.Int(nullable: false),
                        isDeleted = c.Boolean(nullable: false),
                        createdTime = c.DateTime(nullable: false),
                        SharedList_profileId = c.Int(),
                        SharedList_listId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SharedListTables", t => new { t.SharedList_profileId, t.SharedList_listId })
                .Index(t => new { t.SharedList_profileId, t.SharedList_listId });
            
            CreateTable(
                "dbo.ContactTables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        contactMail = c.String(maxLength: 50),
                        contactType = c.String(maxLength: 50),
                        message = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SharedDataTables", new[] { "SharedList_profileId", "SharedList_listId" }, "dbo.SharedListTables");
            DropForeignKey("dbo.SharedListTables", "profileId", "dbo.ProfileTables");
            DropForeignKey("dbo.ProfileTables", "Id", "dbo.AlmondUserTables");
            DropForeignKey("dbo.SharedListTables", "OwnerId", "dbo.AlmondUserTables");
            DropForeignKey("dbo.ListTables", "Owner_Id", "dbo.AlmondUserTables");
            DropForeignKey("dbo.AlmondDataTables", "Owner_Id", "dbo.AlmondUserTables");
            DropForeignKey("dbo.SharedListTables", "listId", "dbo.ListTables");
            DropForeignKey("dbo.AlmondDataTables", "List_Id", "dbo.ListTables");
            DropIndex("dbo.SharedDataTables", new[] { "SharedList_profileId", "SharedList_listId" });
            DropIndex("dbo.ProfileTables", new[] { "Id" });
            DropIndex("dbo.SharedListTables", new[] { "OwnerId" });
            DropIndex("dbo.SharedListTables", new[] { "listId" });
            DropIndex("dbo.SharedListTables", new[] { "profileId" });
            DropIndex("dbo.ListTables", new[] { "Owner_Id" });
            DropIndex("dbo.AlmondDataTables", new[] { "Owner_Id" });
            DropIndex("dbo.AlmondDataTables", new[] { "List_Id" });
            DropTable("dbo.ContactTables");
            DropTable("dbo.SharedDataTables");
            DropTable("dbo.ProfileTables");
            DropTable("dbo.AlmondUserTables");
            DropTable("dbo.SharedListTables");
            DropTable("dbo.ListTables");
            DropTable("dbo.AlmondDataTables");
        }
    }
}
