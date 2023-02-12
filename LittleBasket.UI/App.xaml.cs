using LittleBasket.Domain.Interfaces.Commands;
using LittleBasket.Domain.Models;
using LittleBasket.Infrastructure.Extensions;
using LittleBasket.Service.Stores;
using LittleBasket.UI.Commands;
using LittleBasket.UI.Extensions;
using LittleBasket.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Windows;

namespace LittleBasket.UI
{
    public partial class App : Application
    {
        private readonly IHost? _host;
        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    //Infrustructure 
                    services.AddMySqlDatabase(context.Configuration);
                    services.AddBasketInfrustructure();

                    //Commands
                    services.AddSingleton<IUpdateCommand<List<Check>>, UpdateCheckCommand>();
                    services.AddSingleton<IUpdateCommand<Product>, UpdateProductCommand>();

                    //Stores
                    services.AddSingleton<SelectedBasketProductStore>();
                    services.AddSingleton<NavigationStore>();
                    services.AddSingleton<BasketBuyStore>();
                    services.AddSingleton<BasketHistoryStore>();
                    services.AddSingleton<BasketProductStore>();

                    //ViewModels
                    services.AddViewModels();
                    services.AddSingleton<MainWindow>((services) => new MainWindow()
                    {
                        DataContext = services.GetRequiredService<MainViewModal>()
                        
                    });
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host!.StartAsync();

            _host.Services.GetRequiredService<BasketViewModel>().LoadProductsCommand.Execute(null);
            _host.Services.GetRequiredService<BasketViewModel>().LoadHistoryCommand.Execute(null);


            var startupForm = _host.Services.GetRequiredService<MainWindow>();
            startupForm.Show();



            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host!.StopAsync();
            _host.Dispose();
            base.OnExit(e);
        }
    }
}
