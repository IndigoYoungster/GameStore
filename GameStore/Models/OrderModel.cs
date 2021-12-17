using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models
{
    public class OrderModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int TotalCost { get; set; } = 0;
    }
}
