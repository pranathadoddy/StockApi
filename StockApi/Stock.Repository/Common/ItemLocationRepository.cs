using AutoMapper;
using Framework.Repository;
using Framework.RepositoryContract;
using Microsoft.EntityFrameworkCore;
using Stock.DataAccess.Application;
using Stock.Dto.Common;
using Stock.RepositoryContract.Common;
using System.Linq.Dynamic.Core;

namespace Stock.Repository.Common
{
    public class ItemLocationRepository : BaseRepository<StockContext, MItemLocation, ItemLocationDto, int>, IItemLocationRepository
    {
        public ItemLocationRepository(StockContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task<PagedSearchResult<ItemLocationDto>> PagedSearchAsync(PagedSearchParameter parameter)
        {
            var dbSet = this.Context.MItemLocations
                .Include(item => item.IdLocationNavigation)
                .Include(item => item.IdItemNavigation);

            var queryable =
                string.IsNullOrEmpty(parameter.Filters)
                    ? dbSet.AsQueryable()
                    : dbSet.Where(parameter.Filters);

            queryable =
                string.IsNullOrEmpty(parameter.Keyword)
                    ? queryable
                    : GetKeywordPagedSearchQueryable(queryable, parameter.Keyword);

            return await GetPagedSearchEnumerableAsync(parameter, queryable);
        }

        protected override void EntityToDto(MItemLocation entity, ItemLocationDto dto)
        {
            dto.LocationName = entity.IdLocationNavigation?.Name;
            dto.ItemName = entity.IdItemNavigation?.Name;
            base.EntityToDto(entity, dto);
        }
    }
}
