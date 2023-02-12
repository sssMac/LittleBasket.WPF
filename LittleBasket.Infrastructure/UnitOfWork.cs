using LittleBasket.Domain.Entities;
using LittleBasket.Domain.Interfaces;
using LittleBasket.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBasket.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        private IRepository<Product> _productRepository;
        private IRepository<History> _historyRepository;
        private IRepository<Check> _checkRepository;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }
        public IRepository<Product> ProductRepository
        {
            get
            {
                if (this._productRepository == null)
                {
                    this._productRepository = new Repository<Product>(_context);
                }
                return _productRepository;
            }
        }
        public IRepository<History> HistoryRepository
        {
            get
            {
                if (this._historyRepository == null)
                {
                    this._historyRepository = new Repository<History>(_context);
                }
                return _historyRepository;
            }
        }
        public IRepository<Check> CheckRepository
        {
            get
            {
                if (this._checkRepository == null)
                {
                    this._checkRepository = new Repository<Check>(_context);
                }
                return _checkRepository;
            }
        }


        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.DisposeAsync();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}

