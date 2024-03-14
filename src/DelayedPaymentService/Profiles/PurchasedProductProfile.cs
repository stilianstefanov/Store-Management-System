namespace DelayedPaymentService.Profiles
{
    using AutoMapper;
    using Data.Models;
    using Data.ViewModels.PurchasedProduct;
    using ProductService;

    public class PurchasedProductProfile : Profile
    {
        public PurchasedProductProfile()
        {
            CreateMap<PurchasedProductCreateModel, PurchasedProduct>();

            CreateMap<PurchasedProduct, PurchasedProductViewModel>()
                .ForMember(dest => dest.ProductDetails, opt => opt.Ignore());

            CreateMap<GrpcProductModel, ProductDetailsViewModel>()
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.ProductId));
        }
    }
}
