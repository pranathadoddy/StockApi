using Framework.Service;
using Stock.Dto.Common;
using Stock.RepositoryContract.Common;
using Stock.ServiceContract.Common;

namespace Stock.Service.Common
{
    public class SupplierItemService : BaseService<SupplierItemDto, int, ISupplierItemRepository>, ISupplierItemService
    {
        public SupplierItemService(ISupplierItemRepository repository) : base(repository)
        {
        }
    }
}
