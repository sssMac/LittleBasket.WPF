using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBasket.Domain.Models
{
	/// <summary>
	/// Модель продукта
	/// </summary>
	public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsVisible { get; set; }
        public Product(Guid id, string name, bool isVisible)
        {
            Id = id;
            Name = name;
            IsVisible = isVisible;
        }
    }
}
