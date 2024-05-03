namespace GMVService.Profiles
{
    using AutoMapper;
    using Data.Models;
    using Data.Models.Enums;
    using Data.ViewModels;
    using Messaging.Models;

    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionDetailsModel>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));

            CreateMap<TransactionCreateDto, Transaction>()
                .ForMember(dest => dest.Type,
                    opt => opt.MapFrom(src => Enum.Parse<TransactionType>(src.TransactionType)))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.TotalAmount));
        }
    }
}
