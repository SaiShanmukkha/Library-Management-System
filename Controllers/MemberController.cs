using LibrarySystem.Data;
using LibrarySystem.Models;
using LibrarySystem.Models.DataVM;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibrarySystem.Controllers
{

    public class MemberController : Controller
    {

        private readonly ApplicationDbContext _db;
        public MemberController(ApplicationDbContext db)
        {
            _db = db;
        }



        public IActionResult Index()
        {
            IEnumerable<Member> objList = _db.Members;
            return View(objList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Member obj = new();
            return View(obj);
        }

        [HttpPost]
       // [ValidateAntiForgeryToken]
        public IActionResult Create(MemberVM memberVM)
        {
            if (ModelState.IsValid)
            {
                Member member = new();
                member.MemberName = memberVM.MemberName;
                member.OtherCountry = memberVM.OtherCountry;
                member.MemberGender = memberVM.MemberGender;
                member.PhoneNumber = memberVM.PhoneNumber;
                member.SSN = memberVM.SSN;
                member.LibraryName = memberVM.LibraryName;
                member.languagesKnown = memberVM.languagesKnown;
                member.Profession = memberVM.Profession;
                member.City = memberVM.City;
                member.StreetAddress = memberVM.StreetAddress;
                member.Country = memberVM.Country;
                member.OtherCountry = memberVM.OtherCountry;
                member.MemberTypeId = memberVM.MemberTypeId;
                member.Email = memberVM.Email;
                member.MemberDOB = DateTime.Parse(memberVM.MemberDOB);
                _db.Members.Add(member);
                _db.SaveChanges();
                //return RedirectToAction("Index");
                return Ok();
            }
            return View(memberVM);
        }


        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id is <= 0 or null)
            {
                return NotFound();
            }

            Member obj = _db.Members.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Update(MemberVM memberVM)
        {

            if (ModelState.IsValid)
            {
                if (memberVM == null)
                {
                    return NotFound();
                }
                Member member = new();
                member.MemberId = memberVM.MemberId;
                member.MemberName = memberVM.MemberName;
                member.MemberGender = memberVM.MemberGender;
                member.PhoneNumber = memberVM.PhoneNumber;
                member.Profession = memberVM.Profession;
                member.City = memberVM.City;
                member.StreetAddress = memberVM.StreetAddress;
                member.Country = memberVM.Country;
                member.Email = memberVM.Email;
                member.MemberDOB = DateTime.Parse(memberVM.MemberDOB);
                member.OtherCountry = memberVM.OtherCountry;
                _db.Members.Update(member);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(memberVM);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is <= 0 or null)
            {
                return NotFound();
            }

            Member obj = _db.Members.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(MemberVM memberVM)
        {
            int id = memberVM.MemberId;
            Member member = _db.Members.Find(id);

            _db.Members.Remove(member);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
