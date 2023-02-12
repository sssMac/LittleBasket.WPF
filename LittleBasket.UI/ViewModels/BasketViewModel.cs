using LittleBasket.Domain.Base;
using LittleBasket.UI.Commands;
using LittleBasket.Service.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LittleBasket.Domain.Interfaces.Services;
using LittleBasket.Service;
using LittleBasket.UI.Views;

namespace LittleBasket.UI.ViewModels
{
    public class BasketViewModel : ViewModelBase
    {
        public BasketProductListViewModel BasketProductListViewModel { get; set; }
        public BasketHistoryViewModel BasketHistoryViewModel { get; set; }
        public BasketBuyViewModel BasketBuyViewModel { get; set; }

        public ICommand LoadProductsCommand { get; }
        public ICommand LoadHistoryCommand { get; }
        public ICommand GoToReferenceBookView { get; }
        public BasketViewModel(
            BasketProductStore basketProductStore,
            BasketHistoryStore basketHistoryStore,
            BasketProductListViewModel basketProductListViewModel,
            BasketHistoryViewModel basketHistoryViewModel,
            BasketBuyViewModel basketBuyViewModel,
            NavigationService<ReferenceBookViewModel> navigationService)
        {
            //For DataContext in XMAL
            BasketProductListViewModel = basketProductListViewModel;
            BasketHistoryViewModel = basketHistoryViewModel;
            BasketBuyViewModel = basketBuyViewModel;

            LoadProductsCommand = new LoadProductsCommand(basketProductStore);
            LoadHistoryCommand = new LoadHistoryCommand(basketHistoryStore);
            GoToReferenceBookView = new NavigateCommand<ReferenceBookViewModel>(navigationService);

        }
    }
}
