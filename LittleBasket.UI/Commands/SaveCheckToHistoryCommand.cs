using LittleBasket.Domain.Base;
using LittleBasket.Domain.Models;
using LittleBasket.Service.Stores;
using LittleBasket.Service.Stores;
using LittleBasket.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBasket.UI.Commands
{
    public class SaveCheckToHistoryCommand : AsyncCommandBase
    {
        private readonly BasketBuyStore _basketBuyStore;
        private readonly ObservableCollection<BasketBuyItemViewModel> _basketBuyItemViewModel;

        public SaveCheckToHistoryCommand(BasketBuyStore basketBuyStore, ObservableCollection<BasketBuyItemViewModel> basketBuyItemViewModel)
        {
            _basketBuyStore = basketBuyStore;
            _basketBuyItemViewModel = basketBuyItemViewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if(_basketBuyItemViewModel.Count == 0) { return; }

            var cheks = new List<Check>();

            _basketBuyItemViewModel.ToList().ForEach(c =>
            {
                cheks.Add(new Check
                {
                    ProductId = c.ProductId,
                    ProductName = c.ProductName,
                    Cost = c.Cost,
                    Count = c.Count,
                });
            });
            try
            {
                await _basketBuyStore.UpdateCheck(cheks);
                await _basketBuyStore.SaveCheck();

            }
            catch (Exception)
            {
                //ErrorMessage = "";
            }

        }
    }
}
