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
    public class RoleController : ControllerBase
    {
        private readonly IRoleService roleService;
        private readonly IMapper _mapper;
        private readonly IConfiguration configuration;
        public RoleController(IRoleService roleService , IMapper mapper, IConfiguration configuration)
        {
            this.roleService= roleService;
            _mapper = mapper;
            this.configuration = configuration;
        }

        [HttpGet, Route("GetAllRoles")]
        public IActionResult GetAll()
        {
            try
            {
                List<Role> Roles = roleService.GetAllRoles();
                List<RoleDTO> roleDTO = _mapper.Map<List<RoleDTO>>(Roles);
                return StatusCode(200, Roles);
                
                
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost, Route("Register")]
        [AllowAnonymous] //access the endpoint any any user with out login
        public IActionResult AddRoles(RoleDTO roleDTO)
        {
            try
            {
                Role role = _mapper.Map<Role>(roleDTO);

                roleService.AddRole(role);
                return StatusCode(200,role);
                // return Ok(); //return emplty result

            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet, Route("GetRoleByName")]
        public IActionResult GetRoleByName(string roleName)
        {
            try
            {
                List<Role> role = roleService.GetRoleByName(roleName);
                if (role != null)
                    return StatusCode(200, role);
                else
                    return StatusCode(404, new JsonResult("Invalid Author"));


            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut, Route("UpdateRole")]
        public IActionResult UpdateRole(RoleDTO roleDTO)
        {
            try
            {
                Role role = _mapper.Map<Role>(roleDTO);
                roleService.UpdateRole(role);
                return StatusCode(200, role);
                // return Ok(); //return emplty result

            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }






    }
}
