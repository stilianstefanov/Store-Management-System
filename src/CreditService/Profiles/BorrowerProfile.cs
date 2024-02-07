namespace CreditService.Profiles
{
    using AutoMapper;
    using Data.ViewModels.Borrower;
    using Data.Models;

    public class BorrowerProfile : Profile
    {
        public BorrowerProfile()
        {
            CreateMap<Borrower, BorrowerViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));

            CreateMap<BorrowerCreateModel, Borrower>();
        }
    }
}
