using System;
using Microsoft.EntityFrameworkCore;
using Turnero.Domain.Entities;

namespace Turnero.Data.Context
{
    public class TurneroDbContext : DbContext
    {
        public TurneroDbContext(DbContextOptions<TurneroDbContext> options)
            : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Lab> Labs { get; set; }
        public DbSet<Process> Processes { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationExtension> ReservationExtensions { get; set; }
        public DbSet<Holiday> Holidays { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Tabla Roles
            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Roles");
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Name)
                      .IsRequired()
                      .HasMaxLength(50);
            });

            // Tabla Users
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Username)
                      .IsRequired()
                      .HasMaxLength(50);
                entity.Property(u => u.Email)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(u => u.PasswordHash)
                      .IsRequired()
                      .HasMaxLength(200);
                entity.Property(u => u.CreatedAt)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(u => u.Role)
                      .WithMany(r => r.Users)
                      .HasForeignKey(u => u.RoleId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Tabla Labs
            modelBuilder.Entity<Lab>(entity =>
            {
                entity.ToTable("Labs");
                entity.HasKey(l => l.Id);
                entity.Property(l => l.Name)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(l => l.Location)
                      .HasMaxLength(200);
                entity.Property(l => l.Description)
                      .HasMaxLength(500);
            });

            // Tabla Processes
            modelBuilder.Entity<Process>(entity =>
            {
                entity.ToTable("Processes");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(p => p.DefaultDurationMinutes)
                      .IsRequired();

                entity.HasOne(p => p.Lab)
                      .WithMany(l => l.Processes)
                      .HasForeignKey(p => p.LabId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Tabla Devices
            modelBuilder.Entity<Device>(entity =>
            {
                entity.ToTable("Devices");
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Name)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(d => d.DurationMinutes)
                      .IsRequired();
                entity.Property(d => d.ConfigJson)
                      .HasColumnType("TEXT")
                      .HasDefaultValue("{}");

                entity.HasOne(d => d.Process)
                      .WithMany(p => p.Devices)
                      .HasForeignKey(d => d.ProcessId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Lab)
                      .WithMany(l => l.Devices)
                      .HasForeignKey(d => d.LabId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Tabla Reservations
            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.ToTable("Reservations");
                entity.HasKey(r => r.Id);

                entity.Property(r => r.StartTime)
                      .IsRequired();
                entity.Property(r => r.EndTime)
                      .IsRequired();

                // Convertir tu enum ReservationStatus a string en BD
                entity.Property(r => r.Status)
                      .HasConversion<string>()
                      .IsRequired()
                      .HasMaxLength(20);

                entity.Property(r => r.CancelledByAdmin)
                      .IsRequired()
                      .HasDefaultValue(false);

                entity.Property(r => r.Observations)
                      .HasMaxLength(1000);
                entity.Property(r => r.CreatedAt)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(r => r.User)
                      .WithMany(u => u.Reservations)
                      .HasForeignKey(r => r.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.Device)
                      .WithMany(d => d.Reservations)
                      .HasForeignKey(r => r.DeviceId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Índice para evitar solapamientos
                entity.HasIndex(r => new { r.DeviceId, r.StartTime, r.EndTime })
                      .HasDatabaseName("IX_Reservations_Device_Time");
            });

            // Tabla ReservationExtensions
            modelBuilder.Entity<ReservationExtension>(entity =>
            {
                entity.ToTable("ReservationExtensions");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.RequestedAt)
                      .IsRequired()
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.NewEndTime)
                      .IsRequired();
                entity.Property(e => e.Observations)
                      .HasMaxLength(1000);

                entity.HasOne(e => e.Reservation)
                      .WithMany(r => r.Extensions)
                      .HasForeignKey(e => e.ReservationId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Tabla Holidays
            modelBuilder.Entity<Holiday>(entity =>
            {
                entity.ToTable("Holidays");
                entity.HasKey(h => h.Id);
                entity.Property(h => h.Date)
                      .IsRequired();
                entity.Property(h => h.Description)
                      .HasMaxLength(200);
            });
        }
    }
}
