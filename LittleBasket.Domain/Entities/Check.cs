using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBasket.Domain.Entities
{
    [PrimaryKey(nameof(HistoryId), nameof(ProductId))]
    public class Check
    {
        public Guid HistoryId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Count { get; set; }
        public decimal Cost { get; set; }

        public History History { get; set; }
    }
}
