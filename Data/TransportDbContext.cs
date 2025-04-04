using Microsoft.EntityFrameworkCore;
using UBB_SE_2025_EUROTRUCKERS.Models;

namespace UBB_SE_2025_EUROTRUCKERS.Data
{
    public class TransportDbContext : DbContext
    {
        public TransportDbContext(DbContextOptions<TransportDbContext> options)
            : base(options)
        {
        }

        public DbSet<Company> companies { get; set; }
        public DbSet<Driver> drivers { get; set; }
        public DbSet<Truck> trucks { get; set; }
        public DbSet<Delivery> deliveries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuraciones del modelo
            modelBuilder.Entity<Delivery>()
                .HasOne(d => d.Driver)
                .WithMany()
                .HasForeignKey(d => d.DriverId);

            modelBuilder.Entity<Delivery>()
                .HasOne(d => d.Truck)
                .WithMany()
                .HasForeignKey(d => d.TruckId);

            modelBuilder.Entity<Delivery>()
                .HasOne(d => d.Company)
                .WithMany()
                .HasForeignKey(d => d.CompanyId);

            modelBuilder.Entity<Delivery>().ToTable("deliveries","transport");
        }
    }
}
