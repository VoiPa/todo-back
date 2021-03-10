using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ToDoList.API.DTO;
using ToDoList.BL.Models;
using ToDoList.DAL.Services.Interfaces;

namespace ToDoList.API.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthRepository _repository;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }
        [HttpPost ("register")]
        /*
         * USER registration API
         */
        public async Task<IActionResult>Register(UserForRgisterDTO userForRgisterDto)
        {
            userForRgisterDto.Email = userForRgisterDto.Email.ToLower();
            /* Checking if user exists */
            if (await _repository.UserExists(userForRgisterDto.Email))
            {
                return BadRequest("Username alredy exists");
            }

            var userToCreate = new User
            {
                Email = userForRgisterDto.Email,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
                
            };
            var createdUser = await _repository.Register(userToCreate,userForRgisterDto.Password);
            return StatusCode(201);
        }
    }
}