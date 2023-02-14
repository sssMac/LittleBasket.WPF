using LittleBasket.Domain.Base;
using LittleBasket.Domain.Models;
using LittleBasket.Domain.Models.Request;
using LittleBasket.Service.Stores;
using LittleBasket.UI.Commands;
using LittleBasket.Service.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace LittleBasket.UI.ViewModels
{
	//Класс отвечающий за общий вид блока историй
	//DataContext для компонента BasketHistory
	public class BasketHistoryViewModel : ViewModelBase
    {
        //Хранилища текущей покупки и историй
        private readonly BasketBuyStore _basketBuyStore;
        private readonly BasketHistoryStore _basketHistoryStore;

        //Колекция историй
        private readonly ObservableCollection<BasketHistoryItemViewModel> _basketHistoryItemViewModels;
        public IEnumerable<BasketHistoryItemViewModel> BasketHistoryItemViewModels => _basketHistoryItemViewModels;

        //Команда активации новой покупки
        public ICommand NewBuyCommand { get; }
        public BasketHistoryViewModel(BasketBuyStore basketBuyStore, BasketHistoryStore basketHistoryStore)
        {
            _basketBuyStore = basketBuyStore;
            _basketHistoryStore = basketHistoryStore;

            _basketHistoryItemViewModels = new ObservableCollection<BasketHistoryItemViewModel>();
            NewBuyCommand = new NewBuyCommand(basketBuyStore);

            //Подписки
            _basketHistoryStore.BasketHistoryLoaded += OnHistoryLoaded;
            _basketBuyStore.BasketCheckSave += OnAddedToHistory;

        }

		//Ивент-подписка: добавление в историю новой сохраненой покупки
		private void OnAddedToHistory(List<Check> cheks)
        {

            _basketHistoryItemViewModels.Insert(0, new BasketHistoryItemViewModel(new History
            {
                Date = DateTimeOffset.Now.ToUnixTimeMilliseconds(),
                Checks = cheks,
                Sum = Sum(cheks)
            }));
        }

        //Ивент-подписка: загрузка истории из бд
        private void OnHistoryLoaded()
        {
            _basketHistoryItemViewModels.Clear();

            foreach (History history in _basketHistoryStore?.History.OrderBy(x => x.Date).Reverse())
            {

                _basketHistoryItemViewModels.Add(new BasketHistoryItemViewModel(history));
            }
 
        }

        //Отписка от событий
        protected override void Dispose()
        {
            _basketHistoryStore.BasketHistoryLoaded -= OnHistoryLoaded;
            _basketBuyStore.BasketCheckSave -= OnAddedToHistory;

            base.Dispose();
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
