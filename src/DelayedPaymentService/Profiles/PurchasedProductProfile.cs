namespace DelayedPaymentService.Profiles
{
    using System.Globalization;
    using AutoMapper;
    using Data.Models;
    using Data.ViewModels.PurchasedProduct;
    using ProductService;

    public class PurchasedProductProfile : Profile
    {
        public PurchasedProductProfile()
        {
            CreateMap<PurchasedProductCreateModel, PurchasedProduct>()
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.BoughtQuantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.PurchasePrice, opt => opt.MapFrom(src => src.Price));

            CreateMap<PurchasedProduct, PurchasedProductViewModel>()
                .ForMember(dest => dest.ProductDetails, opt => opt.Ignore());

            CreateMap<GrpcProductModel, ProductDetailsViewModel>()
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.ProductId));

            CreateMap<PurchasedProductCreateModel, GrpcProductStockDecreaseModel>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.ToString(CultureInfo.InvariantCulture)));
        }
    }
}
