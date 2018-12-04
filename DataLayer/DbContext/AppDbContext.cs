using System.Linq;
using Common.Constants;
using Common.Models;
using Common.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.DbContext
{
    public sealed class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public AppDbContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<Drone> Drones { get; set; }

        public DbSet<DronePositionSnapshot> DronePositionSnapshots { get; set; }

        public DbSet<Person> People { get; set; }

        public DbSet<Zone> Zones { get; set; }

        public DbSet<ZoneValidationRequest> ZoneValidationRequests { get; set; }

        public DbSet<ZoneSettings> ZoneSettingsSet { get; set; }

        public DbSet<AreaFilter> AreaFilters { get; set; }

        public DbSet<MapRectangle> MapRectangles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DbConstants.AzureConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            DisableCascadeDeleting(builder);

            // one-to-one
            builder.Entity<ApplicationUser>()
                .HasOne(p => p.Person)
                .WithOne(i => i.IdentityUser)
                .HasForeignKey<ApplicationUser>(b => b.PersonId)
                .IsRequired();

            // one-to-one
            builder.Entity<Zone>()
                .HasOne(p => p.MapRectangle)
                .WithOne(i => i.Zone)
                .HasForeignKey<MapRectangle>(b => b.ZoneId)
                .IsRequired();

            // one-to-one
            //builder.Entity<Zone>()
            //    .HasOne(z => z.ValidationRequest)
            //    .WithOne()
            //    .HasForeignKey<ZoneValidationRequest>(b => b.TargetZoneId)
            //    .IsRequired(false);

            // one-to-one
            builder.Entity<Zone>()
                .HasOne(p => p.Settings)
                .WithOne(i => i.Zone)
                .HasForeignKey<ZoneSettings>(b => b.ZoneId)
                .IsRequired();

            builder.Entity<Drone>()
                .HasIndex(u => u.Code)
                .IsUnique();
        }

        private void DisableCascadeDeleting(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
