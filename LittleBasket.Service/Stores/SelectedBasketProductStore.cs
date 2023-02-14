using AutoMapper;
using LittleBasket.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBasket.Service.Stores
{
    //Хранилище выбранного продукта
    //было одно из пробных, для этого проекта считаю его +- бесполезным
    public class SelectedBasketProductStore
    {

        private Product _selectedProduct;

        public Product SelectedProduct
        {
            get 
            { 
                return _selectedProduct; 
            }
            set
            {
                _selectedProduct = value;
                SelectedProductChanged?.Invoke();
            }
        }

        public event Action SelectedProductChanged;

        public SelectedBasketProductStore()
        {

        }

    }
}
