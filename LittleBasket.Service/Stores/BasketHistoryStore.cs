using AutoMapper;
using LittleBasket.Domain.Interfaces.Services;
using LittleBasket.Domain.Models;
using LittleBasket.Domain.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBasket.Service.Stores
{
    //Хранилище для общего доступа к истории покупок
    public class BasketHistoryStore
    {
        //Использует хранилище текущей покупки
        private readonly BasketBuyStore _basketBuyStore;

        //Сервис для взаимодействия с бд, и маппер  
        private readonly IBasketService _basketService; 
        private readonly IMapper _mapper;

        //Коллекция истории покупок
        private List<History>? _history;
        public IEnumerable<History>? History => _history;

        //Ивент, вызов которого загрузит историю из бд
        public event Action BasketHistoryLoaded;
		public event Action<bool, Guid> IsEditModeChanged;

		//Подписка него обновит историю
		public event Action<History> HistoryUpdated;

        public BasketHistoryStore(
            BasketBuyStore basketBuyStore, 
            IMapper mapper, IBasketService 
            basketService)
        {
            _basketBuyStore = basketBuyStore;
            _mapper = mapper;
            _basketService = basketService;

            _history = new List<History>();

            _basketBuyStore.BasketCheckSave += OnBasketCheckSave;
			_basketBuyStore.HistoryItemUpdated += OnHistoryItemUpdated;
        }

        //Ивент-подписка: обновляет изменный элемент списка
		private async void OnHistoryItemUpdated(Guid historyId, List<Check> cheks)
		{
            var entCheks = new List<Domain.Entities.Check>();
            cheks.ForEach(c =>
            {
                entCheks.Add(new Domain.Entities.Check
                {
                    HistoryId = historyId,
                    ProductName = c.ProductName,
                    Cost= c.Cost,
                    ProductId= c.ProductId,
                    Count = c.Count
                });
            });
            await _basketService.EditHistory(historyId, entCheks);

            var history = _history.Find(x => x.Id == historyId);
            history.Checks = cheks;
            history.Sum = Sum(cheks);
            history.Date = DateTimeOffset.Now.ToUnixTimeMilliseconds();
			HistoryUpdated?.Invoke(history);
		}


		//Тригирит ивент загрузки истории из бд
		//Вызывается из команды
		public async Task Load()
        {
            IEnumerable<History> history =
                _mapper.Map<IEnumerable<History>>(await _basketService.GetHistory());

            _history.Clear();
            _history.AddRange(history);

            BasketHistoryLoaded?.Invoke();
        }

        //Ивент-подписка: сохранить текущую покупку в историю
        public async void OnBasketCheckSave(List<Check> cheks)
        {
            var sum = Sum(cheks);
            var id = Guid.NewGuid();
            var addHistoryRequest = new List<AddChek>();
            cheks.ForEach(c =>
            {
                addHistoryRequest.Add(new AddChek
                {
                    ProductId = c.ProductId,
                    ProductName = c.ProductName,
                    Cost= c.Cost,
                    Count = c.Count,
                    Sum = sum
                });;
            });
            await _basketService.AddHistory(id, addHistoryRequest);

            var history = new History
            {
                Id = id,
                Date = DateTimeOffset.Now.ToUnixTimeMilliseconds(),
                Checks = cheks,
                Sum = sum
            };

			_history.Add(history);
            HistoryUpdated?.Invoke(history);

		}

        public void ChangeEditMode(bool boolean, Guid id = default)
        {
            IsEditModeChanged?.Invoke(boolean, id);

		}
        private decimal Sum(List<Check> cheks)
        {
            var res = 0M;
            cheks.ForEach(che =>
            {
                res += che.Count * che.Cost;
            });
            return res;
        }
    }
}
