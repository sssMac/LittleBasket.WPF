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
        //Используется хранилище продуктов и историй
        private readonly BasketProductStore _basketProductStore;
        private readonly BasketHistoryStore _basketHistoryStore;

		//Коллкция продуктов в текущей покупки
		private List<Check>? _cheks;
        public IEnumerable<Check>? Cheks => _cheks;
        public DateTime CheckDate { get; set; }


        //Проверка доступности функции сохранить и сбросить, редактировать
        public bool IsActive { get; set; }

        //Проверка на режим редактирования
        //активируется при просмотре чека из истории
        public bool IsEditMode { get; set; }
        public Guid IDofEditedHistory { get; set; }


        //Список доступных событий ->
        //удалить продукт, сохранить в историю, сброс,
        //обновить поля, изменить статус текущей покупки
        public event Action<Check> BasketCheckItemDeleted;
        public event Action<List<Check>> BasketCheckSave;
        public event Action BasketCheckReset;
        public event Action<Check> CheckUpdated;
        public event Action<bool> IsActiveChanged;
		public event Action SetItemFromHistory;
        public event Action<Guid, List<Check>> HistoryItemUpdated;
        public BasketBuyStore(BasketProductStore basketProductStore)
        {
            _basketProductStore = basketProductStore;

			_cheks = new List<Check>();

            _basketProductStore.ProductAddedToBasket += OnProductAddedToBasket;
        }

		public void EditModeChanged(Guid id)
		{
			IsEditMode = true;
            IDofEditedHistory = id;
		}

		//Ивент-подписка: добавляет продукт из списка продуктов в корзину
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
        
        //Блок методов который тригирит свой ивент
        //Вызываются из определенных команд
        //Надеюсь из названия все понятно
		public void DeleteCheckItem(string productName)
        {
            var check = _cheks.FirstOrDefault(c => c.ProductName == productName);

            _cheks.Remove(check);

            BasketCheckItemDeleted?.Invoke(check);

            if(_cheks.Count == 0)
            {
                IsActive = false;
                IsActiveChanged?.Invoke(IsActive);
				_basketHistoryStore.ChangeEditMode(false);

			}
		}

        public void ResetCheck()
        {
            _cheks = null;
            IsActive = false;
            IsActiveChanged?.Invoke(IsActive);
            BasketCheckReset?.Invoke();
            IsEditMode = false;

		}

		public async Task SaveCheck()
        {
            if(!_cheks.Equals(null) && IsEditMode)
            {
                HistoryItemUpdated?.Invoke(IDofEditedHistory, _cheks);
            }
            else if (!_cheks.Equals(null))
            {
                BasketCheckSave?.Invoke(_cheks);
            }
            ResetCheck();
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

        public void SetItemHistory(List<Check> checks, long date)
        {
            CheckDate = DateTimeOffset.FromUnixTimeMilliseconds(date).LocalDateTime;
            _cheks = checks;
			SetItemFromHistory?.Invoke();
            NewBuy();

		}
        public void NewBuy()
        {
            IsActive = true;
            IsActiveChanged?.Invoke(IsActive);
        }

    }
}

