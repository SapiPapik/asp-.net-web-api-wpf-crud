﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Data.Entities.Base {
    public interface IModifiedAt {
        DateTime ModifiedAt { get; set; }
    }
}
