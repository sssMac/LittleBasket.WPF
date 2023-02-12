using LittleBasket.Service;
using LittleBasket.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBasket.UI.Extensions
{
    public static class AddViewModelsSCEctension
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services)
        {
            services.AddSingleton<BasketBuyItemViewModel>();
            services.AddSingleton<BasketBuyViewModel>();
            services.AddSingleton<BasketHistoryItemViewModel>();
            services.AddSingleton<BasketHistoryViewModel>();
            services.AddSingleton<ReferenceBookListItemViewModel>();
            services.AddSingleton<ReferenceBookListViewModel>();
            services.AddSingleton<BasketProductListViewModel>();

            services.AddSingleton<ReferenceBookViewModel>();
            services.AddSingleton<BasketViewModel>();
            services.AddSingleton<MainViewModal>();

            //Navigation
            services.AddSingleton<Func<BasketViewModel>>((s) => () => s.GetRequiredService<BasketViewModel>());
            services.AddSingleton<NavigationService<BasketViewModel>>();

            services.AddSingleton<Func<ReferenceBookViewModel>>((s) => () => s.GetRequiredService<ReferenceBookViewModel>());
            services.AddSingleton<NavigationService<ReferenceBookViewModel>>();


            return services;
        }

    }
}
