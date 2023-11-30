using AutoMapper;
using Framework.Repository;
using Stock.DataAccess.Application;
using Stock.Dto.Common;
using Stock.RepositoryContract.Common;

namespace Stock.Repository.Common
{
    public class ItemRepository : BaseRepository<StockContext, MItem, ItemDto, int>, IItemRepository
    {
        public ItemRepository(StockContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
