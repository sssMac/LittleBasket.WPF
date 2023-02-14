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
	//Класс отвечающий за общий вид списка продуктов
	//DataContext для компонента BasketProductList
	public class BasketProductListViewModel : ViewModelBase
	{
        //Хранилище продуктов, продукта на который нажали, текущей покупки
        private readonly BasketProductStore _basketProductStore;
        private readonly SelectedBasketProductStore _selectedBasketProductStore;
        private readonly BasketBuyStore _basketBuyStore;

        //Коллекция всех доступных продуктов
        private readonly ObservableCollection<BasketProductListItemViewModel> _basketProductListItemViewModels;
        public IEnumerable<BasketProductListItemViewModel> BasketProductListItemViewModels => _basketProductListItemViewModels;

        //Выбранный продукт
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
        
        //Команда добавления продукта в текущую покупку
        public ICommand AddProductToBasketCommand { get; }

        //Проверка на доступность к добавлению товаров в корзину
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

        //Ивент-подписка: добавление нового продукта в список
        private void OnProductAdded(Product obj)
        {
            _basketProductListItemViewModels.Add(new BasketProductListItemViewModel(obj));
            _basketProductListItemViewModels.OrderBy(x => x);
        }

        //Ивент-подписка: изменения каких-либо полей у продукта, в нашем случае, видимость продукта на главной
        private void OnBasketProductUpdated(Product product)
        {
            _basketProductListItemViewModels
                .FirstOrDefault(p => p.Product.Name == product.Name).Product = product;
        }

        //Ивента: изменение поля isActive, вкл/выкл функции "добавить в корзину"
        private void OnActiveChanged(bool obj)
        {
            _isActiveBuy = obj;
            OnPropertyChanged(nameof(IsActiveBuy));
        }
        
        //Ивента: выгрузка продуктов из бд
        private void OnBasketProductsLoaded()
        {
            _basketProductListItemViewModels.Clear();

            foreach (Product product in _basketProductStore.Products)
            {
                _basketProductListItemViewModels.Add(new BasketProductListItemViewModel(product));
            }
        }

        //Отписка от событий
        protected override void Dispose()
        {
            _basketProductStore.BasketProductsLoaded -= OnBasketProductsLoaded;
            _basketBuyStore.IsActiveChanged -= OnActiveChanged;
            _basketProductStore.BasketProductUpdated -= OnBasketProductUpdated;
            _basketProductStore.ProductAdded -= OnProductAdded;

            base.Dispose();
        }
    }
}
