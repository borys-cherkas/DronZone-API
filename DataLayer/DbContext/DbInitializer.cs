using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DbContext.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;

namespace DataLayer.DbContext
{

    public static class WebHostDbExtensions
    {
        public static IWebHost Seed(this IWebHost webhost)
        {
            using (var scope = webhost.Services.GetService<IServiceScopeFactory>().CreateScope())
            {
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

        public DbInitializer(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task InitializeAsync()
        {
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
