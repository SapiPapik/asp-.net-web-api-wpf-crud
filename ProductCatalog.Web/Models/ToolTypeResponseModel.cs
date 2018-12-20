using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductCatalog.Web.Models {
    public class ToolTypeResponseModel {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DescriptionApplicationOptions { get; set; }
    }
}