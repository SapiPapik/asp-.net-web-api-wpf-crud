using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCatalog.BLL.Contracts.Dtos;

namespace ProductCatalog.BLL.Contracts.Contracts {
    public interface IEquipmentService : IBaseEquipmentService, IImportEquipment {
    }
}
