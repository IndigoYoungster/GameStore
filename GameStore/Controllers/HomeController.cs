using GameStore.Data;
using GameStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Controllers
{
    public class HomeController : Controller
    {
        public ApplicationDbContext _db;
        public CartModel Cart;

        public HomeController(ApplicationDbContext db, CartModel cart)
        {
            _db = db;
            Cart = cart;
            Cart.ListCartItems = Cart.GetCartItems();
        }

        public IActionResult Index()
        {
            IEnumerable<GameModel> gamesList = _db.Games;
            return View(gamesList);
        }

        public IActionResult AddToCart(int? id)
        {
            var AddingGame = _db.Games.Find(id);

            if (ModelState.IsValid)
            {
                Cart.AddToCart(AddingGame);
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
