using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        
        [AllowAnonymous]
        [HttpPost("register")]
        /*
         * USER registration API
         */
        public async Task<IActionResult> Register(RegisterDTO registerDto)
        {
            registerDto.Email = registerDto.Email.ToLower();
            /* Checking if user exists */
            if (await _repository.UserExists(registerDto.Email))
            {
                return BadRequest("Username alredy exists");
            }

            var userToCreate = new AppUser
            {
                Email = registerDto.Email,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };
           var createdAppUser= await _repository.Register(userToCreate, registerDto.Password);
            return StatusCode(201);
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDto)
        {
            var userFromRepo = await _repository.Login(loginDto.Email, loginDto.Password);
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