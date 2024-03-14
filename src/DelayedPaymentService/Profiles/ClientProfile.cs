namespace DelayedPaymentService.Profiles
{
    using AutoMapper;
    using Data.ViewModels.Client;
    using Data.Models;

    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));

            CreateMap<ClientCreateModel, Client>();

            CreateMap<ClientUpdateModel, Client>();
        }
    }
}
