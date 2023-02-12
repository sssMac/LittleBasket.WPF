﻿using LittleBasket.Domain.Base;
using LittleBasket.Service.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBasket.UI.Commands
{
    public class DeleteFromBasketCommand : CommandBase
    {
        private readonly BasketBuyStore _basketBuyStore;
        public DeleteFromBasketCommand(BasketBuyStore basketBuyStore)
        {
            _basketBuyStore = basketBuyStore;
        }

        public override async void Execute(object parameter)
        {
            try
            {
                if(parameter is string)
                {
                    await _basketBuyStore.DeleteCheckItem(parameter.ToString());

                }
                else if(parameter == null){
                    await _basketBuyStore.ResetCheck();
                }

            }
            catch (Exception)
            {
                //ErrorMessage = "";
            }
        }
    }
}
