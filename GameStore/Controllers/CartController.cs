using GameStore.Data;
using GameStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace GameStore.Controllers
{
    public class CartController : Controller
    {
        private CartModel Cart;
        private readonly ApplicationDbContext _db;

        public CartController(ApplicationDbContext db, CartModel cart)
        {
            _db = db;
            Cart = cart;
        }

        public IActionResult Index()
        {
            Cart.ListCartItems = Cart.GetCartItems();

            return View(Cart.ListCartItems);
        }

        public IActionResult AddButton(int? id)
        {
            CartItemModel item = _db.CartItems.First(g => g.CartId == Cart.CartId && g.Game.Id == id);
            item.Count++;
            _db.CartItems.Update(item);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult RemoveButton(int? id)
        {
            CartItemModel item = _db.CartItems.First(g => g.CartId == Cart.CartId && g.Game.Id == id);
            item.Count--;
            _db.CartItems.Update(item);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
