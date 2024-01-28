namespace WarehouseService.Profiles
{
    using AutoMapper;
    using Data.Models;
    using Data.ViewModels;
    using Messaging.Models;

    public class WarehouseProfile : Profile
    {
        public WarehouseProfile()
        {
            CreateMap<Warehouse, WarehouseViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));

            CreateMap<WarehouseReadModel, Warehouse>();

            CreateMap<Product, ProductViewModel>();

            CreateMap<ProductCreatedDto, Product>()
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.WarehouseId, opt => opt.MapFrom(src => Guid.Parse(src.WarehouseId)));
        }
    }
}
