using LittleBasket.Domain.Base;
using LittleBasket.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBasket.UI.ViewModels
{
    public class BasketHistoryItemViewModel : ViewModelBase
    {
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
