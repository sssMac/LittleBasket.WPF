using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBasket.Domain.Models
{
    public class History
    {
        public Guid Id { get; set; }
        public long Date { get; set; }
        public List<Check> Checks { get; set; }
        public decimal Sum { get; set; }
    }
}
