using LittleBasket.Domain.Base;
using LittleBasket.Service.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBasket.UI.Commands
{
	//Команда удаления продукта из корзины покупки
	//тригерит ивент BasketCheckItemDeleted из BasketBuyStore
	public class DeleteFromBasketCommand : CommandBase
    {
        private readonly BasketBuyStore _basketBuyStore;
        public DeleteFromBasketCommand(BasketBuyStore basketBuyStore)
        {
            _basketBuyStore = basketBuyStore;
        }

        public override void Execute(object parameter)
        {
            try
            {
                if(parameter is string)
                {
                    _basketBuyStore.DeleteCheckItem(parameter.ToString());

                }
                else if(parameter == null){
                    _basketBuyStore.ResetCheck();
				}

            }
            catch (Exception)
            {
                //ErrorMessage = "";
            }
        }
    }
}
