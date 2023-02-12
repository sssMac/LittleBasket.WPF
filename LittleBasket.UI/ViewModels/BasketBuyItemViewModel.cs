using LittleBasket.Domain.Base;
using LittleBasket.Domain.Models;
using LittleBasket.Service.Stores;
using LittleBasket.UI.Commands;
using System;
using System.Windows.Input;

namespace LittleBasket.UI.ViewModels
{
    public class BasketBuyItemViewModel : ViewModelBase
    {
        private readonly BasketBuyStore _basketBuyStore;

        private Guid _productId; 
        public Guid ProductId
        {
            get { return _productId; }
            set
            {
                _productId = value;
                OnPropertyChanged(nameof(ProductId));
            }
        }
        private string _productName;
        public string ProductName
        {
            get { return _productName; }
            set
            {
                _productName = value; 
                OnPropertyChanged(nameof(ProductName));
            }
        }
        private int _count;
        public int Count
        {
            get { return _count; }
            set
            {
                _count = value; 
                OnPropertyChanged(nameof(Count));
                OnPropertyChanged(nameof(Sum));
            }
        }
        private decimal _cost;
        public decimal Cost
        {
            get { return _cost; }
            set
            {
                _cost = value;
                OnPropertyChanged(nameof(Cost));
                OnPropertyChanged(nameof(Sum));
            }
        }

        public decimal Sum => Count * Cost;

        public ICommand DeleteCheckCommand { get; }

        public BasketBuyItemViewModel(BasketBuyStore basketBuyStore)
        {
            DeleteCheckCommand = new DeleteFromBasketCommand(basketBuyStore);
        }

        public void Update(Check check)
        {
            _productId = check.ProductId;
            _productName = check.ProductName;
            _count = check.Count;
            _cost = check.Cost;

            OnPropertyChanged(nameof(ProductId));
            OnPropertyChanged(nameof(ProductName));
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged(nameof(Cost));
        }
    }
}
