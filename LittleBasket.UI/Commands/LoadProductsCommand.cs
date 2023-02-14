using LittleBasket.Domain.Base;
using LittleBasket.Service.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBasket.UI.Commands
{
	//Команда выгрузки продуктов из бд
	//тригерит ивент BasketProductsLoaded из BasketProductStore
	public class LoadProductsCommand : AsyncCommandBase
    {
        private readonly BasketProductStore _basketProductStore;
        public LoadProductsCommand(BasketProductStore basketProductStore)
        {
            _basketProductStore = basketProductStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _basketProductStore.Load();

            }
            catch (Exception)
            {
                //ErrorMessage = "";
            }

        }

    }
}
