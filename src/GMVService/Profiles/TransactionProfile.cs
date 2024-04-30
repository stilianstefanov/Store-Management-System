namespace GMVService.Profiles
{
    using AutoMapper;
    using Data.Models;
    using Data.ViewModels;

    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionDetailsModel>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));
        }
    }
}
