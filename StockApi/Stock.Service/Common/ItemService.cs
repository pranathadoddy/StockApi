using Framework.Service;
using Stock.Dto.Common;
using Stock.RepositoryContract.Common;
using Stock.ServiceContract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Service.Common
{
    public class ItemService : BaseService<ItemDto, int, IItemRepository>, IItemService
    {
        public ItemService(IItemRepository repository) : base(repository)
        {
        }
    }
}
