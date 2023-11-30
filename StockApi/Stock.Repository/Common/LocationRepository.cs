using AutoMapper;
using Framework.Repository;
using Stock.DataAccess.Application;
using Stock.Dto.Common;
using Stock.RepositoryContract.Common;

namespace Stock.Repository.Common
{
    public class LocationRepository : BaseRepository<StockContext, MLocation, LocationDto, int>, ILocationRepository
    {
        public LocationRepository(StockContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
