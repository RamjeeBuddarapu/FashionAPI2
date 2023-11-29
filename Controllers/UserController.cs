using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FashionHexa.Entities;
using FashionHexa.Services;
using FashionHexa.Models;
using FashionHexa.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Runtime.ConstrainedExecution;

namespace FashionHexa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper _mapper;
        private readonly IConfiguration configuration;

        public UserController(IUserService userService, IMapper mapper, IConfiguration configuration)
        {
            this.userService = userService;
            _mapper = mapper;
            this.configuration = configuration;
        }
        [HttpGet, Route("GetAllUsers")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllUsers()
        {
            try
            {
                List<User> users = userService.GetAllUsers();
                List<UserDTO> usersDTO = _mapper.Map<List<UserDTO>>(users);
                return StatusCode(200, users);

            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost, Route("Register")]
        [AllowAnonymous] //access the endpoint any any user with out login
        public IActionResult AddUser(UserDTO userDTO)
        {
            try
            {
                User user = _mapper.Map<User>(userDTO);
                userService.CreateUser(user);
                return StatusCode(200, user);
                

            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut, Route("EditUser")]
        [Authorize(Roles = "Customer")]
        public IActionResult EditUser(UserDTO userDTO)
        {
            try
            {
                User user = _mapper.Map<User>(userDTO);
                userService.EditUser(user);
                return StatusCode(200, user);
                

            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete, Route("DeleteUser/{userId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteUser(string userId)
        {
            try
            {
                userService.DeleteUser(userId);
                return StatusCode(200, new JsonResult($"User with Id {userId} is Deleted"));

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

 
        [HttpPost, Route("Validate")]
        [AllowAnonymous]
        public IActionResult Validate(Login login)
        {
            try
            {
                User user = userService.ValidteUser(login.Email, login.Password);
                AuthResponse authReponse = new AuthResponse();
                if (user != null)
                {
                    authReponse.UserName = user.Name;
                    authReponse.RoleName = user.Role.ToString();
                    authReponse.Token = GetToken(user);
                }
                return StatusCode(200, authReponse);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        private string GetToken(User user)
        {
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
            //header part
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature
            );
            //payload part
            var subject = new ClaimsIdentity(new[]
            {
                        new Claim(ClaimTypes.Name,user.Name),
                        new Claim(ClaimTypes.Role, user.Role.ToString()),
                        new Claim(ClaimTypes.Email,user.Email),
                    });

            var expires = DateTime.UtcNow.AddMinutes(10);
            //signature part
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = expires,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            return jwtToken;
        }


    }
}
