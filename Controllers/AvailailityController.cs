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
    public class AvailailityController : ControllerBase
    {
        private readonly IAvailabilityService availabilityService;
        private readonly IMapper _mapper;
        private readonly IConfiguration configuration;

        public AvailailityController(IAvailabilityService availabilityService, IMapper mapper, IConfiguration configuration)
        {
            this.availabilityService = availabilityService;
            _mapper = mapper;
            this.configuration = configuration;
        }

        [HttpGet, Route("GetAllAvailabilities")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllAvailabilities()
        {
            try
            {
                List<Availability> availabilities = availabilityService.GetAvailabilityList();
                List<AvailabilityDTO> availabilityDTO = _mapper.Map<List<AvailabilityDTO>>(availabilities);
                return StatusCode(200, availabilities);

            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet, Route("GetAvailabilityById/{availabilityId}")]
        [Authorize] //all authenticated users can access this
        public IActionResult GetAvailabilityById(string availailityId)
        {
            try
            {
                Availability availability = availabilityService.GetAvailabilityById(availailityId);
                AvailabilityDTO availabilityDTO = _mapper.Map<AvailabilityDTO>(availability);
                if (availability != null)
                    return StatusCode(200, availability);
                else
                    return StatusCode(404, new JsonResult("Invalid Id"));

            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
    }
}
