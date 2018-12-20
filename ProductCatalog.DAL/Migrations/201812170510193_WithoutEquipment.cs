namespace ProductCatalog.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WithoutEquipment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Equipments", "ToolTypeId", "dbo.ToolTypes");
            DropForeignKey("dbo.Equipments", "BrandId", "dbo.Brands");
            DropIndex("dbo.Equipments", new[] { "BrandId" });
            DropIndex("dbo.Equipments", new[] { "ToolTypeId" });
            DropTable("dbo.Equipments");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Equipments",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Specifications = c.String(maxLength: 50),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                        BrandId = c.Guid(nullable: false),
                        ToolTypeId = c.Guid(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(nullable: false),
                        IsArchive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Equipments", "ToolTypeId");
            CreateIndex("dbo.Equipments", "BrandId");
            AddForeignKey("dbo.Equipments", "BrandId", "dbo.Brands", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Equipments", "ToolTypeId", "dbo.ToolTypes", "Id", cascadeDelete: true);
        }
    }
}
