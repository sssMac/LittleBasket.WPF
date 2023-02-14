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
        private Check _check;

        public Guid ProductId
        {
            get { return _check.ProductId; }
            set
            {
				_check.ProductId = value;
                OnPropertyChanged(nameof(ProductId));
            }
        }
        public string ProductName
        {
            get { return _check.ProductName; }
            set
            {
				_check.ProductName = value; 
                OnPropertyChanged(nameof(ProductName));
            }
        }
        public int Count
        {
            get { return _check.Count; }
            set
            {
				_check.Count = value; 
                OnPropertyChanged(nameof(Count));
                OnPropertyChanged(nameof(Sum));
            }
        }
        public decimal Cost
        {
            get { return _check.Cost; }
            set
            {
				_check.Cost = value;
                OnPropertyChanged(nameof(Cost));
                OnPropertyChanged(nameof(Sum));
            }
        }

        public decimal Sum => Count * Cost;

        //Команда удаления продукта из корзины
        public ICommand DeleteCheckCommand { get; }

        public BasketBuyItemViewModel(BasketBuyStore basketBuyStore, Check check = null)
        {
			_check = check;
			DeleteCheckCommand = new DeleteFromBasketCommand(basketBuyStore);
        }

        //Изменения отдельной группы полей на UI
        public void Update(Check check)
        {
			_check.ProductId = check.ProductId;
			_check.ProductName = check.ProductName;
			_check.Count = check.Count;
			_check.Cost = check.Cost;

            OnPropertyChanged(nameof(ProductId));
            OnPropertyChanged(nameof(ProductName));
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged(nameof(Cost));
        }
    }
}
