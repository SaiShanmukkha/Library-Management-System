using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    public class BookAllocation
    {
        [Key]
        public int BookAllocationId { get; set; }

        [Required]
        [DisplayName("Issue Date")]
        public DateTime IssueDate { get; set; }

        [Required]
        [DisplayName("Due Date")]
        public DateTime DueDate { get; set; }
        
        
        [DisplayName("Return Date")]
        public DateTime ReturnDate { get; set; }

        [Required]
        public bool IsBookSubmitted { get; set; }

        [ForeignKey("Member")]
        public int MemberId { get; set; }
        public virtual Member Member { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}
