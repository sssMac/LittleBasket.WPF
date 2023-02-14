using LittleBasket.Domain.Base;
using LittleBasket.Domain.Models;
using LittleBasket.Service.Stores;
using LittleBasket.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBasket.UI.Commands
{
	//Команда которая берет товары из истории и показывает их в корзине
	//Также переключается режим на редактирование
	public class SetHistoryToBuyCommand : CommandBase
	{
		private readonly BasketBuyStore _basketBuyStore;
		private readonly BasketHistoryStore _basketHistoryStore;

		public SetHistoryToBuyCommand(BasketBuyStore basketBuyStore, BasketHistoryStore basketHistoryStore)
		{
			_basketBuyStore = basketBuyStore;
			_basketHistoryStore = basketHistoryStore;
		}

		public override void Execute(object obj)
		{
			if(obj is History)
			{
				var history = (History)obj;
				_basketBuyStore.SetItemHistory(history.Checks, history.Date);
				_basketHistoryStore.ChangeEditMode(true, history.Id);

			}
		}
	}
}
