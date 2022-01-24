using GameStore.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace GameStore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<GameModel> Games { get; set; }
        public DbSet<CartItemModel> CartItems { get; set; }
    }
}
