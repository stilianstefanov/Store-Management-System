namespace CreditService.Profiles
{
    using AutoMapper;
    using Data.Models;
    using Data.ViewModels.PurchasedProduct;

    public class PurchasedProductProfile : Profile
    {
        public PurchasedProductProfile()
        {
            CreateMap<PurchasedProductCreateModel, PurchasedProduct>();
        }
    }
}
