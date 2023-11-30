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
    public class SupplierItemRepository : BaseRepository<StockContext, MSupplierItem, SupplierItemDto, int>, ISupplierItemRepository
    {
        public SupplierItemRepository(StockContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task<PagedSearchResult<SupplierItemDto>> PagedSearchAsync(PagedSearchParameter parameter)
        {
            var dbSet = this.Context.MSupplierItems
                .Include(item => item.IdSupplierNavigation)
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

        protected override void EntityToDto(MSupplierItem entity, SupplierItemDto dto)
        {
            dto.SupplierName = entity.IdSupplierNavigation?.Name;
            dto.ItemName = entity.IdItemNavigation?.Name;

            base.EntityToDto(entity, dto);
        }
    }
}
