﻿using System.Linq;
using Common.Constants;
using Common.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.DbContext
{
    public sealed class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
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


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DbConstants.LocalConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            DisableCascadeDeleting(builder);

            //builder.Entity<AppUser>()
            //    .HasOne(p => p.Participant)
            //    .WithOne(i => i.AppUser)
            //    .HasForeignKey<Participant>(b => b.AppUserId);

            //builder.Entity<AthleticField>()
            //    .HasOne(m => m.EquipmentIntegrationRequest)
            //    .WithOne(d => d.AthleticField)
            //    .IsRequired(false);

            //builder.Entity<Reservation_Participant>()
            //    .HasIndex(u => u.Code)
            //    .IsUnique();

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
