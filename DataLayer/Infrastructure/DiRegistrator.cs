﻿using Common.Constants;
using DataLayer.DbContext;
using DataLayer.DbContext.Abstractions;
using DataLayer.Repositories;
using DataLayer.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataLayer.Infrastructure
{

    public static class DiRegistrator
    {
        public static void Register(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(DbConstants.AzureConnectionString, b => b.MigrationsAssembly("DronZone-API"));

                // Register the entity sets needed by OpenIddict.
                // Note: use the generic overload if you need
                // to replace the default OpenIddict entities
                options.UseOpenIddict();
            });

            services.AddTransient<IAreaFilterRepository, AreaFilterRepository>();
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<IDroneRepository, DroneRepository>();
            services.AddTransient<IZoneRepository, ZoneRepository>();
            services.AddTransient<IZoneSettingsRepository, ZoneSettingsRepository>();
            services.AddTransient<IMapRectanglesRepository, MapRectanglesRepository>();
            services.AddTransient<IZoneValidationRequestRepository, ZoneValidationRequestRepository>();
            
            services.AddScoped<IDbInitializer, DbInitializer>();
        }
    }
}
