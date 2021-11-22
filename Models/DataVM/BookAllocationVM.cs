using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibrarySystem.Models.DataVM
{
    public class BookAllocationVM
    {
        public int BookAllocationId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsBookSubmitted { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public bool isBookAvailable { get; set; }


    }
}
