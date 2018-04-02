using AutoMapper;

namespace DataLayer.Infrastructure
{
    public static class MappingsRegistrator
    {
        public static void RegisterMappings(IMapperConfigurationExpression mapper)
        {
            RegisterSomeEntityMappings(mapper);
        }

        private static void RegisterSomeEntityMappings(IMapperConfigurationExpression mapper)
        {
            //mapper.CreateMap<AddKindOfSportViewModel, KindOfSport>()
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.SportName))
            //    .ReverseMap();
        }
    }
}