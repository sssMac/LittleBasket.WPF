using LittleBasket.Domain.Base;
using LittleBasket.Domain.Interfaces.Commands;
using LittleBasket.Domain.Models;
using LittleBasket.Service.Stores;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LittleBasket.UI.Commands
{
    public class UpdateCheckCommand : IUpdateCommand<List<Check>>
    {
        private readonly BasketBuyStore _basketBuyStore;

        public UpdateCheckCommand(BasketBuyStore basketBuyStore)
        {
            _basketBuyStore = basketBuyStore;
        }

        public async Task Execute(List<Check> checks)
        {
            _basketBuyStore.UpdateCheck(checks);
        }
    }
}
