using Framework.Service;
using Stock.Dto.Common;
using Stock.RepositoryContract.Common;
using Stock.ServiceContract.Common;

namespace Stock.Service.Common
{
    public class SupplierService : BaseService<SupplierDto, int, ISupplierRepository>, ISupplierService
    {
        public SupplierService(ISupplierRepository repository) : base(repository)
        {
        }
    }
}
