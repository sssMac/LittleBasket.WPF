using LittleBasket.Domain.Base;
using LittleBasket.Service.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBasket.UI.ViewModels
{
    public class MainViewModal : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
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

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
