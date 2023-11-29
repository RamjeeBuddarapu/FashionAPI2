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
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IMapper _mapper;
        private readonly IConfiguration configuration;
        public ProductController(IProductService productService, IMapper mapper, IConfiguration configuration)
        {
            this.productService = productService;
            _mapper = mapper;
            this.configuration = configuration;
        }

        [HttpGet, Route("GetAllProducts")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllProducts()
        {
            try
            {
                List<Product> products = productService.GetProducts();
                List<ProductDTO> productsDTO = _mapper.Map<List<ProductDTO>>(products); 
                return StatusCode(200, productsDTO); 
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet, Route("GetProductById/{productId}")]
        [Authorize] //all authenticated users can access this
        public IActionResult GetProductById(int productId)
        {
            try
            {
                Product product = productService.GetProductById(productId);
                ProductDTO productDTO = _mapper.Map<ProductDTO>(product);
                if (product != null)
                    return StatusCode(200, product);
                else
                    return StatusCode(404, new JsonResult("Invalid Id")); 
                
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, Route("AddProduct")]
        public IActionResult Add([FromBody] ProductDTO productDTO)
        {
            try
            {
                Product product = _mapper.Map<Product>(productDTO); 
                productService.AddProduct(product);
                return StatusCode(200, product); 
                
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut, Route("EditProduct")]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(ProductDTO productDTO)
        {
            try
            {
                Product product = _mapper.Map<Product>(productDTO); 
                productService.UpdateProduct(product);
                return StatusCode(200, product);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete, Route("DeleteProduct/{productId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int productId)
        {
            try
            {
                productService.DeleteProduct(productId);
                return StatusCode(200, new JsonResult($"Product with Id {productId} is Deleted"));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet, Route("GetProductByPrice/{Price}")]
        [Authorize]
        public IActionResult GetProductByPrice(double price) 
        {
            try
            {
                List<Product> product = productService.GetProductsByPrice(price);
                if (product != null)
                    return StatusCode(200, product);
                else
                    return StatusCode(404, new JsonResult("Invalid Price Range"));
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }
    }
}
