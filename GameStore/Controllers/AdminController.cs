using GameStore.Data;
using GameStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace GameStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AdminController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<GameModel> objGamesList = _db.Games;
            return View(objGamesList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GameModel obj)
        {
            if (ModelState.IsValid)
            {
                _db.Games.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var gameFromDb = _db.Games.Find(id);

            if (gameFromDb == null)
            {
                return NotFound();
            }

            return View(gameFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(GameModel obj)
        {
            if (ModelState.IsValid)
            {
                _db.Games.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var gameFromDb = _db.Games.Find(id);

            if (gameFromDb == null)
            {
                return NotFound();
            }

            return View(gameFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var deleteGame = _db.Games.Find(id);

            if (ModelState.IsValid)
            {
                _db.Games.Remove(deleteGame);
                _db.SaveChanges();
                TempData["success"] = "Category delete successfully!";
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
