using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToDoList.BL.Models;
using ToDoList.DAL;
using ToDoList.DAL.Services.Interfaces;

namespace ToDoList.API.Controllers
{
    
    [Authorize]
    public class ToDoItemsController : BaseApiController
    {
        private readonly IToDoItemsRepository _todoService;
        private readonly ILogger<ToDoItemsController> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;

        public ToDoItemsController(IToDoItemsRepository todoService,  ILogger<ToDoItemsController> logger, 
            ApplicationDbContext dbContext, UserManager<AppUser> userManager)
        {
            _todoService = todoService;
            _logger = logger;
            _dbContext = dbContext;
            _userManager = userManager;
        }

        // Get all items
        [HttpGet("admin")]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetAllAsync()
        {
            // if (await _todoService.UserExists(App.Email))
            // {
            //     return BadRequest("Username alredy exists");
            // }
           
            // if(user == null)
            // {
            //     _logger.LogError($"Unknown user tried getting all items.");
            //     return Unauthorized();
            // }
            var items = new List<ToDoItem>();
            items.AddRange(await _todoService.AllItemsAsync());
            _logger.LogInformation("Returned all items for admin");
            return Ok(items);
        }
        // Get all items
        [HttpPost("user")]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetAllAsyncByUser()
        {
            var user = await _userManager.GetUserAsync(User);
            string email = User.FindFirst(ClaimTypes.Email)?.Value;
            //var user = _dbContext.Users.
           // var user = _dbContext.Users.SingleOrDefault(x => x.Email == email);
            if(user == null)
            {
                _logger.LogError($"Unknown user tried getting all items.");
                return Unauthorized();
            }
            var items = new List<ToDoItem>();
            items.AddRange(await _todoService.GetCompleteItemsAsync(user));
            items.AddRange(await _todoService.GetIncompleteItemsAsync(user));
            
            _logger.LogInformation($"Returned all items to {user.Email}");
            return Ok();
        }

    }
}