using Framework.Service;
using Stock.Dto.Common;
using Stock.RepositoryContract.Common;
using Stock.ServiceContract.Common;

namespace Stock.Service.Common
{
    public class LocationService : BaseService<LocationDto, int, ILocationRepository>, ILocationService
    {
        public LocationService(ILocationRepository repository) : base(repository)
        {
        }
    }
}
