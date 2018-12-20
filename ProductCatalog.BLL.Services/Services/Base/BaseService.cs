using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCatalog.DAL.Contracts;

namespace ProductCatalog.BLL.Services.Services.Base {
    public abstract class BaseService {
        protected readonly IUnitOfWork UnitOfWork;

        public BaseService(IUnitOfWork unitOfWork) {
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
    }
}
