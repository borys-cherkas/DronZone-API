using AutoMapper;
using Common.Models;
using DronZone_API.ViewModels;
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
                .ForMember(dest => dest.MapRectangle, opt => opt.MapFrom(x => new MapRectangle { East = x.East, North = x.North, South = x.South, West = x.West }));

            mapper.CreateMap<AddAreaFilterViewModel, AreaFilter>();
        }
    }
}