using LittleBasket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBasket.Domain.Models
{
    public class Check
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Count { get; set; }
        public decimal Cost { get; set; }
        public Check(Guid productId, string productName, int count, decimal cost)
        {
            ProductId = productId;
            ProductName = productName;
            Count = count;
            Cost = cost;
        }

        public Check() { }

    }
}
