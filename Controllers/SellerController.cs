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
    public class SellerController : ControllerBase
    {

        private readonly ISellerService sellerService;
        private readonly IMapper _mapper;
        private readonly IConfiguration configuration;
        public SellerController(ISellerService sellerService, IMapper mapper, IConfiguration configuration)
        {
            this.sellerService = sellerService;
            _mapper = mapper;
            this.configuration = configuration;
        }

        [HttpGet, Route("GetAllSellers")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllSellers()
        {
            try
            {
                List<Seller> sellers = sellerService.GetAllSeller();
                List<SellerDTO> sellerDTO = _mapper.Map<List<SellerDTO>>(sellers);
                return StatusCode(200, sellers);

            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost, Route("Register Seller")]
        [AllowAnonymous] //access the endpoint any any user with out login
        public IActionResult AddSeller(SellerDTO sellerDTO)
        {
            try
            {
                Seller seller = _mapper.Map<Seller>(sellerDTO);
                sellerService.AddSeller(seller);
                return StatusCode(200, seller);


            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut, Route("EditSeller")]
        [Authorize(Roles = "Seller")]
        public IActionResult EditSeller(SellerDTO sellerDTO)
        {
            try
            {
                Seller seller = _mapper.Map<Seller>(sellerDTO);
                sellerService.UpdaterSeller(seller);
                return StatusCode(200, seller);


            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete, Route("DeleteSeller/{sellerId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteSeller(string sellerId)
        {
            try
            {
                sellerService.Deleteseller(sellerId);
                return StatusCode(200, new JsonResult($"User with Id {sellerId} is Deleted"));

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet, Route("GetSellerById/{SellerId}")]
        [Authorize(Roles ="Admin")]
        public IActionResult SellerById(string sellerId)
        {
            try
            {
                Seller seller = sellerService.GetSellerById(sellerId);
                SellerDTO sellerDTO = _mapper.Map<SellerDTO>(seller);
                if (seller != null)
                    return StatusCode(200, seller);
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
