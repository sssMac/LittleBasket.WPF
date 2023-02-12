using LittleBasket.Domain.Entities;
using LittleBasket.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace LittleBasket.Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Check> Checks { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Product> Products { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
           : base(options) {}

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            builder.Seed();

        }
    }
}
