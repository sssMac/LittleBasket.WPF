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
        }


        //М
        public async Task Load()
        {
            IEnumerable<History> history =
                _mapper.Map<IEnumerable<History>>(await _basketService.GetHistory());

            _history.Clear();
            _history.AddRange(history);

            BasketHistoryLoaded?.Invoke();
        }

        public async void OnBasketCheckSave(List<Check> cheks)
        {
            var sum = Sum(cheks);

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
            await _basketService.AddHistory(addHistoryRequest);

            _history.Add(new History
            {
                Date = DateTimeOffset.Now.ToUnixTimeMilliseconds(),
                Checks = cheks,
                Sum = sum
            });

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
