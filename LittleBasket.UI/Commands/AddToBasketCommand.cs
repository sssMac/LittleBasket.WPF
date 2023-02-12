using LittleBasket.Domain.Base;
using LittleBasket.Service.Stores;
using LittleBasket.Service.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBasket.UI.Commands
{
    public class AddToBasketCommand : CommandBase
    {
        private readonly BasketProductStore _basketProductStore;
        private readonly SelectedBasketProductStore _selectedBasketProductStore;

        public AddToBasketCommand(BasketProductStore basketProductStore, SelectedBasketProductStore selectedBasketProductStore)
        {
            _basketProductStore = basketProductStore;
            _selectedBasketProductStore = selectedBasketProductStore;
        }

        public override async void Execute(object parameter)
        {
            try
            {
                var product = _selectedBasketProductStore.SelectedProduct;
                await _basketProductStore.AddToBasket(product);

            }
            catch (Exception)
            {
                //ErrorMessage = "";
            }

        }
    }
}
