using Framework.Service;
using Stock.Dto.Common;
using Stock.RepositoryContract.Common;
using Stock.ServiceContract.Common;

namespace Stock.Service.Common
{
    public class ItemLocationService : BaseService<ItemLocationDto, int, IItemLocationRepository>, IItemLocationService
    {
        public ItemLocationService(IItemLocationRepository repository) : base(repository)
        {
        }
    }
}
