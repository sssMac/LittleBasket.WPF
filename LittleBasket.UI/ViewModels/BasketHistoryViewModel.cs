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

		private BasketHistoryItemViewModel _selectedBasketHistoryItemViewModel;
		public BasketHistoryItemViewModel SelectedBasketHistoryItemViewModel
		{
			get
			{
				return _selectedBasketHistoryItemViewModel;
			}
			set
			{
				_selectedBasketHistoryItemViewModel = value;
				OnPropertyChanged(nameof(SelectedBasketHistoryItemViewModel));
                if (!_isActiveBuy)
                {
					SetHistoryItemCommand.Execute(_selectedBasketHistoryItemViewModel.History);
					SetEditModeCommand.Execute(SelectedBasketHistoryItemViewModel.HistoryId);
				}
			}
		}

		//Команда активации новой покупки
		public ICommand NewBuyCommand { get; }
        public ICommand SetHistoryItemCommand { get; }
		public ICommand SetEditModeCommand { get; }

		//Проверка на доступность к добавлению товаров в корзину
		private bool _isActiveBuy;
		public bool IsActiveBuy
		{
			get { return _isActiveBuy; }
			set
			{
				_isActiveBuy = value;
			}
		}
		public bool InverseActiveBuy => !_isActiveBuy;
		public BasketHistoryViewModel(BasketBuyStore basketBuyStore, BasketHistoryStore basketHistoryStore)
        {
            _basketBuyStore = basketBuyStore;
            _basketHistoryStore = basketHistoryStore;

            _basketHistoryItemViewModels = new ObservableCollection<BasketHistoryItemViewModel>();
            NewBuyCommand = new NewBuyCommand(_basketBuyStore);
            SetHistoryItemCommand = new SetHistoryToBuyCommand(_basketBuyStore, _basketHistoryStore);
			SetEditModeCommand = new SetEditModeCommand(_basketBuyStore) ;
			//Подписки
			_basketHistoryStore.BasketHistoryLoaded += OnHistoryLoaded;
			_basketHistoryStore.HistoryUpdated += OnHistoryUpdated;
			_basketBuyStore.IsActiveChanged += OnActiveChanged;

		}

		//Ивент-подписка: обновление истории при изменении или сохранении
		private void OnHistoryUpdated(History history)
		{
			var isExist = _basketHistoryItemViewModels.FirstOrDefault(x => x.HistoryId == history.Id);
			if (isExist != null)
			{
				_basketHistoryItemViewModels.Remove(isExist);
			}
			_basketHistoryItemViewModels.Insert(0, new BasketHistoryItemViewModel(history));
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

		//Ивента: изменение поля isActive, вкл/выкл функции "добавить в корзину"
		private void OnActiveChanged(bool obj)
		{
			_isActiveBuy = obj;

			OnPropertyChanged(nameof(IsActiveBuy));
			OnPropertyChanged(nameof(InverseActiveBuy));
		}
		//Отписка от событий
		protected override void Dispose()
        {
            _basketHistoryStore.BasketHistoryLoaded -= OnHistoryLoaded;
			_basketHistoryStore.HistoryUpdated -= OnHistoryUpdated;
			_basketBuyStore.IsActiveChanged -= OnActiveChanged;

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
