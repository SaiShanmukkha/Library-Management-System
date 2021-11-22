using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        [Required]
        [DisplayName("Author Name")]
        [Column("Name")]
        public string AuthorName { get; set; }

        [Required]
        [DisplayName("Author Email")]
        [Column("Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DisplayName("Phone Number")]
        [MinLength(10, ErrorMessage = "Enter Valid Phone Number")]
        [MaxLength(10, ErrorMessage = "Enter Valid Phone Number")]
        [Phone]
        [Column("Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [DisplayName("Author Country")]
        [Column("Country")]
        public string AuthorCountry { get; set; }

        [Required]
        [DisplayName("Author Expertise")]
        [Column("Expertise")]
        public string AuthorExpertise { get; set; }
    }
}
