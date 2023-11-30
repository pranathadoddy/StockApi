using AutoMapper;
using Framework.Repository;
using Stock.DataAccess.Application;
using Stock.Dto.Common;
using Stock.RepositoryContract.Common;

namespace Stock.Repository.Common
{
    public class SupplierRepository : BaseRepository<StockContext, MSupplier, SupplierDto, int>, ISupplierRepository
    {
        public SupplierRepository(StockContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
