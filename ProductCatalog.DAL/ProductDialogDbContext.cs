using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCatalog.Data.Entities;
using ProductCatalog.DAL.Mappings;

namespace ProductCatalog.DAL {
    public class ProductDialogDbContext : DbContext {
        public ProductDialogDbContext() : base("ProductDialogDbContext") {
#if DEBUG
            Database.Log = e => Debug.WriteLine(e);
            Configuration.LazyLoadingEnabled = false;
#endif
        }

        public DbSet<Brand> Brands { get; set; }
        //public DbSet<Equipment> Equipments { get; set; }
        public DbSet<ToolType> ToolTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new BrandMap());
            //modelBuilder.Configurations.Add(new EquipmentMap());
            modelBuilder.Configurations.Add(new ToolTypeMap());
        }
    }
}
