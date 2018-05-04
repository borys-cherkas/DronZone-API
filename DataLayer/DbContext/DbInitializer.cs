﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DbContext.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;
using OpenIddict.Core;
using OpenIddict.Models;

namespace DataLayer.DbContext
{

    public static class WebHostDbExtensions
    {
        public static IWebHost Seed(this IWebHost webhost)
        {
            using (var scope = webhost.Services.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Database.EnsureCreated();

                // alternatively resolve UserManager instead and pass that if only think you want to seed are the users
                using (var dbInit = scope.ServiceProvider.GetRequiredService<IDbInitializer>())
                {
                    dbInit.InitializeAsync().GetAwaiter().GetResult();
                }
            }
            return webhost;
        }
    }

    public class DbInitializer : IDbInitializer
    {
        private readonly AppDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly OpenIddictApplicationManager<OpenIddictApplication> _iddictApplicationManager;

        public DbInitializer(
            AppDbContext context, 
            RoleManager<IdentityRole> roleManager, 
            OpenIddictApplicationManager<OpenIddictApplication> iddictApplicationManager)
        {
            _dbContext = context;
            _roleManager = roleManager;
            _iddictApplicationManager = iddictApplicationManager;
        }

        public async Task InitializeAsync()
        {
            await _dbContext.Database.EnsureCreatedAsync();

            await InitializeIdServAsync();
            await InitializeRolesAsync();
        }

        private async Task InitializeIdServAsync()
        {
            // Create a new service scope to ensure the database context is correctly disposed when this methods returns.
            if (await _iddictApplicationManager.FindByClientIdAsync("mvc") == null)
            {
                var descriptor = new OpenIddictApplicationDescriptor
                {
                    ClientId = "mvc",
                    ClientSecret = "901564A5-E7FE-42CB-B10D-61EF6A8F3654",
                    DisplayName = "MVC client application",
                    PostLogoutRedirectUris = {new Uri("http://localhost:53507/signout-callback-oidc")},
                    RedirectUris = {new Uri("http://localhost:53507/signin-oidc")},
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Authorization,
                        OpenIddictConstants.Permissions.Endpoints.Logout,
                        OpenIddictConstants.Permissions.Endpoints.Token
                    }
                };

                await _iddictApplicationManager.CreateAsync(descriptor);
            }

            // To test this sample with Postman, use the following settings:
            //
            // * Authorization URL: http://localhost:54540/connect/authorize
            // * Access token URL: http://localhost:54540/connect/token
            // * Client ID: postman
            // * Client secret: [blank] (not used with public clients)
            // * Scope: openid email profile roles
            // * Grant type: authorization code
            // * Request access token locally: yes
            if (await _iddictApplicationManager.FindByClientIdAsync("postman") == null)
            {
                var descriptor = new OpenIddictApplicationDescriptor
                {
                    ClientId = "postman",
                    DisplayName = "Postman",
                    RedirectUris = {new Uri("https://www.getpostman.com/oauth2/callback")},
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Authorization,
                        OpenIddictConstants.Permissions.Endpoints.Token
                    }
                };

                await _iddictApplicationManager.CreateAsync(descriptor);
            }
        }

        private async Task InitializeRolesAsync()
        {
            string[] roles = { "User", "Administrator" };

            foreach (var role in roles)
            {
                var roleExists = await _roleManager.RoleExistsAsync(role);
                if (!roleExists)
                {
                    var newRole = new IdentityRole(role);
                    await _roleManager.CreateAsync(newRole);
                    // In the real world, there might be claims associated with roles
                    // _roleManager.AddClaimAsync(newRole, new )
                }
            }
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}