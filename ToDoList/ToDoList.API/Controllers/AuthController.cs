using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList.API.DTO;
using ToDoList.API.Services.Interfaces;
using ToDoList.BL.Models;
using ToDoList.DAL.Services.Interfaces;

namespace ToDoList.API.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly IAuthRepository _repository;
        private readonly ITokenService _tokenService;

        public AuthController(IAuthRepository repository, ITokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        /*
         * USER registration API
         */
        public async Task<IActionResult> Register(UserForRgisterDTO userForRgisterDto)
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
            var createdUser = await _repository.Register(userToCreate, userForRgisterDto.Password);
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(UserForLoginDTO userForLoginDto)
        {
            var userFromRepo = await _repository.Login(userForLoginDto.Email, userForLoginDto.Password);
            if (userFromRepo == null)
            {
                return Unauthorized("Incorrect user name or password");
            }

            return new UserDTO
            {
                Email = userFromRepo.Email,
                Token = _tokenService.CreateToken(userFromRepo)
            };
        }
    }
}