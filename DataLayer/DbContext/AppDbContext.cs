﻿using System.Linq;
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

        public DbSet<Zone> RegisteredZones { get; set; }

        public DbSet<ZoneSettings> ZoneSettingsSet { get; set; }

        public DbSet<DroneFilter> DroneFilters { get; set; }

        public DbSet<MapRectangle> MapRectangles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DbConstants.LocalConnectionString);
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
            builder.Entity<Zone>()
                .HasOne(p => p.Settings)
                .WithOne(i => i.Zone)
                .HasForeignKey<ZoneSettings>(b => b.ZoneId)
                .IsRequired();

            builder.Entity<Drone>()
                .HasIndex(u => u.Code)
                .IsUnique();

            //builder.Entity<AthleticField>()
            //    .HasOne(m => m.EquipmentIntegrationRequest)
            //    .WithOne(d => d.AthleticField)
            //    .IsRequired(false);

            //builder.Entity<Reservation_Participant>()
            //    .HasKey(x => new { x.ParticipantId, x.ReservationId });
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
