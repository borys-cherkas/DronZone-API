using Microsoft.Extensions.DependencyInjection;

namespace DronZone_API.Infrastructure
{
    public static class DiRegistrator
    {
        public static void Register(IServiceCollection services)
        {
            BusinessLayer.Infrastructure.DiRegistrator.Register(services);
        }
    }
}
