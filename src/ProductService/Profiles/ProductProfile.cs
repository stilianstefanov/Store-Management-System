namespace ProductService.Profiles
{
    using AutoMapper;
    using Data.Models;
    using Data.ViewModels;

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
        }
    }
}
