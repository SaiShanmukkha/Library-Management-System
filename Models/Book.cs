using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        [DisplayName("Book Name:")]
        [Column("Name")]
        public string BookName { get; set; }
        [Required]
        [DisplayName("Book Edition:")]
        [Column("Edition")]
        public string BookEdition { get; set; }
        [Required]
        [DisplayName("Book Publisher:")]
        [Column("Publisher")]
        public string BookPublisher { get; set; }
        [Required]
        [DisplayName("Book Year:")]
        [Column("Year")]
        public int BookYear { get; set; }
        [Required]
        [DisplayName("Book Language:")]
        [Column("Language")]
        public string BookLanguage { get; set; }

        [DisplayName("IS Available")]
        public bool isAvailable { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }

    }
}
