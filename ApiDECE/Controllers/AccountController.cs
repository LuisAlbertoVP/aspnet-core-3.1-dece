using System;
using System.Text;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ApiDECE.Models.BD;
using ApiDECE.Models.DTO.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace ApiDECE.Controllers
{
    [Route("Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserDAO dao = new UserDAO();
        private readonly IConfiguration _configuration;


        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [Route("Login")]
        [HttpPost]
        public IActionResult Login([FromBody] Usuario user)
        {
            user = dao.Login(HashPassword(user));
            if (user != null)
            {
                string estado = user.Estado;
                if (estado.Equals("1"))
                {
                    return CreateToken(user);
                }
                return Unauthorized();
            }
            return NotFound();
        }

        private Usuario HashPassword(Usuario user)
        {
            var combinedPassword = string.Concat(user.Clave, "S3creT-@D3cE_2o20:LV+1");
            var sha256 = new SHA256Managed();
            var bytes = Encoding.UTF8.GetBytes(combinedPassword);
            var hash = sha256.ComputeHash(bytes);
            user.Clave = Convert.ToBase64String(hash);
            return user;
        }

        public IActionResult CreateToken(Usuario user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, System.Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, user.Id)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Tokens:Issuer"], _configuration["Tokens:Audience"],
                claims, notBefore: DateTime.UtcNow ,expires: DateTime.UtcNow.AddHours(5), signingCredentials: credentials);
           
            var results = new
            {
                id = user.Id,
                nombres = user.NombreCompleto,
                key = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            };

            return Ok(results);
        } 
    } 
} 