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
    //Класс связывающий BasketView (основная страница)
    public class BasketViewModel : ViewModelBase
    {
		//Данные ViewModel используются для binding как DataContext к следущим компонентам
		//в папке Components

		//Component: BasketProductList
		public BasketProductListViewModel BasketProductListViewModel { get; set; }
		//Component: BasketHistory
		public BasketHistoryViewModel BasketHistoryViewModel { get; set; }
		//Component: BasketBuy
		public BasketBuyViewModel BasketBuyViewModel { get; set; }


        //Команды
        //выгрузки продуктов, истории
        public ICommand LoadProductsCommand { get; }
        public ICommand LoadHistoryCommand { get; }
        //редирект на страницу со справочником
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
