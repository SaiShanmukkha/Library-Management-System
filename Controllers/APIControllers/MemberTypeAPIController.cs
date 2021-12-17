using LibrarySystem.Data;
using LibrarySystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LibrarySystem.Controllers.APIControllers
{
    [Route("api/membertype")]
    [ApiController]
    public class MemberTypeAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public MemberTypeAPIController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpPost("createMemberType")]
        public IActionResult CreateMemberType(MemberType memberType)
        {
            if (memberType == null)
            {
                return BadRequest();
            }
            _db.MemberTypes.Add(memberType);
            _db.SaveChanges();
            return Ok();
        }


        [HttpGet("GetMemberTypes")]
        public IActionResult GetMemberTypes()
        {
            var memberTypes = _db.MemberTypes.ToList();
            return Ok(memberTypes);
        }

        [HttpGet("getMemberType/{id}")]
        public IActionResult GetMemberById(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var memberType = _db.MemberTypes.Find(id);
            if (memberType == null)
            {
                return NotFound();
            }
            return Ok(memberType);
        }

        [HttpPost("updateMemberType")]
        public IActionResult UpdateMemberType(MemberType memberType)
        {
            if (memberType == null)
            {
                return BadRequest();
            }
            _db.MemberTypes.Update(memberType);
            _db.SaveChanges();
            return Ok();
        }

        [HttpDelete("deleteMemberType/{id}")]
        public IActionResult DeleteMemberType(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var obj = _db.MemberTypes.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.MemberTypes.Remove(obj);
            _db.SaveChanges();
            return Ok();
        }
    }
}
