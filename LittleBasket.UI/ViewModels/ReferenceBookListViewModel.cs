using LittleBasket.Domain.Base;
using LittleBasket.Domain.Interfaces.Commands;
using LittleBasket.Domain.Models;
using LittleBasket.Service.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LittleBasket.UI.ViewModels
{
    public class ReferenceBookListViewModel : ViewModelBase
    {
        private readonly BasketProductStore _basketProductStore;


        private readonly ObservableCollection<ReferenceBookListItemViewModel> _referenceBookListItemViewModels;
        public IEnumerable<ReferenceBookListItemViewModel> ReferenceBookListItemViewModels => _referenceBookListItemViewModels;

        public IUpdateCommand<Product> UpdateProductCommand { get; }


        public ReferenceBookListViewModel(BasketProductStore basketProductStore, IUpdateCommand<Product> updateProductCommand)
        {
            _basketProductStore = basketProductStore;
            UpdateProductCommand = updateProductCommand;
            _referenceBookListItemViewModels = new ObservableCollection<ReferenceBookListItemViewModel>();

            _basketProductStore.ProductAdded += OnProductAdded;
            OnProductsLoaded();
        }


        private void OnProductAdded(Product product)
        {
            _referenceBookListItemViewModels.Insert(0, new ReferenceBookListItemViewModel(product, UpdateProductCommand));
            _referenceBookListItemViewModels.OrderBy(x => x);
            
        }
        protected override void Dispose()
        {
            _basketProductStore.ProductAdded -= OnProductAdded;

            base.Dispose();
        }
        private void OnProductsLoaded()
        {
            _referenceBookListItemViewModels.Clear();

            foreach (Product product in _basketProductStore.Products)
            {
                _referenceBookListItemViewModels.Add(new ReferenceBookListItemViewModel(product, UpdateProductCommand));
            }
        }
    }
}
