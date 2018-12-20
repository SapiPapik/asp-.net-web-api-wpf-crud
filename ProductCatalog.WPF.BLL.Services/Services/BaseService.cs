using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCatalog.DAL.Contracts;

namespace ProductCatalog.WPF.BLL.Services.Services {
    public abstract class BaseService {
        protected readonly IUnitOfWork UnitOfWork;

        public BaseService(IUnitOfWork uow) {
            UnitOfWork = uow;
        }
    }
}
