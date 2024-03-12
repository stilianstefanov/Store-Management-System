namespace ProductService.Profiles
{
    using AutoMapper;
    using Data.Models;
    using Data.ViewModels;
    using Messaging.Models;
    using WarehouseService;

    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));

            CreateMap<ProductCreateModel, Product>();

            CreateMap<Product, ProductDetailsViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Warehouse, opt => opt.Ignore());

            CreateMap<ProductUpdateModel, Product>();

            CreateMap<Product, ProductCreatedDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));

            CreateMap<Product, ProductUpdatedDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));

            CreateMap<Product, ProductPartialUpdatedDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));

            CreateMap<GrpcWarehouseModel, WarehouseViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.WarehouseId));

            CreateMap<Product, GrpcProductModel>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id.ToString()));

            CreateMap<Product, ProductDashViewModel>();
        }
    }
}
