using ProductCatalog.Data.Entities;

namespace ProductCatalog.DAL.Migrations {
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<ProductCatalog.DAL.ProductDialogDbContext> {
        public Configuration() {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ProductCatalog.DAL.ProductDialogDbContext context) {
            if (!context.Brands.Any() && !context.ToolTypes.Any()) {
                var brand = new Brand {
                    Name = "TestBrand",
                    Description = "TestDescription",
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                };

                var toolType = new ToolType {
                    Name = "TestName",
                    DescriptionApplicationOptions = "TestDescription",
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                };

                var equipment = new Equipment {
                    Specifications = "TestSpecification",
                    Price = 10,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    BrandId = brand.Id,
                    ToolTypeId = toolType.Id
                };

                context.SaveChanges();
            }
        }
    }
}
