using AutoMapper;
using BusinessLayer.Filters;
using Common.Models;
using Common.Models.Additional;
using DronZone_API.ViewModels;
using DronZone_API.ViewModels.Drone.List;
using DronZone_API.ViewModels.Filter;
using DronZone_API.ViewModels.Filter.List;
using DronZone_API.ViewModels.Zone;

namespace DronZone_API.Infrastructure
{
    public static class MappingsRegistrator
    {
        public static void RegisterMappings(IMapperConfigurationExpression mapper)
        {
            BusinessLayer.Infrastructure.MappingsRegistrator.RegisterMappings(mapper);

            RegisterSomeEntityMappings(mapper);
        }

        private static void RegisterSomeEntityMappings(IMapperConfigurationExpression mapper)
        {
            mapper.CreateMap<AddZoneValidationRequestViewModel, ZoneValidationRequest>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(x => ZoneValidationStatus.WaitingForAdministrator))
                .ForMember(dest => dest.RequestType, opt => opt.MapFrom(x => ZoneValidationType.Creation))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Reason));

            mapper.CreateMap<ModifyZoneValidationRequestViewModel, ZoneValidationRequest>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(x => ZoneValidationStatus.WaitingForAdministrator))
                .ForMember(dest => dest.RequestType, opt => opt.MapFrom(x => ZoneValidationType.Modifying))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Reason))
                .ForMember(dest => dest.TargetZoneId, opt => opt.MapFrom(src => src.ZoneId));

            mapper.CreateMap<ZoneListFilterViewModel, ZoneListFilter>()
                .ReverseMap();

            mapper.CreateMap<DroneListFilterViewModel, DroneListFilter>()
                .ReverseMap();

            mapper.CreateMap<AddAreaFilterViewModel, AreaFilter>();

            mapper.CreateMap<AreaFilter, FilterDetailedViewModel>();
        }
    }
}