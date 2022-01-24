using GameStore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Models
{
    public class CartModel
    {
        public ApplicationDbContext _db;

        public CartModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public string CartId { get; set; }
        public List<CartItemModel> ListCartItems { get; set; }

        public static CartModel GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<ApplicationDbContext>();
            string sessionCartId = session.GetString("SessionCartId") ?? Guid.NewGuid().ToString();

            session.SetString("SessionCartId", sessionCartId);

            return new CartModel(context) { CartId = sessionCartId };
        }

        public void AddToCart(GameModel game)
        {

            if (!_db.CartItems.Any(g => g.CartId == CartId && g.Game.Id == game.Id))
            {
                _db.CartItems.Add(new CartItemModel
                {
                    CartId = CartId,
                    Game = game,
                    Count = 1
                });
            }
            else
            {
                CartItemModel item = _db.CartItems.First(g => g.CartId == CartId && g.Game.Id == game.Id);
                item.Count++;
                _db.CartItems.Update(item);
            }
            _db.SaveChanges();
        }

        public List<CartItemModel> GetCartItems()
        {
            return _db.CartItems.Where(c => c.CartId == CartId).Include(s => s.Game).ToList();
        }
    }
}
