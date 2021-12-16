using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }

        [DisplayName("Member Name:")]
        [Column("Name")]
        [Required]
        public string MemberName { get; set; }

        [DisplayName("Gender:")]
        [Column("Gender")]
        [Required]
        public string MemberGender { get; set; }

        [DisplayName("Date of Birth:")]
        [Column("Date of Birth")]
        [Required]
        public DateTime MemberDOB { get; set; }

        [DisplayName("Street Address")]
        [Column("Street Address")]
        [Required]
        public string StreetAddress { get; set; }


        public string SSN { get; set; }

        public string languagesKnown { get; set; }

        public string LibraryName { get; set; }

        public string OtherCountry { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        [DisplayName("Phone Number")]
        [MinLength(10,ErrorMessage ="Enter Valid Phone Number")]
        [MaxLength(10, ErrorMessage = "Enter Valid Phone Number")]
        [Phone]
        public long PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Profession { get; set; }

        [ForeignKey("MemberType")]
        public int? MemberTypeId { get; set; }

        public virtual MemberType MemberType { get; set; }
    }
}
