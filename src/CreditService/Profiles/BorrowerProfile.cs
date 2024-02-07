namespace CreditService.Profiles
{
    using AutoMapper;
    using Data.Models;
    using Data.ViewModels;

    public class BorrowerProfile : Profile
    {
        public BorrowerProfile()
        {
            CreateMap<Borrower, BorrowerViewModel>();
        }
    }
}
