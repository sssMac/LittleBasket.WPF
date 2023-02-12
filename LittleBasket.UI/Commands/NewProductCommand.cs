using LittleBasket.Domain.Base;
using LittleBasket.Domain.Models;
using LittleBasket.Service.Stores;
using System;
using System.Threading.Tasks;

namespace LittleBasket.UI.Commands
{
    public class NewProductCommand : AsyncCommandBase
    {
        private readonly BasketProductStore _basketProductStore;

        public NewProductCommand(BasketProductStore basketProductStore)
        {
            _basketProductStore = basketProductStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {

            if(parameter is string)
            {

                await _basketProductStore.AddNewProduct((string)parameter);
            }
        }
    }
}
