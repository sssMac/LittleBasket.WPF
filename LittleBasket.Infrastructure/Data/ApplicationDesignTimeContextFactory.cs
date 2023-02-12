using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace LittleBasket.Infrastructure.Data
{
    public class ApplicationDesignTimeContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {

        public ApplicationContext CreateDbContext(string[] args = null)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false)
            .Build();

            return new ApplicationContext(
                new DbContextOptionsBuilder<ApplicationContext>()
                .UseMySQL(configuration.GetConnectionString("DefaultConnection")).Options);
        }
    }
}
