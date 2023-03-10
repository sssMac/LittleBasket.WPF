using LittleBasket.Domain.Base;
using LittleBasket.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBasket.UI.ViewModels
{
	//Класс отвечабщий за элемент из списка продуктов
	//DataContext для компонента BasketProductListItem
	public class BasketProductListItemViewModel : ViewModelBase
	{
        //Модель продукта на UI
        public Product Product { get; set; }
        public string ProductName => Product.Name;
        public bool IsVisible => Product.IsVisible;

        public BasketProductListItemViewModel(Product product)
        {
            Product = product;
        }
    }
}
