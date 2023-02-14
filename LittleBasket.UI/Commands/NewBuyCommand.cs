using LittleBasket.Domain.Base;
using LittleBasket.Domain.Models;
using LittleBasket.Service.Stores;
using LittleBasket.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBasket.UI.Commands
{
	//Команда активации новой покупки
	//тригерит ивент IsActiveChanged из BasketBuyStore
	public class NewBuyCommand : CommandBase
    {
        private readonly BasketBuyStore _basketBuyStore;

        public NewBuyCommand(BasketBuyStore basketBuyStore)
        {
            _basketBuyStore = basketBuyStore;
        }

        public override async void Execute(object parameter)
        {
            try
            {
                 await _basketBuyStore.NewBuy();

            }
            catch (Exception)
            {
                //ErrorMessage = "";
            }

        }
    }
}
