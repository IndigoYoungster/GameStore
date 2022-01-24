using System;

namespace GameStore.Models
{
    public class CartItemModel
    {
        public int Id { get; set; }
        public GameModel Game { get; set; }
        public int Count { get; set; }

        public string CartId { get; set; }
    }
}
