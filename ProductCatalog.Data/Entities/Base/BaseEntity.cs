using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Data.Entities.Base {
    public abstract class BaseEntity : IGuidId, ICreatedAt, IModifiedAt, IArchive {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public bool IsArchive { get; set; }
    }
}
