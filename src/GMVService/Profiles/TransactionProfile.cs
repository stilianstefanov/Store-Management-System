namespace GMVService.Profiles
{
    using System.Globalization;
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
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
                .ForMember(dest => dest.DateTime,
                    opt => opt.MapFrom(src => src.DateTime.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture)));

            CreateMap<TransactionCreateDto, Transaction>()
                .ForMember(dest => dest.Type,
                    opt => opt.MapFrom(src => Enum.Parse<TransactionType>(src.TransactionType)))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.TotalAmount));
        }
    }
}
