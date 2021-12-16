using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibrarySystem.Models.DataVM
{
    public class MemberVM
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public string MemberGender { get; set; }
        public string MemberDOB { get; set; }
        public string StreetAddress { get; set; }
        public string SSN { get; set; }
        public string LibraryName { get; set; }
        public string languagesKnown { get; set; }
        public string OtherCountry { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public long PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Profession { get; set; }
    }
}
