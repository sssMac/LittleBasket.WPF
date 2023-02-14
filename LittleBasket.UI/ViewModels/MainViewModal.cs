using LittleBasket.Domain.Base;
using LittleBasket.Service.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBasket.UI.ViewModels
{
	//Класс связывающий MainWindow (главное окно)
	public class MainViewModal : ViewModelBase
    {
        //Хранилище навигации между страницами
        private readonly NavigationStore _navigationStore;

        //ViewModels страниц на которые можно переключиться, они передаются в curentViewModel
        //для навигации между страницами
        private readonly BasketViewModel _basketViewModel;
        private readonly ReferenceBookViewModel _referenceBookViewModel;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public MainViewModal(NavigationStore navigationStore,
            BasketViewModel basketViewModel)
        {
            _navigationStore = navigationStore;
            _basketViewModel = basketViewModel;

            _navigationStore.CurrentViewModel = _basketViewModel;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }
        
        //Ивент-подписка: изменяет текущее окно
        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
