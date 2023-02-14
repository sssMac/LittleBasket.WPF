using LittleBasket.Domain.Base;
using LittleBasket.Domain.Interfaces;
using LittleBasket.Domain.Interfaces.Services;
using LittleBasket.Infrastructure.Data;
using LittleBasket.Infrastructure.Mapper;
using LittleBasket.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LittleBasket.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
		/// <summary>
		/// Подключение базы данных MySql
		/// </summary>
		public static IServiceCollection AddMySqlDatabase(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseMySQL(config.GetConnectionString("DefaultConnection"));
            });

            services.AddSingleton<ApplicationContextFactory>();

            return services;
        }

		/// <summary>
		/// Добавление инфраструктуры. Регистрация ряда сервисов
		/// </summary>
		public static IServiceCollection AddBasketInfrustructure(this IServiceCollection services)
        {
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IBasketService, BasketService>();
            services.AddSingleton<NavigationService<ViewModelBase>>();
            services.AddAutoMapper(typeof(AppMappingProfile));

            return services;
        }


    }
}
