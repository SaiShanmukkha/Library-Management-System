using LibrarySystem.Data;
using LibrarySystem.Models;
using LibrarySystem.Models.DataVM;
using Microsoft.AspNetCore.Http;
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


        public IActionResult Index(string BName, string AName)
        {
            if (BName != null && AName != null)
            {
                IEnumerable<Author> authorListData = _db.Authors.Where(x => x.AuthorName.Contains(AName)).OrderBy(x => x.AuthorName).ToList();
                List<BookAllocationVM> baList = new();
                TempData["BName"] = BName;
                //HttpContext.Session.SetString("BName", BName);
                TempData["AName"] = AName;
                //HttpContext.Session.SetString("AName", AName);

                foreach (var author in authorListData)
                {
                    var booklistData = _db.Books.Where(x => x.AuthorId == author.AuthorId && x.BookName.Contains(BName)).ToList();

                    foreach(var bookItem in booklistData)
                    {
                        BookAllocation bookAllocationData = _db.BookAllocations.Where(x => x.BookId == bookItem.BookId && x.ReturnDate.Equals(DateTime.MinValue)).FirstOrDefault();
                        BookAllocationVM bavm = new();

                        if (bookAllocationData != null)
                        {
                            bavm.IssueDate = bookAllocationData.IssueDate;
                            bavm.DueDate = bookAllocationData.DueDate;
                            bavm.BookAllocationId = bookAllocationData.BookAllocationId;
                            Member memberData = _db.Members.Find(bookAllocationData.MemberId);
                            bavm.MemberId = bookAllocationData.MemberId;
                            bavm.MemberName = memberData.MemberName;
                        }

                        bavm.BookId = bookItem.BookId;
                        bavm.BookName = bookItem.BookName;
                        bavm.AuthorName = author.AuthorName;
                        bavm.isBookAvailable = bookItem.isAvailable;

                        baList.Add(bavm);
                    }
                }

                ViewBag.SearchList = baList;
                ViewBag.haveData = baList.Count > 0 ? true : false;
                ViewBag.message = baList.Count > 0 ? "" : "No Data Found";
                return View();
            }
            else if (BName == null && AName != null)
            {
                TempData["BName"] = "";
                //HttpContext.Session.SetString("BName", "");
                TempData["AName"] = AName;
                //HttpContext.Session.SetString("AName", AName);
                IEnumerable<Author> authorResult = _db.Authors.Where(x => x.AuthorName.Contains(AName)).OrderBy(x => x.AuthorName).ToList();
                List<BookAllocationVM> baList = new();

                foreach (var author in authorResult)
                {
                    var bookListData = _db.Books.Where(x => x.AuthorId == author.AuthorId).ToList();
                    foreach(var bookItem in bookListData)
                    {
                        BookAllocation bookAllocationData = _db.BookAllocations.Where(x => x.BookId == bookItem.BookId && x.ReturnDate.Equals(DateTime.MinValue)).FirstOrDefault();
                        BookAllocationVM bavm = new();

                        if (bookAllocationData != null)
                        {
                            bavm.BookAllocationId = bookAllocationData.BookAllocationId;
                            bavm.IssueDate = bookAllocationData.IssueDate;
                            bavm.DueDate = bookAllocationData.DueDate;
                            Member memberData = _db.Members.Find(bookAllocationData.MemberId);
                            bavm.MemberId = bookAllocationData.MemberId;
                            bavm.MemberName = memberData.MemberName;
                        }                        
                        bavm.BookId = bookItem.BookId;
                        bavm.BookName = bookItem.BookName;
                        bavm.AuthorName = author.AuthorName;                        
                        bavm.isBookAvailable = bookItem.isAvailable;
                        baList.Add(bavm);
                    }

                }
                ViewBag.SearchList = baList;
                ViewBag.haveData = baList.Count > 0 ? true : false;
                ViewBag.message = baList.Count > 0 ? "" : "No Data Found";
                return View();

            } else if (BName != null && AName == null)
            {
                IEnumerable<Book> bookResult = _db.Books.Where(x => x.BookName.Contains(BName)).ToList();
                TempData["BName"] = BName;
                //HttpContext.Session.SetString("BName", BName);
                TempData["AName"] = "";
                //HttpContext.Session.SetString("AName", "");
                List<BookAllocationVM> baList = new();


                foreach (var book in bookResult)
                {
                    var bookAllocationData = _db.BookAllocations.Where(x => x.BookId == book.BookId && x.ReturnDate == DateTime.MinValue).FirstOrDefault();
                    BookAllocationVM bavm = new();
                   
                    bavm.BookId = book.BookId;
                    bavm.BookName = book.BookName;
                    bavm.isBookAvailable = book.isAvailable;
                    var authorData = _db.Authors.Where(x => x.AuthorId == book.AuthorId).FirstOrDefault();
                    bavm.AuthorName = authorData.AuthorName;

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
                ViewBag.haveData = baList.Count > 0 ? true : false;
                ViewBag.message = baList.Count > 0 ? "" : "No Data Found";
                return View();
            }
            else {
                TempData["BName"] = "";
                //HttpContext.Session.SetString("BName", "");
                TempData["AName"] = "";
                //HttpContext.Session.SetString("AName", "");
                ViewBag.haveData = false;
                ViewBag.message = "";
                return View();
        } }

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
                TempData.Keep("AName");
                TempData.Keep("BName");
                return RedirectToAction("Index", "BookAllocation", new { AName = TempData["AName"], BName = TempData["BName"] });
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
            TempData.Keep("AName");
            TempData.Keep("BName");
            return RedirectToActionPermanent("index","BookAllocation", new { AName = TempData["AName"], BName = TempData["BName"] });
        }


        //[Route("/MemberBookAllocationData/{id?}")]
        [HttpGet]
        public IActionResult MemberBookAllocationData(int? id)
        {
            IEnumerable<BookAllocation> bookAllocationData = _db.BookAllocations.Where(x => x.MemberId == id).ToList();
            Member memberData = _db.Members.Find(id);
           List<BookAllocationVM> activeAllocations = new();
           List<BookAllocationVM> pastAllocations = new();

            foreach(BookAllocation ba in bookAllocationData)
            {
                Book bookData = _db.Books.Find(ba.BookId);
                BookAllocationVM bavm = new();
                bavm.BookAllocationId = ba.BookAllocationId;
                bavm.BookId = ba.BookId;
                bavm.BookName = bookData.BookName;
                bavm.IssueDate = ba.IssueDate;
                bavm.DueDate = ba.DueDate;
                bavm.ReturnDate = ba.ReturnDate;
                if (ba.ReturnDate.Equals(DateTime.MinValue))
                {
                    activeAllocations.Add(bavm);
                }
                else
                {
                    pastAllocations.Add(bavm);
                }
            }

            ViewBag.ActiveAllocations = activeAllocations;
            ViewBag.PastAllocations = pastAllocations;
            ViewBag.MemberData = memberData;
            return View("MemberBookAllocation");
        }

        //public IActionResult BookAllocationHistory(int? id)
        //{
        //    var history = _db.BookAllocations.Where(x => x.MemberId == id).ToList();
        //    return View(history);
        //}

    }
}






