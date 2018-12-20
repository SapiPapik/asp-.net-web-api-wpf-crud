using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCatalog.Data.Entities;

namespace ProductCatalog.DAL.Mappings {
    public class BrandMap : EntityTypeConfiguration<Brand> {
        public BrandMap() {
            ToTable("Brands");

            HasKey(b => b.Id);

            Property(b => b.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(b => b.Name).HasMaxLength(50);
            Property(b => b.Description).HasMaxLength(50);
        }
    }
}
