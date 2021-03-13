using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToDoList.API.DTO;
using ToDoList.API.Models;
using ToDoList.API.DATA;
using ToDoList.API.Services.Interfaces;

namespace ToDoList.API.Controllers
{
    [Authorize]
    public class ToDoItemsUserController : BaseApiController
    {
        private readonly IToDoItemsRepository _todoService;
        private readonly ILogger<ToDoItemsUserController> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ToDoItemsUserController(IToDoItemsRepository todoService, ILogger<ToDoItemsUserController> logger,
            ApplicationDbContext dbContext, IMapper mapper)
        {
            _todoService = todoService;
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        // Get all ToDo's
        [HttpGet("view")]
        public async Task<ActionResult<IEnumerable<ToDoItemDto>>> GetAllAsyncByUser()
        {
            string userIdent = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _dbContext.Users.Single(a => a.Email == userIdent);
            if (user.Role != "role2" && !string.IsNullOrEmpty(user.Email))
            {
                _logger.LogError($"Unknown user tried getting all items.");
                return Unauthorized();
            }

            var items = new List<ToDoItem>();
            items.AddRange(await _todoService.GetCompleteItemsAsync(user));
            items.AddRange(await _todoService.GetIncompleteItemsAsync(user));
            _logger.LogInformation("Returned all items for admin");
            return Ok(items);
        }

        // Get item by Id
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetItemById(int id)
        {
            var item = await _todoService.GetItemAsync(id);
            if (item == null)
            {
                _logger.LogError($"Item with id {id} not found.");
                return NotFound();
            }

            _logger.LogInformation($"Returned item with ID {id}");
            return Ok(item);
        }

        // Create item
        [HttpPost("create")]
        public async Task<ActionResult<ToDoItem>> CreateItem([FromQuery] ToDoItemDto item)
        {
            string userIdent = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _dbContext.Users.Single(a => a.Email == userIdent);
            if (user.Role != "role2" && !string.IsNullOrEmpty(user.Email))
            {
                _logger.LogError($"Unknown user tried getting all items.");
                return Unauthorized();
            }

            if (item == null)
            {
                _logger.LogError($"Received null item.");
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Received invalid item.");
                return BadRequest();
            }

            // create mapping
            var dbItem = _mapper.Map<ToDoItem>(item);
            await _todoService.AddItemAsync(dbItem, user);

            _logger.LogInformation($"Created new TodoItem with id {dbItem.Id}");
            return CreatedAtAction(nameof(GetItemById), new {Id = dbItem.Id}, dbItem);
        }

        // Update item
        [HttpPut("{id}")]
        public async Task<ActionResult<ToDoItem>> UpdateItemAsync([FromQuery] ToDoItemUpdateDto newItem, int id)
        {
            string userIdent = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _dbContext.Users.Single(a => a.Email == userIdent);
            if (user.Role != "role2" && !string.IsNullOrEmpty(user.Email))
            {
                _logger.LogError($"Unknown user tried getting all items.");
                return Unauthorized();
            }

            var items = new List<ToDoItem>();
            items.AddRange(await _todoService.GetCompleteItemsAsync(user));
            items.AddRange(await _todoService.GetIncompleteItemsAsync(user));
            _logger.LogInformation("Returned all items for admin");

            var dbItem = await _todoService.GetItemAsync(id);
            if (dbItem == null)
            {
                _logger.LogError($"Item with id {id} not found.");
                return NotFound();
            }

            if (string.IsNullOrEmpty(newItem.TaskDescription))
            {
                newItem.TaskDescription = dbItem.TaskDescription;
            }
            dbItem = new ToDoItem()
            {
                Id = dbItem.Id,
                TaskDescription = newItem.TaskDescription,
                IsDone = newItem.IsDone
            };

            await _todoService.UpdateTodoAsync(dbItem, user);

            _logger.LogInformation($"Updated item with id {dbItem.Id}.");
            return Ok(items);
        }

        // Delete item
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(int id)
        {
            string userIdent = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _dbContext.Users.Single(a => a.Email == userIdent);
            if (user.Role != "role2" && !string.IsNullOrEmpty(user.Email))
            {
                _logger.LogError($"Unknown user tried getting all items.");
                return Unauthorized();
            }

            await _todoService.DeleteTodoAsync(id);
            _logger.LogInformation($"Removed item with id {id}.");
            var items = new List<ToDoItem>();
            items.AddRange(await _todoService.GetCompleteItemsAsync(user));
            items.AddRange(await _todoService.GetIncompleteItemsAsync(user));
            _logger.LogInformation("Returned all items for admin");
            return Ok(items);
        }
    }
}