using LittleBasket.Domain.Base;
using LittleBasket.Domain.Models;
using LittleBasket.Service.Stores;
using LittleBasket.UI.Commands;
using LittleBasket.Service.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LittleBasket.UI.ViewModels
{
    public class BasketBuyViewModel : ViewModelBase
    {
        private readonly BasketProductStore _basketProductStore;
        private readonly BasketBuyStore _basketBuyStore;

        private DateTime _checkDate;
        public DateTime CheckDate
        {
            get { return _checkDate; }
            set { _checkDate = value; }
        }

        private readonly ObservableCollection<BasketBuyItemViewModel> _basketBuyItemViewModels;
        public IEnumerable<BasketBuyItemViewModel> BasketBuyItemViewModels => _basketBuyItemViewModels;
        public ICommand DeleteAllCheckCommand { get; }
        public ICommand SaveCheckToHistoryCommand { get; }

        private bool _isActiveBuy;
        public bool IsActiveBuy
        {
            get { return _isActiveBuy; }
            set
            {
                _isActiveBuy = value; ;
            }
        }

        public BasketBuyViewModel(BasketProductStore basketProductStore, BasketBuyStore basketBuyStore)
        {
            _checkDate = DateTime.Now;

            _basketProductStore = basketProductStore;
            _basketBuyStore = basketBuyStore;
            _basketBuyItemViewModels = new ObservableCollection<BasketBuyItemViewModel>();

            DeleteAllCheckCommand = new DeleteFromBasketCommand(basketBuyStore);
            SaveCheckToHistoryCommand = new SaveCheckToHistoryCommand(basketBuyStore, _basketBuyItemViewModels);

            _basketProductStore.ProductAddedToBasket += OnProductAddedToBasket;
            _basketBuyStore.CheckUpdated += OnCheckUpdated;
            _basketBuyStore.BasketCheckItemDeleted += OnBasketCheckItemDeleted;
            _basketBuyStore.BasketCheckReset += OnBasketCheckReset;

            _basketBuyStore.IsActiveChanged += OnActiveChanged;

        }
        private void OnActiveChanged(bool obj)
        {
            _isActiveBuy = obj;
            OnPropertyChanged(nameof(IsActiveBuy));
        }
        private void OnCheckUpdated(Check obj)
        {
            BasketBuyItemViewModel basketBuyItemViewModels =
                _basketBuyItemViewModels.FirstOrDefault(y => y.ProductId == obj.ProductId);

            if (basketBuyItemViewModels != null)
            {
                basketBuyItemViewModels.Update(obj);
            };
        }

        private void OnBasketCheckReset()
        {
            _basketBuyItemViewModels.Clear();
        }

        private void OnBasketCheckItemDeleted(Check check)
        {
            var obj = _basketBuyItemViewModels.FirstOrDefault(c => c.ProductId== check.ProductId);
            _basketBuyItemViewModels.Remove(obj);
        }

        private void OnProductAddedToBasket(Product product)
        {
            if (product != null)
            {
                var isExist = _basketBuyItemViewModels.Any(c =>
                c.ProductName == product.Name);
                if (isExist && _basketBuyItemViewModels.Count != 0)
                {
                    _basketBuyItemViewModels.FirstOrDefault(c => c.ProductName == product.Name)
                        .Count++;
                    return;
                }

                _basketBuyItemViewModels?.Add(new BasketBuyItemViewModel(_basketBuyStore)
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Cost = 0,
                    Count = 1
                });
            }
        }
        protected override void Dispose()
        {
            _basketProductStore.ProductAddedToBasket -= OnProductAddedToBasket;
            _basketBuyStore.CheckUpdated -= OnCheckUpdated;
            _basketBuyStore.BasketCheckItemDeleted -= OnBasketCheckItemDeleted;
            _basketBuyStore.BasketCheckReset -= OnBasketCheckReset;
            _basketBuyStore.IsActiveChanged -= OnActiveChanged;

            base.Dispose();
        }
    }
}
