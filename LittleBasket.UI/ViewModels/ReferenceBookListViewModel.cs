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
	//Класс отвечающий за общий вид листа прдуктов в справочнике
	//DataContext для компонента ReferenceBookList
	public class ReferenceBookListViewModel : ViewModelBase
    {
        //Хранилище продуктов
        private readonly BasketProductStore _basketProductStore;

        //Коллекция со всеми продуктами
        private readonly ObservableCollection<ReferenceBookListItemViewModel> _referenceBookListItemViewModels;
        public IEnumerable<ReferenceBookListItemViewModel> ReferenceBookListItemViewModels => _referenceBookListItemViewModels;


        //Комманда обновления полей продукта
        //Тут не используется, передается каждому элементу листа
        public IUpdateCommand<Product> UpdateProductCommand { get; }


        public ReferenceBookListViewModel(BasketProductStore basketProductStore, IUpdateCommand<Product> updateProductCommand)
        {
            _basketProductStore = basketProductStore;
            UpdateProductCommand = updateProductCommand;
            _referenceBookListItemViewModels = new ObservableCollection<ReferenceBookListItemViewModel>();

            _basketProductStore.ProductAdded += OnProductAdded;
            OnProductsLoaded();
        }

        //Ивент-подписка: добавление нового продукта
        private void OnProductAdded(Product product)
        {
            _referenceBookListItemViewModels.Insert(0, new ReferenceBookListItemViewModel(product, UpdateProductCommand));
            _referenceBookListItemViewModels.OrderBy(x => x);
            
        }
        
        //Ивент-подписка: выгрузка продуктов из бд
        private void OnProductsLoaded()
        {
            _referenceBookListItemViewModels.Clear();

            foreach (Product product in _basketProductStore.Products)
            {
                _referenceBookListItemViewModels.Add(new ReferenceBookListItemViewModel(product, UpdateProductCommand));
            }
        }

        //Отписка от событий
        protected override void Dispose()
        {
            _basketProductStore.ProductAdded -= OnProductAdded;

            base.Dispose();
        }
    }
}
