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

            services.AddTransient<IDroneFilterService, DroneFilterService>();
            services.AddTransient<IPersonService, PersonService>();
            services.AddTransient<IZoneService, ZoneService>();
        }
    }
}