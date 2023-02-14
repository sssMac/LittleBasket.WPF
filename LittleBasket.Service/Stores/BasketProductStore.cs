using AutoMapper;
using LittleBasket.Domain.Interfaces.Commands;
using LittleBasket.Domain.Interfaces.Services;
using LittleBasket.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBasket.Service.Stores
{
    //Общее хранилище продуктов
    public class BasketProductStore
    {
        //Сервис для управления бд и маппер
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;

        //Коллекция продуктов
        private readonly List<Product> _products;
        public IEnumerable<Product> Products => _products;

        //Блок ивентов ->
        //загрузку продуктов из бд, добавления продукта в корзину,
        //обновление полей продукта, добавление нового продукта
        public event Action BasketProductsLoaded;
        public event Action<Product> ProductAddedToBasket;
        public event Action<Product> BasketProductUpdated;
        public event Action<Product> ProductAdded;

        public BasketProductStore(
            IBasketService basketService, 
            IMapper mapper)
        {
            _basketService = basketService;
            _mapper = mapper;
            _products = new List<Product>();
        }

        //Блок методов которые тригирят ивенты
        //Вызываются из команд
        public async Task Load()
        {
            IEnumerable<Product> products =
                _mapper.Map<IEnumerable<Product>>(await _basketService.GetProducts());

            _products.Clear();
            _products.AddRange(products);

            BasketProductsLoaded?.Invoke();
        }
        public async Task AddToBasket(Product product)
        {

            ProductAddedToBasket?.Invoke(product);
        }
        public async Task Update(Product product)
        {
            await _basketService.ChangeVisibility(product.Id, product.IsVisible);
            int currentIndex = _products.FindIndex(y => y.Id == product.Id);

            if (currentIndex != -1)
            {
                _products[currentIndex] = product;
            }
            else
            {
                _products.Add(product);
            }

            BasketProductUpdated?.Invoke(product);
        }
        public async Task AddNewProduct(string productName)
        {
            if (!_products.Any(p => p.Name.ToUpper() == productName.ToUpper()))
            {
                var id = Guid.NewGuid();
                var product = new Product(id, productName, false);
                _products.Add(product);
                _basketService.AddProduct(id,productName);

                ProductAdded.Invoke(product);
            }
        }

    }
}
