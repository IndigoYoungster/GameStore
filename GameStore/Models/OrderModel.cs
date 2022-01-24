using System;

namespace GameStore.Models
{
    public class OrderModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int TotalCost { get; set; } = 0;
    }
}
