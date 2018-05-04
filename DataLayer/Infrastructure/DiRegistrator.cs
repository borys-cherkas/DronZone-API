﻿using Common.Constants;
using Common.Models.Identity;
using DataLayer.DbContext;
using DataLayer.DbContext.Abstractions;
using Microsoft.AspNetCore.Identity;
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
                options.UseSqlServer(DbConstants.LocalConnectionString);

                // Register the entity sets needed by OpenIddict.
                // Note: use the generic overload if you need
                // to replace the default OpenIddict entities
                options.UseOpenIddict();
            });

            services.AddScoped<IDbInitializer, DbInitializer>();
        }
    }
}