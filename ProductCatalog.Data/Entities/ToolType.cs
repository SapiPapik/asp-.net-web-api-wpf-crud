using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCatalog.Data.Entities.Base;

namespace ProductCatalog.Data.Entities {
    public class ToolType : BaseEntity {
        public string Name { get; set; }
        public string DescriptionApplicationOptions { get; set; }
    }
}
