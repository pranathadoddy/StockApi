using Framework.ServiceContract;
using Stock.Dto.Common;

namespace Stock.ServiceContract.Common
{
    public interface IItemService : IBaseService<ItemDto, int>
    {
    }
}
