namespace AlmondWeb.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SharedDataTables", "SharedData_Id", c => c.Int());
            AddColumn("dbo.SharedDataTables", "SharedListTable_profileId", c => c.Int());
            AddColumn("dbo.SharedDataTables", "SharedListTable_listId", c => c.Int());
            CreateIndex("dbo.SharedDataTables", "SharedData_Id");
            CreateIndex("dbo.SharedDataTables", new[] { "SharedListTable_profileId", "SharedListTable_listId" });
            AddForeignKey("dbo.SharedDataTables", "SharedData_Id", "dbo.SharedDataTables", "Id");
            AddForeignKey("dbo.SharedDataTables", new[] { "SharedListTable_profileId", "SharedListTable_listId" }, "dbo.SharedListTables", new[] { "profileId", "listId" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SharedDataTables", new[] { "SharedListTable_profileId", "SharedListTable_listId" }, "dbo.SharedListTables");
            DropForeignKey("dbo.SharedDataTables", "SharedData_Id", "dbo.SharedDataTables");
            DropIndex("dbo.SharedDataTables", new[] { "SharedListTable_profileId", "SharedListTable_listId" });
            DropIndex("dbo.SharedDataTables", new[] { "SharedData_Id" });
            DropColumn("dbo.SharedDataTables", "SharedListTable_listId");
            DropColumn("dbo.SharedDataTables", "SharedListTable_profileId");
            DropColumn("dbo.SharedDataTables", "SharedData_Id");
        }
    }
}
