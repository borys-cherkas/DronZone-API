using System.IO;
using DataLayer.DbContext;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace DronZone_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args)
                .Seed()
                .Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseApplicationInsights()
                .UseStartup<Startup>()
                .Build();
    }
}
