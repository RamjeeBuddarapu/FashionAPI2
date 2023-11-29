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
    public class UserRatingsController : ControllerBase
    {
        private readonly IUserRatingsService userRatingsService;
        private readonly IMapper _mapper;
        private readonly IConfiguration configuration;

        public UserRatingsController(IUserRatingsService userRatingsService, IMapper mapper, IConfiguration configuration)
        {
            this.userRatingsService = userRatingsService;
            _mapper = mapper;
            this.configuration = configuration;
        }
        [HttpPost,Route("SubmitRating")]
        [Authorize(Roles ="Customer")]
        public IActionResult SubmitRating(UserRatingsDTO userRatingsDTO)
        {
            try
            {
                UserRatings userRatings = _mapper.Map<UserRatings>(userRatingsDTO);
                userRatingsService.AddRating(userRatings);
                return StatusCode(200, userRatings);


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpGet,Route("GetRatingsByProduct")]
        [Authorize]
        public IActionResult RatingsByproductId(string productId)
        {
            try
            {
                
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }






    }
}
