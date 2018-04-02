using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer.Infrastructure
{
    public static class DiRegistrator
    {
        public static void Register(IServiceCollection services)
        {
            DataLayer.Infrastructure.DiRegistrator.Register(services);
        }
    }
}
