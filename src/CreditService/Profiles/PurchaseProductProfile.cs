namespace CreditService.Profiles
{
    using AutoMapper;
    using Data.Models;
    using Data.ViewModels.PurchaseProduct;

    public class PurchaseProductProfile : Profile
    {
        public PurchaseProductProfile()
        {
            CreateMap<PurchaseProductCreateModel, PurchaseProduct>();
        }
    }
}
