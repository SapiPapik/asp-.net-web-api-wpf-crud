using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.BLL.Contracts.Dtos {
    public class ToolTypeDto {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DescriptionApplicationOptions { get; set; }
    }
}
