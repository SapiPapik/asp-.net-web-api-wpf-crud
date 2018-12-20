namespace ProductCatalog.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Description = c.String(maxLength: 50),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(nullable: false),
                        IsArchive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ToolTypes", t => t.ToolTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Brands", t => t.BrandId, cascadeDelete: true)
                .Index(t => t.BrandId)
                .Index(t => t.ToolTypeId);
            
            CreateTable(
                "dbo.ToolTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        DescriptionApplicationOptions = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedAt = c.DateTime(nullable: false),
                        IsArchive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Equipments", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.Equipments", "ToolTypeId", "dbo.ToolTypes");
            DropIndex("dbo.Equipments", new[] { "ToolTypeId" });
            DropIndex("dbo.Equipments", new[] { "BrandId" });
            DropTable("dbo.ToolTypes");
            DropTable("dbo.Equipments");
            DropTable("dbo.Brands");
        }
    }
}
