using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ToDoList.BL.Models;
using ToDoList.DAL.Services.Interfaces;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ToDoList.DAL.Services.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            _key=new SymmetricSecurityKey (Encoding.UTF8.GetBytes(config.GetSection("TokenKey").Value));
        }

        public string CreateToken(User user)
        {
            /* adding claims */
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Email)
            };
            
            /*  creating credentials */
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            
            /*  describing how our token must look */
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            
            /* creating token*/
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            /* return token who ever needs him*/
            return tokenHandler.WriteToken(token);

        }
    }
}