using GameStore.Models;
using GameStore.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace GameStore.Controllers
{
    public class OrderController : Controller
    {
        private OrderModel Order;
        private CartModel Cart;
        private EmailSender emailSender;

        public OrderController(CartModel cart)
        {
            Cart = cart;
            emailSender = new EmailSender();
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

                emailSender.SendEmailAsync(Order, cartItems);

                return View(Order);
            }
            return NotFound();
        }

    }
}
