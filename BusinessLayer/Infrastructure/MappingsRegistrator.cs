using AutoMapper;
using Common.Models;

namespace BusinessLayer.Infrastructure
{
    public static class MappingsRegistrator
    {
        public static void RegisterMappings(IMapperConfigurationExpression mapper)
        {
            DataLayer.Infrastructure.MappingsRegistrator.RegisterMappings(mapper);

            RegisterSomeEntityMappings(mapper);
        }

        private static void RegisterSomeEntityMappings(IMapperConfigurationExpression mapper)
        {
            mapper.CreateMap<ZoneValidationRequest, Zone>().ReverseMap();
        }
    }
}
