using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.WPF.Models {
    public class EquipmentModel {
        public Guid Id { get; set; }
        public string Specifications { get; set; }
        public decimal Price { get; set; }

        public string BrandName { get; set; }
        public Guid BrandId { get; set; }
        public string ToolTypeName { get; set; }
        public Guid ToolTypeId { get; set; }
    }
}
