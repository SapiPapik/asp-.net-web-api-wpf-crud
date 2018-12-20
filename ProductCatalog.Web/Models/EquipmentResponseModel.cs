using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductCatalog.Web.Models {
    public class EquipmentResponseModel {
        public Guid Id { get; set; }
        public string Specifications { get; set; }
        public decimal Price { get; set; }

        public Guid BrandId { get; set; }
        public Guid ToolTypeId { get; set; }

        public BrandResponseModel Brand { get; set; }
        public ToolTypeResponseModel ToolType { get; set; }
    }
}