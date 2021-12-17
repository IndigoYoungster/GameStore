using GameStore.Data;
using GameStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Controllers
{
    public class OrderController : Controller
    {
        public OrderModel Order;
        public CartModel Cart;
        public ApplicationDbContext _db;

        public OrderController(CartModel cart, ApplicationDbContext db)
        {
            _db = db;
            Cart = cart;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Send(OrderModel order)
        {
            if (ModelState.IsValid)
            {
                Order = order;

                List<CartItemModel> cartItems = Cart.GetCartItems();
                int orderSum = 0;
                foreach (var item in cartItems)
                {
                    orderSum += item.Game.Price * item.Count;
                }
                Order.TotalCost = orderSum;

                return View(Order);
            }
            return NotFound();
        }

    }
}
