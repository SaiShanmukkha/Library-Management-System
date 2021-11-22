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
    public class BookAllocationController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BookAllocationController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index(string bname)
        {
            if (bname != null)
            {
                IEnumerable<Book> bookResult = _db.Books.Where(x => x.BookName.Contains(bname)).ToList();


                List<BookAllocationVM> baList = new();


                foreach (var book in bookResult)
                {
                    var bookAllocationData = _db.BookAllocations.Where(x => x.BookId == book.BookId && x.ReturnDate == DateTime.MinValue).FirstOrDefault();
                    BookAllocationVM bavm = new();
                    bavm.BookId = book.BookId;
                    bavm.BookName = book.BookName;
                    bavm.isBookAvailable = book.isAvailable;

                    if (bookAllocationData != null)
                    {
                        bavm.MemberId = bookAllocationData.MemberId;
                        var memberData = _db.Members.Where(x => x.MemberId == bavm.MemberId).FirstOrDefault();
                        bavm.IssueDate = bookAllocationData.IssueDate;
                        bavm.DueDate = bavm.IssueDate.AddDays(7);
                        bavm.MemberName = memberData.MemberName;
                    }
                    baList.Add(bavm);
                }
                ViewBag.SearchList = baList;
                ViewBag.haveData = true;
                return View();
            }
            ViewBag.haveData = false;
            return View();
        }

        public IActionResult Issue(int? id)
        {
            IEnumerable<SelectListItem> MemberDropDown = _db.Members.Select(i => new SelectListItem
            {
                Text = i.MemberName,
                Value = i.MemberId.ToString()
            });
            ViewBag.MemberDropDown = MemberDropDown;
            Book bookDetails = _db.Books.Find(id);
            ViewBag.BookData = bookDetails;
            return View();
        }

        [HttpPost]
        public IActionResult IssueBook(BookAllocation ba)
        {
            if (ba == null)
            {
                return BadRequest();
            }
            try
            {
                int bookid = ba.BookId;
                Book book = _db.Books.Find(bookid);
                book.isAvailable = false;
                _db.Books.Update(book);
                _db.BookAllocations.Add(ba);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return Content("Some Error occured. Try Again Later" + e.Message);
            }
        }


        public IActionResult Return(int? id)
        {
            if (id<=0 || id == null)
            {
                return NotFound();
            }
            BookAllocation ba = _db.BookAllocations.Where(x => x.BookId == id && x.ReturnDate.Equals(DateTime.MinValue)).FirstOrDefault();
            ViewBag.BookData = _db.Books.Find(ba.BookId);
            ViewBag.MemberData = _db.Members.Find(ba.MemberId);
            return View(ba);
        }

        [HttpPost]
        public IActionResult ReturnBook(BookAllocation ba)
        {
            Book bk = _db.Books.Find(ba.BookId);
            bk.isAvailable = true;
            _db.Books.Update(bk);
            _db.BookAllocations.Update(ba);
            _db.SaveChanges();
            return RedirectToActionPermanent("index");
        }

    }
}






