using LibrarySystem.Data;
using LibrarySystem.Models;
using LibrarySystem.Models.DataVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibrarySystem.Controllers
{

    public class BookController : Controller
    {

        private readonly ApplicationDbContext _db;
        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }



        public IActionResult Index()
        {
            DateTime dt = DateTime.Now.Date;
            DateTime DueDate = dt.AddDays(7);

            IEnumerable<Book> objList = _db.Books;
            return View(objList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> TypeDropDown = _db.Authors.Select(i => new SelectListItem
            {
                Text = i.AuthorName,
                Value = i.AuthorId.ToString()
            });
            ViewBag.TypeDropDown = TypeDropDown;
            Book obj = new();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book BookVM)
        {        
            if (ModelState.IsValid)
            {
                BookVM.isAvailable = Convert.ToBoolean(BookVM.isAvailable);
                _db.Books.Add(BookVM);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            IEnumerable<SelectListItem> TypeDropDown = _db.Authors.Select(i => new SelectListItem
            {
                Text = i.AuthorName,
                Value = i.AuthorId.ToString()
            });
            ViewBag.TypeDropDown = TypeDropDown;
            return View(BookVM);
        }


        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id is <= 0 or null)
            {
                return NotFound();
            }

            Book obj = _db.Books.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            IEnumerable<SelectListItem> TypeDropDown = _db.Authors.Select(i => new SelectListItem
            {
                Text = i.AuthorName,
                Value = i.AuthorId.ToString()
            });
            ViewBag.TypeDropDown = TypeDropDown;
            return View(obj);
        }
        
        public IActionResult Show(int? id)
        {
            if (id is <= 0 or null)
            {
                return NotFound();
            }

            Book obj = _db.Books.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            IEnumerable<SelectListItem> TypeDropDown = _db.Authors.Select(i => new SelectListItem
            {
                Text = i.AuthorName,
                Value = i.AuthorId.ToString()
            });
            ViewBag.TypeDropDown = TypeDropDown;
            return View(obj);
        }

        [HttpPost]
        public IActionResult Update(Book BookVM)
        {

            if (ModelState.IsValid)
            {
                if (BookVM == null)
                {
                    return NotFound();
                }
                _db.Books.Update(BookVM);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            IEnumerable<SelectListItem> TypeDropDown = _db.Authors.Select(i => new SelectListItem
            {
                Text = i.AuthorName,
                Value = i.AuthorId.ToString()
            });
            ViewBag.TypeDropDown = TypeDropDown;
            return View(BookVM);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is <= 0 or null)
            {
                return NotFound();
            }

            Book obj = _db.Books.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            IEnumerable<SelectListItem> TypeDropDown = _db.Authors.Select(i => new SelectListItem
            {
                Text = i.AuthorName,
                Value = i.AuthorId.ToString()
            });
            ViewBag.TypeDropDown = TypeDropDown;
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(Book Book)
        {
            int id = Book.BookId;
            Book book = _db.Books.Find(id);

            _db.Books.Remove(book);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
