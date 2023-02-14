using LittleBasket.Domain.Base;
using LittleBasket.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBasket.UI.ViewModels
{
	//Класс отвечающий за элемент из списка историй
	//DataContext для компонента BasketHistoryItem
	public class BasketHistoryItemViewModel : ViewModelBase
	{
        //Модель использующаяся на UI
        public History History { get; private set; }

        public Guid HistoryId => History.Id;
        public DateTime Date => DateTimeOffset.FromUnixTimeMilliseconds(History.Date).LocalDateTime;
        public List<Check> Checks => History.Checks;
        public decimal Sum => History.Sum;

        public BasketHistoryItemViewModel(History history)
        {
            History = history;
        }
    }
}
