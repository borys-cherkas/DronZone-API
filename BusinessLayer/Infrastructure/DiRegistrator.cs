using BusinessLayer.Services;
using BusinessLayer.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer.Infrastructure
{
    public static class DiRegistrator
    {
        public static void Register(IServiceCollection services)
        {
            DataLayer.Infrastructure.DiRegistrator.Register(services);

            services.AddTransient<IAreaFilterService, AreaFilterService>();
            services.AddTransient<IPersonService, PersonService>();
            services.AddTransient<IZoneService, ZoneService>();
            services.AddTransient<IDroneService, DroneService>();
        }
    }
}