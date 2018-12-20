using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.BLL.Contracts.Dtos {
    public class EquipmentDto {
        public Guid Id { get; set; }
        public string Specifications { get; set; }
        public decimal Price { get; set; }

        public Guid BrandId { get; set; }
        public Guid ToolTypeId { get; set; }

        public BrandDto Brand { get; set; }
        public ToolTypeDto ToolType { get; set; }
    }
}
