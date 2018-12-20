using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCatalog.Data.Entities;

namespace ProductCatalog.DAL.Mappings {
    public class ToolTypeMap : EntityTypeConfiguration<ToolType> {
        public ToolTypeMap() {
            ToTable("ToolTypes");

            HasKey(tt => tt.Id);

            Property(tt => tt.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
