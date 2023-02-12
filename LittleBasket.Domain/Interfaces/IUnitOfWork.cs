using LittleBasket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBasket.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        public IRepository<Product> ProductRepository { get; }
        public IRepository<History> HistoryRepository { get; }
        public IRepository<Check> CheckRepository { get; }


        public Task Save();
        public void Dispose();
    }
}
