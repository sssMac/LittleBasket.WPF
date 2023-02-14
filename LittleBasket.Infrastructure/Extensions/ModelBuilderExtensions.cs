using LittleBasket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBasket.Infrastructure.Extensions
{
    public static class ModelBuilderExtension
    {
		/// <summary>
		/// Пресоздает данные в бд
		/// </summary>
		public static void Seed(this ModelBuilder builder)
        {
            var products = new List<Product>
            {
                new Product {Id = Guid.NewGuid(), Name = "Молоко", IsVisible = true},
                new Product {Id = Guid.NewGuid(), Name = "Хлеб", IsVisible = true},
                new Product {Id = Guid.NewGuid(), Name = "Мясо", IsVisible = true},
                new Product {Id = Guid.NewGuid(), Name = "Масло", IsVisible = true},
                new Product {Id = Guid.NewGuid(), Name = "Арбуз", IsVisible = true},
                new Product {Id = Guid.NewGuid(), Name = "Колбаса", IsVisible = false},
                new Product {Id = Guid.NewGuid(), Name = "Сыр", IsVisible = false},
                new Product {Id = Guid.NewGuid(), Name = "Печенье", IsVisible = false},
            };
            builder.Entity<Product>().HasData(products);
        }
    }
}

