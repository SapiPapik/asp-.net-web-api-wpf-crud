using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCatalog.Data.Entities;

namespace ProductCatalog.DAL.Mappings {
    public class EquipmentMap : EntityTypeConfiguration<Equipment> {
        public EquipmentMap() {
            ToTable("Equipments");

            HasKey(e => e.Id);

            Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(e => e.Price).HasColumnType("Money");
            Property(e => e.Specifications).HasMaxLength(50);
        }
    }
}
