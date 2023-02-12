using LittleBasket.Domain.Base;
using LittleBasket.Domain.Models;
using LittleBasket.Service.Stores;
using LittleBasket.UI.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace LittleBasket.UI.ViewModels
{
    public class BasketProductListViewModel : ViewModelBase
    {
        private readonly BasketProductStore _basketProductStore;
        private readonly SelectedBasketProductStore _selectedBasketProductStore;
        private readonly BasketBuyStore _basketBuyStore;

        private readonly ObservableCollection<BasketProductListItemViewModel> _basketProductListItemViewModels;
        public IEnumerable<BasketProductListItemViewModel> BasketProductListItemViewModels => _basketProductListItemViewModels;

        private BasketProductListItemViewModel _selectedBasketProductListItemViewModels;
        public BasketProductListItemViewModel SelectedBasketProductListItemViewModels
        {
            get
            {
                return _selectedBasketProductListItemViewModels;
            }
            set
            {
                _selectedBasketProductListItemViewModels = value;
                OnPropertyChanged(nameof(SelectedBasketProductListItemViewModels));

                _selectedBasketProductStore.SelectedProduct = value?.Product;
            }
        }
        public ICommand AddProductToBasketCommand { get; }

        private bool _isActiveBuy;
        public bool IsActiveBuy
        {
            get { return _isActiveBuy; }
            set
            {
                _isActiveBuy = value;;
            }
        }

        public BasketProductListViewModel(
            BasketProductStore basketProductStore,
            SelectedBasketProductStore selectedBasketProductStore,
            BasketBuyStore basketBuyStore)
        {
            _basketProductStore = basketProductStore;
            _selectedBasketProductStore = selectedBasketProductStore;
            _basketBuyStore = basketBuyStore;

            _basketProductListItemViewModels = new ObservableCollection<BasketProductListItemViewModel>();

            AddProductToBasketCommand = new AddToBasketCommand(_basketProductStore, _selectedBasketProductStore);

            _basketProductStore.BasketProductsLoaded += OnBasketProductsLoaded;
            _basketBuyStore.IsActiveChanged += OnActiveChanged;
            _basketProductStore.BasketProductUpdated += OnBasketProductUpdated;
            _basketProductStore.ProductAdded += OnProductAdded;
        }

        private void OnProductAdded(Product obj)
        {
            _basketProductListItemViewModels.Add(new BasketProductListItemViewModel(obj));
            _basketProductListItemViewModels.OrderBy(x => x);
        }

        private void OnBasketProductUpdated(Product product)
        {
            _basketProductListItemViewModels
                .FirstOrDefault(p => p.Product.Name == product.Name).Product = product;
        }

        private void OnActiveChanged(bool obj)
        {
            _isActiveBuy = obj;
            OnPropertyChanged(nameof(IsActiveBuy));
        }

        protected override void Dispose()
        {
            _basketProductStore.BasketProductsLoaded -= OnBasketProductsLoaded;
            _basketBuyStore.IsActiveChanged -= OnActiveChanged;
            _basketProductStore.BasketProductUpdated -= OnBasketProductUpdated;
            _basketProductStore.ProductAdded -= OnProductAdded;

            base.Dispose();
        }
        private void OnBasketProductsLoaded()
        {
            _basketProductListItemViewModels.Clear();

            foreach (Product product in _basketProductStore.Products)
            {
                _basketProductListItemViewModels.Add(new BasketProductListItemViewModel(product));
            }
        }
    }
}
