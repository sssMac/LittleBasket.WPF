using LittleBasket.Domain.Base;
using LittleBasket.UI.Commands;
using LittleBasket.Service.Stores;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using LittleBasket.Service;

namespace LittleBasket.UI.ViewModels
{
    public class ReferenceBookViewModel : ViewModelBase
    {
        public ReferenceBookListViewModel ReferenceBookListViewModel { get; set; }

        private string _searchInput;
        public string SearchInput
        {
            get => _searchInput;
            set
            {
                _searchInput = value;
                OnPropertyChanged(nameof(SearchInput));
            }
        }

        public ICommand GoToBasketViewModel { get; }

        public ICommand AddProductToBookCommand { get; }

        public ReferenceBookViewModel(
            NavigationService<BasketViewModel> navigationService,
            ReferenceBookListViewModel referenceBookListViewModel, BasketProductStore basketProductStore)
        {
            ReferenceBookListViewModel = referenceBookListViewModel;

            GoToBasketViewModel = new NavigateCommand<BasketViewModel>(navigationService);
            AddProductToBookCommand = new NewProductCommand(basketProductStore);
        }

    }
}
