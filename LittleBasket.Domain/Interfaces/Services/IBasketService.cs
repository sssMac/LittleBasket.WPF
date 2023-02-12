using LittleBasket.Domain.Entities;
using LittleBasket.Domain.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBasket.Domain.Interfaces.Services
{
    public interface IBasketService
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<History>> GetHistory();
        Task AddProduct(Guid id,string name);
        Task AddHistory(List<AddChek> addChecks);
        Task ChangeVisibility(Guid productId, bool isVisible);
    }
}
