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
    public class BrandController : ControllerBase
    {
        private readonly IBrandService brandService;
        private readonly IMapper _mapper;
        private readonly IConfiguration configuration;
        public BrandController(IBrandService brandService, IMapper mapper, IConfiguration configuration)
        {
            this.brandService=brandService;
            _mapper = mapper;
            this.configuration = configuration;
        }

        [HttpGet, Route("GetAllBrands")]
        public IActionResult GetAllBrands()
        {
            try
            {
                List<Brand>brands = brandService.GetAllBrands();
                List<BrandDTO> brandDTO = _mapper.Map<List<BrandDTO>>(brands);
                return StatusCode(200, brands);


            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost, Route("AddBrands")]
        [AllowAnonymous] //access the endpoint any any user with out login
        public IActionResult AddBrand(BrandDTO brandDTO)
        {
            try
            {
                Brand brand= _mapper.Map<Brand>(brandDTO);

                brandService.AddBrand(brand);
                return StatusCode(200, brand);
                // return Ok(); //return emplty result

            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut, Route("UpdateBrand")]
        public IActionResult UpdateBrand(BrandDTO brandDTO)
        {
            try
            {
                Brand brand = _mapper.Map<Brand>(brandDTO);
                brandService.UpdateBrand(brand);
                return StatusCode(200, brand);
                // return Ok(); //return emplty result

            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete, Route("DeleteBrand/{BrandId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteBrand(string BrandId)
        {
            try
            {
                brandService.DeleteBrand(BrandId);
                return StatusCode(200, new JsonResult($"Brand with Id {BrandId} is Deleted"));

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet, Route("GetBrandById/{brandId}")]
        [Authorize] //all authenticated users can access
        public IActionResult BrandById(string brandId)
        {
            try
            {
                Brand brand = brandService.BrandById(brandId);
                BrandDTO brandDTO = _mapper.Map<BrandDTO>(brand);
                if (brand != null)
                    return StatusCode(200, brand);
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
