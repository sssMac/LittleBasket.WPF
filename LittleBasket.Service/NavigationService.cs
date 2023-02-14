using LittleBasket.Domain.Base;
using LittleBasket.Domain.Interfaces.Services;
using LittleBasket.Service.Stores;
using System;


namespace LittleBasket.Service
{
    //Сервис контроля текущей страницы
    public class NavigationService<TViewModel> where TViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public NavigationService(NavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

		//Редирект на тип страницы который создает "Func<TViewModel> createViewModel"
		public void Navigate()
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
