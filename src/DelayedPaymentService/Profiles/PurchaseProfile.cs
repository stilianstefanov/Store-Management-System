namespace DelayedPaymentService.Profiles
{
    using System.Globalization;
    using AutoMapper;
    using Data.Models;
    using Data.ViewModels.Purchase;

    public class PurchaseProfile : Profile
    {
        public PurchaseProfile()
        {
            CreateMap<Purchase, PurchaseViewModel>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture)))
                .ForMember(dest => dest.Amount, opt => 
                    opt.MapFrom(src => src.Products.Where(p => !p.IsDeleted).Sum(p => p.BoughtQuantity * p.PurchasePrice)));
        }
    }
}
