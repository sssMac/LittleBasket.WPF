using Microsoft.EntityFrameworkCore;

namespace LittleBasket.Infrastructure.Data
{
    public class ApplicationContextFactory
    {
        private readonly DbContextOptions<ApplicationContext> _options;

        public ApplicationContextFactory(DbContextOptions<ApplicationContext> options)
        {
            _options = options;
        }

        public ApplicationContext Create()
        {
            return new ApplicationContext(_options);
        }
    }
}
