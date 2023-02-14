using AutoMapper;
using AutoMapper.Internal;
using LittleBasket.Domain.Interfaces.Commands;
using LittleBasket.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBasket.Service.Stores
{
    //Хранилище для общего дуступа к текущей покупки
    public class BasketBuyStore
    {
        //Используется хранилище продуктов
        private readonly BasketProductStore _basketProductStore;

        //Коллкция продуктов в текущей покупки
        private List<Check>? _cheks;
        public IEnumerable<Check>? Cheks => _cheks;

        //Проверка доступности функции сохранить и сбросить
        public bool IsActive { get; set; }

        //Список доступных событий ->
        //удалить продукт, сохранить в историю, сброс,
        //обновить поля, изменить статус текущей покупки
        public event Action<Check> BasketCheckItemDeleted;
        public event Action<List<Check>> BasketCheckSave;
        public event Action BasketCheckReset;
        public event Action<Check> CheckUpdated;
        public event Action<bool> IsActiveChanged;
        public BasketBuyStore(BasketProductStore basketProductStore)
        {
            _basketProductStore = basketProductStore;

            _cheks = new List<Check>();

            _basketProductStore.ProductAddedToBasket += OnProductAddedToBasket;
        }

        //
        public void OnProductAddedToBasket(Product product)
        {
            if (product != null)
            {
                if (_cheks.Any(c => c.ProductName == product.Name))
                {
                    _cheks.FirstOrDefault(c => c.ProductName == product.Name).Count++;

                    return;
                }

                var check = new Check(product.Id, product.Name, 1, 0);
                _cheks.Add(check);
            }
        }

        public async Task DeleteCheckItem(string productName)
        {
            var check = _cheks.FirstOrDefault(c => c.ProductName == productName);

            _cheks.Remove(check);

            BasketCheckItemDeleted?.Invoke(check);

            if(_cheks.Count == 0)
            {
                IsActive = false;
                IsActiveChanged?.Invoke(IsActive);
            }
        }

        public async Task ResetCheck()
        {
            _cheks.Clear();
            IsActive = false;
            IsActiveChanged?.Invoke(IsActive);
            BasketCheckReset?.Invoke();
        }

        public async Task SaveCheck()
        {

            if (!_cheks.Equals(null))
            {
                BasketCheckSave?.Invoke(_cheks);
            }
        }

        public async Task UpdateCheck(List<Check> checks)
        {
            checks.ForEach(c => 
            {

                int currentIndex = _cheks.FindIndex(y => y.ProductId == c.ProductId);

                if (currentIndex != -1)
                {
                    _cheks[currentIndex] = c;
                }
                else
                {
                    _cheks.Add(c);
                }

                CheckUpdated?.Invoke(c);

            });
        }

        public async Task NewBuy()
        {
            IsActive = true;
            IsActiveChanged?.Invoke(IsActive);
        }

    }
}

