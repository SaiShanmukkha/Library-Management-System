using LibrarySystem.Data;
using LibrarySystem.Models;
using LibrarySystem.Models.DataVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibrarySystem.Controllers.APIControllers
{
    [Route("api/Data")]
    [ApiController]
    public class BAAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public BAAPIController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("BookAllocationHistory")]
        public IActionResult GetBookAllocationHistory(int? id)
        {
            if(id<=0 || id is null)
            {
                return BadRequest();
            }
            var bookAVMList = _db.BookAllocations.Where(x => x.MemberId == id).ToList();

            var  history= new List<BookAllocationVM>();

            foreach (var item in bookAVMList)
            {
                var bookAVM = new BookAllocationVM();
                bookAVM.BookId = item.BookId;
                Book  book= _db.Books.Where(x=>x.BookId==item.BookId).FirstOrDefault();
                bookAVM.BookName = book.BookName;
                bookAVM.IssueDate = item.IssueDate;
                bookAVM.DueDate = item.DueDate;
                bookAVM.ReturnDate = item.ReturnDate;
                bookAVM.BookAllocationId = item.BookAllocationId;
                bookAVM.IsBookSubmitted = item.IsBookSubmitted;
                bookAVM.isBookAvailable = item.IsBookSubmitted;
                history.Add(bookAVM);
            }
            return Ok(history);
        }


        [HttpGet("GetOverDue/{id}")]
        public IActionResult GetOverDue(int? id)
        {
            if (id <= 0 || id is null)
            {
                return BadRequest();
            }
            var bookAVMList = _db.BookAllocations.Where(x => x.MemberId == id && x.DueDate.AddDays(3).CompareTo(DateTime.Today)>=0 && x.ReturnDate.Equals(DateTime.MinValue)).ToList();
            return Ok(bookAVMList.Count);
        }

        [HttpGet("GetPastDue/{id}")]
        public IActionResult GetPastDue(int? id)
        {
            if(id<=0 || id is null)
            {
                return BadRequest();
            }
            var bookAVMList = _db.BookAllocations.Where(x => x.MemberId == id && x.DueDate.CompareTo(DateTime.Today) < 0 && x.ReturnDate.Equals(DateTime.MinValue)).ToList();
            return Ok(bookAVMList.Count);
        }

       
        [HttpGet("greet")]
        public IActionResult Greet()
        {
            return Ok("Hello World");
        }


    }
}
