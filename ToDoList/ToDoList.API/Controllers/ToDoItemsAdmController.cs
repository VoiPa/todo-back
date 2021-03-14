using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToDoList.API.DTO;
using ToDoList.API.Entities;
using ToDoList.API.Services.Interfaces;

namespace ToDoList.API.Controllers
{
    [Authorize]
    public class ToDoItemsAdmController : BaseApiController
    {
        private readonly IToDoItemsRepository _todoService;
        private readonly ILogger<ToDoItemsAdmController> _logger;


        public ToDoItemsAdmController(IToDoItemsRepository todoService, ILogger<ToDoItemsAdmController> logger)
        {
            _todoService = todoService;
            _logger = logger;
          
        }

        // Get all ToDo's
        [HttpPost("view")]
        public async Task<ActionResult<IEnumerable<ToDoItemDto>>> GetAllAsync()
        {
            string userIdent = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user =  _todoService.ValidateUserAsync(userIdent);
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
            var user = _todoService.ValidateUserAsync(userIdent);
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