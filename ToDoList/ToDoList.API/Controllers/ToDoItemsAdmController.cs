using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToDoList.API.DTO;
using ToDoList.BL.Models;
using ToDoList.DAL;
using ToDoList.DAL.Services.Interfaces;

namespace ToDoList.API.Controllers
{
    [Authorize]
    public class ToDoItemsAdmController : BaseApiController
    {
        private readonly IToDoItemsRepository _todoService;
        private readonly ILogger<ToDoItemsAdmController> _logger;
        private readonly ApplicationDbContext _dbContext;

        public ToDoItemsAdmController(IToDoItemsRepository todoService, ILogger<ToDoItemsAdmController> logger,
            ApplicationDbContext dbContext)
        {
            _todoService = todoService;
            _logger = logger;
            _dbContext = dbContext;
        }

        // Get all ToDo's
        [HttpPost("view")]
        public async Task<ActionResult<IEnumerable<ToDoItemDto>>> GetAllAsync()
        {
            string userIdent = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _dbContext.Users.Single(a => a.Email == userIdent);
            if (user.Role != "role1")
            {
                _logger.LogError($"Unknown user tried getting all items.");
                return Unauthorized();
            }

            var items = new List<ToDoItem>();
            items.AddRange(await _todoService.AllItemsAsync());
            _logger.LogInformation("Returned all items for admin");
            return Ok(items);
        }

        // Delete item
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(int id)
        {
            string userIdent = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _dbContext.Users.Single(a => a.Email == userIdent);
            if (user.Role != "role1")
            {
                _logger.LogError($"Unknown user tried getting all items.");
                return Unauthorized();
            }

            await _todoService.DeleteTodoAsync(id);
            _logger.LogInformation($"Removed item with id {id}.");
            var items = new List<ToDoItem>();
            items.AddRange(await _todoService.AllItemsAsync());
            _logger.LogInformation("Returned all items for admin");
            return Ok(items);
        }
    }
}