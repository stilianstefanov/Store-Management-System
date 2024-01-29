namespace ProductService.Profiles
{
    using AutoMapper;
    using Data.Models;
    using Data.ViewModels;
    using Messaging.Models;

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
        }
    }
}
