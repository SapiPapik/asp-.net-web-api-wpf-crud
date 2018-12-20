﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.BLL.Services.Exceptions {
    public class ObjectAlreadyExistException : Exception {
        public ObjectAlreadyExistException(string message) : base(message) { }
    }
}
