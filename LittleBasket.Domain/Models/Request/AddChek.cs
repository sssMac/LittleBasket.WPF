using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBasket.Domain.Models.Request
{
    public class AddChek
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Count { get; set; }
        public decimal Cost { get; set; }
        public decimal Sum { get; set; }
    }
}
