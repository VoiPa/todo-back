using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.BL.Models;
using ToDoList.DAL;

namespace ToDoList.API.Controllers
{
    public class ErrorController : BaseApiController
    {
        private readonly ApplicationDbContext _context;

        public ErrorController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "Just text";
        }
        [HttpGet("not-found")]
        public ActionResult<User> GetNotFound()
        {
            var user = _context.Users.Find(-1);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        
        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            var user = _context.Users.Find(-1);
            var returnUser = user.ToString();
            
            return returnUser;
        }
        
        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("This was not a good request, I belive you can better");
        }
    }
}