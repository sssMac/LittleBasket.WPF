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
	//Команда выгрузки истории из бд
	//тригерит ивент BasketHistoryLoaded из BasketHistoryStore
	public class LoadHistoryCommand : AsyncCommandBase
    {
        private readonly BasketHistoryStore _basketHistoryStore;

        public LoadHistoryCommand(BasketHistoryStore basketHistoryStore)
        {
            _basketHistoryStore = basketHistoryStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _basketHistoryStore.Load();

            }
            catch (Exception)
            {
                //ErrorMessage = "";
            }

        }
    }
}
