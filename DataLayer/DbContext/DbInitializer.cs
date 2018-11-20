using System;
using System.Threading.Tasks;
using Common.Constants;
using Common.Models;
using Common.Models.Identity;
using DataLayer.DbContext.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;
using OpenIddict.Core;
using OpenIddict.EntityFrameworkCore.Models;

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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly OpenIddictApplicationManager<OpenIddictApplication> _iddictApplicationManager;

        public DbInitializer(
            AppDbContext context, 
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            OpenIddictApplicationManager<OpenIddictApplication> iddictApplicationManager)
        {
            _dbContext = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _iddictApplicationManager = iddictApplicationManager;
        }

        public async Task InitializeAsync()
        {
            await _dbContext.Database.EnsureCreatedAsync();

            await InitializeIdServAsync();
            await InitializeRolesAsync();
            await InitializeUsersAsync();
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
            string[] roles = { AppRoles.User, AppRoles.Administrator };

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

        private async Task InitializeUsersAsync()
        {
            var adminUsername1 = "admin1@test.com";
            var adminUsername2 = "admin2@test.com";
            var pass = "Test123!";

            await AddAdminIfNotExistAsync(adminUsername1, pass);
            await AddAdminIfNotExistAsync(adminUsername2, pass);
        }

        private async Task AddAdminIfNotExistAsync(string username, string password)
        {
            var isExist = await _userManager.FindByEmailAsync(username) != null;
            if (!isExist)
            {
                var person = new Person()
                {
                    FirstName = username,
                    LastName = username
                };
                var result = await _dbContext.People.AddAsync(person);
                person = result.Entity;
                var admin1 = new ApplicationUser()
                {
                    UserName = username,
                    Email = username,
                    PersonId = person.Id
                };
                await _userManager.CreateAsync(admin1, password);
                await _userManager.AddToRoleAsync(admin1, AppRoles.Administrator);
            }
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
