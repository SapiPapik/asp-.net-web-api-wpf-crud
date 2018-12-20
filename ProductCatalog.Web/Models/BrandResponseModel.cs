using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductCatalog.Web.Models {
    public class BrandResponseModel {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}