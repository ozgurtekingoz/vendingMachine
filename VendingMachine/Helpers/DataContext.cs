using Microsoft.EntityFrameworkCore;
using RestApiSample.Models.Entities;

namespace VendingMachine.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<PaymentType> PaymentType { get; set; }
        public DbSet<Food> Foods { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Drink>().ToTable("Drink");
            modelBuilder.Entity<Food>().ToTable("Food");
            modelBuilder.Entity<PaymentType>().ToTable("PaymentType");
        }
    }
}
