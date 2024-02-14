namespace CreditService.Profiles
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

            CreateMap<GrpcProductModel, ProductDetailsViewModel>()
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.ProductId));
        }
    }
}
