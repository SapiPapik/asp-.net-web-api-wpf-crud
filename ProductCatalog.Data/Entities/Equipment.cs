using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCatalog.Data.Entities.Base;

namespace ProductCatalog.Data.Entities {
    public class Equipment : BaseEntity {
        public string Specifications { get; set; }
        public decimal Price { get; set; }

        public Guid BrandId { get; set; }
        public Guid ToolTypeId { get; set; }
    }
}
