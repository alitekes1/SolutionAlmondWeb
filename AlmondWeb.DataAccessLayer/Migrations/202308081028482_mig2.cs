namespace AlmondWeb.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SharedListTables", "Owner_Id", "dbo.AlmondUserTables");
            DropIndex("dbo.SharedListTables", new[] { "Owner_Id" });
            RenameColumn(table: "dbo.SharedListTables", name: "Owner_Id", newName: "OwnerId");
            AddColumn("dbo.SharedListTables", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.SharedListTables", "OwnerId", c => c.Int(nullable: false));
            CreateIndex("dbo.SharedListTables", "OwnerId");
            AddForeignKey("dbo.SharedListTables", "OwnerId", "dbo.AlmondUserTables", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SharedListTables", "OwnerId", "dbo.AlmondUserTables");
            DropIndex("dbo.SharedListTables", new[] { "OwnerId" });
            AlterColumn("dbo.SharedListTables", "OwnerId", c => c.Int());
            DropColumn("dbo.SharedListTables", "Id");
            RenameColumn(table: "dbo.SharedListTables", name: "OwnerId", newName: "Owner_Id");
            CreateIndex("dbo.SharedListTables", "Owner_Id");
            AddForeignKey("dbo.SharedListTables", "Owner_Id", "dbo.AlmondUserTables", "Id");
        }
    }
}
