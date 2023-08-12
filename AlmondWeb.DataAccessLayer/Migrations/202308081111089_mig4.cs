namespace AlmondWeb.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SharedDataTables", "SharedData_Id", "dbo.SharedDataTables");
            DropIndex("dbo.SharedDataTables", new[] { "SharedData_Id" });
            RenameColumn(table: "dbo.SharedDataTables", name: "SharedListTable_profileId", newName: "SharedList_profileId");
            RenameColumn(table: "dbo.SharedDataTables", name: "SharedListTable_listId", newName: "SharedList_listId");
            RenameIndex(table: "dbo.SharedDataTables", name: "IX_SharedListTable_profileId_SharedListTable_listId", newName: "IX_SharedList_profileId_SharedList_listId");
            DropColumn("dbo.SharedDataTables", "SharedData_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SharedDataTables", "SharedData_Id", c => c.Int());
            RenameIndex(table: "dbo.SharedDataTables", name: "IX_SharedList_profileId_SharedList_listId", newName: "IX_SharedListTable_profileId_SharedListTable_listId");
            RenameColumn(table: "dbo.SharedDataTables", name: "SharedList_listId", newName: "SharedListTable_listId");
            RenameColumn(table: "dbo.SharedDataTables", name: "SharedList_profileId", newName: "SharedListTable_profileId");
            CreateIndex("dbo.SharedDataTables", "SharedData_Id");
            AddForeignKey("dbo.SharedDataTables", "SharedData_Id", "dbo.SharedDataTables", "Id");
        }
    }
}
