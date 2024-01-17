namespace WarehouseService.Profiles
{
    using AutoMapper;
    using Data.Models;
    using Data.ViewModels;

    public class WarehouseProfile : Profile
    {
        public WarehouseProfile()
        {
            CreateMap<Warehouse, WarehouseViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));

            CreateMap<WarehouseReadModel, Warehouse>();
        }
    }
}
