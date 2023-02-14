using LittleBasket.Domain.Base;
using LittleBasket.Domain.Models;
using LittleBasket.Service.Stores;
using LittleBasket.UI.Commands;
using System;
using System.Windows.Input;

namespace LittleBasket.UI.ViewModels
{
	//Клаас отвещающий за элемент из списка текущей покупки
	//DataContext для компонента BasketBuyItem
	public class BasketBuyItemViewModel : ViewModelBase
	{ 
        //Модель использующаяся на UI
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

        //Команда удаления продукта из корзины
        public ICommand DeleteCheckCommand { get; }

        public BasketBuyItemViewModel(BasketBuyStore basketBuyStore)
        {
            DeleteCheckCommand = new DeleteFromBasketCommand(basketBuyStore);
        }

        //Изменения отдельной группы полей на UI
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
