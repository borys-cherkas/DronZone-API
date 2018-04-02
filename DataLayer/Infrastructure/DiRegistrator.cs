using Common.Constants;
using DataLayer.DbContext;
using DataLayer.DbContext.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataLayer.Infrastructure
{

    public static class DiRegistrator
    {
        public static void Register(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(DbConstants.LocalConnectionString));

            services.AddScoped<IDbInitializer, DbInitializer>();
        }
    }
}
