using AutoMapper;
using Common.Models;
using DronZone_API.ViewModels;
using DronZone_API.ViewModels.Filter;
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
            mapper.CreateMap<AddZoneViewModel, Zone>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ZoneName))
                .ForMember(dest => dest.MapRectangle, opt => opt.MapFrom(x => new MapRectangle
                {
                    TopLeftLatitude = x.TopLeftLatitude,
                    TopLeftLongitude = x.TopLeftLongitude,
                    BottomRightLatitude = x.BottomRightLatitude,
                    BottomRightLongitude = x.BottomRightLongitude
                }));

            mapper.CreateMap<EditZoneViewModel, Zone>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ZoneId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ZoneName))
                .ForMember(dest => dest.MapRectangle, opt => opt.MapFrom(x => new MapRectangle
                {
                    TopLeftLatitude = x.TopLeftLatitude,
                    TopLeftLongitude = x.TopLeftLongitude,
                    BottomRightLatitude = x.BottomRightLatitude,
                    BottomRightLongitude = x.BottomRightLongitude
                }));

            mapper.CreateMap<AddAreaFilterViewModel, AreaFilter>();

            mapper.CreateMap<AreaFilter, FilterDetailedViewModel>();
        }
    }
}