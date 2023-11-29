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
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IMapper _mapper;
        private readonly IConfiguration configuration;

        public OrderController(IOrderService orderService, IMapper mapper, IConfiguration configuration)
        {
            this.orderService = orderService;
            _mapper = mapper;
            this.configuration = configuration;
        }

        [HttpPost, Route("PlaceOrder")]
        [Authorize(Roles = "Customer")]
        public IActionResult PlaceOrder(Order order)
        {
            try
            {
                order.OrderId = Guid.NewGuid(); //genereate new guid
                orderService.PlaceOrder(order);
                return StatusCode(200, order);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet, Route("GetOrdersByUser/{userId}")]
        [Authorize(Roles = "Customer")]
        public IActionResult GetOrdersByUser(string userId)
        {
            try
            {
                List<Order> orders = orderService.GetOrdersByUser(userId);
                return StatusCode(200, orders);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet, Route("GetOrders")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetOrders()
        {
            try
            {
                List<Order> orders = orderService.GetOrders();
                return StatusCode(200, orders);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet, Route("GetOrderById/{orderId}")]
        [Authorize] //all authenticated users can access
        public IActionResult GetOrderById(Guid orderId)
        {
            try
            {
                Order order = orderService.GetOrder(orderId);
                OrderDTO orderDTO = _mapper.Map<OrderDTO>(order);
                if (order != null)
                    return StatusCode(200, order);
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
