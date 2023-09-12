using BasicBilling.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace BasicBilling.API
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity => 
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Consumption>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Consumption>()
                    .HasOne(co => co.Service)
                    .WithMany(se => se.Consumptions)
                    .HasForeignKey(co => co.ServiceId);

            modelBuilder.Entity<Consumption>()
                    .HasOne(co => co.Client)
                    .WithMany(cli => cli.Consumptions)
                    .HasForeignKey(co => co.ClientId);


            modelBuilder.Entity<Consumption>()
               .HasOne(e => e.PaymentDetail)
               .WithOne(e => e.Consumption)
               .HasForeignKey<Payment>(e => e.ConsumptionId)
               .IsRequired();

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Consumption> Consumptions { get; set; }

        public DbSet<Payment> Payments { get; set; }
    }
}