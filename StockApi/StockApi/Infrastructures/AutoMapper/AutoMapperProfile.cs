using AutoMapper;
using Stock.DataAccess.Application;
using Stock.Dto.Common;

namespace StockApi.Infrastructures.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            this.CreateMap<MSupplier, SupplierDto>().ReverseMap();
            this.CreateMap<MItem, ItemDto>().ReverseMap();
            this.CreateMap<MLocation, LocationDto>().ReverseMap();
            this.CreateMap<MItemLocation, ItemLocationDto>().ReverseMap();
            this.CreateMap<MSupplierItem, SupplierItemDto>().ReverseMap();
        }
    }
}
