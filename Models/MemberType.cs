using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    public class MemberType
    {
        [Key]
        public int MemberTypeId { get; set; }
        [Required]
        public string MemberTypeName { get; set; }
    }
}
