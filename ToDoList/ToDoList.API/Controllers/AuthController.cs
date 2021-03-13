using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.API.DTO;
using ToDoList.API.Models;
using ToDoList.API.Services.Interfaces;

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
        public async Task<IActionResult> Register([FromQuery] RegisterDto registerDto)
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
            var createdAppUser = await _repository.Register(userToCreate, registerDto.Password);
            return StatusCode(201);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login([FromQuery] LoginDTO loginDto)
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

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword([FromQuery] ForgotPasswordRequest model)
        {
            _repository.ForgotPassword(model, Request.Headers["origin"]);
            return Ok(new {message = "Please check your email for password reset instructions"});
        }
        
        [AllowAnonymous]
        [HttpPost("reset-password")]
        public IActionResult ResetPassword([FromQuery] ResetPasswordRequest model)
        {
            _repository.ResetPassword(model);
            return Ok(new {message = "Password reset successful, you can now login"});
        }
    }
}