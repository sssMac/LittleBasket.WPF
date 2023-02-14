using LittleBasket.Domain.Interfaces.Commands;
using LittleBasket.Domain.Models;
using LittleBasket.Service.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBasket.UI.Commands
{
    //Команда обновления полей продукта
    public class UpdateProductCommand : IUpdateCommand<Product>
    {
        private readonly BasketProductStore _basketProductStore;

        public UpdateProductCommand(BasketProductStore basketProductStore)
        {
            _basketProductStore = basketProductStore;
        }

        public async Task Execute(Product obj)
        {
            await _basketProductStore.Update(obj);
        }
    }
}
