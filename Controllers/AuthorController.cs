using LibrarySystem.Data;
using LibrarySystem.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibrarySystem.Controllers
{

    public class AuthorController : Controller
    {

        private readonly ApplicationDbContext _db;
        public AuthorController(ApplicationDbContext db)
        {
            _db = db;
        }



        public IActionResult Index()
        {
            IEnumerable<Author> objList = _db.Authors;
            return View(objList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Author obj = new();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Author Author)
        {
            if (ModelState.IsValid)
            {
                _db.Authors.Add(Author);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Author);
        }


        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id is <= 0 or null)
            {
                return NotFound();
            }

            Author obj = _db.Authors.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Update(Author Author)
        {

            if (ModelState.IsValid)
            {
                if (Author == null)
                {
                    return NotFound();
                }
                _db.Authors.Update(Author);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Author);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is <= 0 or null)
            {
                return NotFound();
            }

            Author obj = _db.Authors.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(Author auth)
        {

            Author author = _db.Authors.Find(auth.AuthorId);

            _db.Authors.Remove(author);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
