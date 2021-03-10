using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ToDoList.BL.Models;
using ToDoList.DAL;

namespace ToDoList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public UsersController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            var users = _dbContext.Users.ToList();
            return users;
        }
    }
}