using LittleBasket.Domain.Entities;
using LittleBasket.Domain.Interfaces;
using LittleBasket.Domain.Interfaces.Services;
using LittleBasket.Domain.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBasket.Service
{
    //Сервис управления бд через UnitOfWork
    public class BasketService : IBasketService
    {
        private IUnitOfWork _unitOfWork;

        public BasketService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //получить продкты
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _unitOfWork.ProductRepository.Get();
        }

        //получить историю
        public async Task<IEnumerable<History>> GetHistory()
        {
            return await _unitOfWork.HistoryRepository.Get(null,null,"Checks");
        }

        //добавить новый продукт 
        public async Task AddProduct(Guid id,string name)
        {
            var product = new Product
            {
                Id = id,
                Name = name,
                IsVisible = false,
            };

            await _unitOfWork.ProductRepository.Insert(product);
            await _unitOfWork.Save();
        }

        //добавить новую историю
        public async Task AddHistory(Guid id, List<AddChek> addChecks)
        {
            var checks = new List<Check>();
            var history = new History
            {
                Id = id,
                Date = DateTimeOffset.Now.ToUnixTimeMilliseconds()
            };

            addChecks.ForEach(async check =>
            {
                checks.Add(new Check
                {
                    HistoryId = history.Id,
                    ProductId = check.ProductId,
                    ProductName = check.ProductName,
                    Count = check.Count,
                    Cost = check.Cost * check.Count,
                        
                });
            });

            history.Checks = checks;
            history.Checks.ForEach(c =>
            {
                history.Sum += c.Cost * c.Count;
            });

            await _unitOfWork.HistoryRepository.Insert(history);
            await _unitOfWork.Save();
        }

        //изменить видимость продукта
        public async Task ChangeVisibility(Guid productId, bool isVisible)
        {
            var product = (await _unitOfWork.ProductRepository.Get(x => x.Id == productId)).First();
            if (product != null)
            {
                product.IsVisible = isVisible;
            }

            await _unitOfWork.Save();
        }

        public async Task EditHistory(Guid id, List<Check> cheks)
        {
            var history = (await _unitOfWork.HistoryRepository.Get(x => x.Id == id)).FirstOrDefault();
            if (history != null)
            {
                var sum = 0M;
				cheks.ForEach(che =>
				{
					sum += che.Count * che.Cost;
				});

                history.Sum = sum;
				history.Date = DateTimeOffset.Now.ToUnixTimeMilliseconds();
				history.Checks = cheks;
                await _unitOfWork.Save();
            }
        }

    }
}
