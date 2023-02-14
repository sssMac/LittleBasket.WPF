using LittleBasket.Domain.Base;
using LittleBasket.Service.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBasket.UI.Commands
{
	//Команда для перехода в режим редактирования
	public class SetEditModeCommand : CommandBase
	{
		private readonly BasketBuyStore _basketBuyStore;

		public SetEditModeCommand(BasketBuyStore basketBuyStore)
		{
			_basketBuyStore = basketBuyStore;
		}

		public override void Execute(object parameter)
		{
			_basketBuyStore.EditModeChanged((Guid)parameter);
		}
	}
}
