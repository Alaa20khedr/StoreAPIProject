using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Data.Entities;
using Store.Service.HandleResponses;
using Store.Service.OrderService;
using Store.Service.OrderService.Dtos;
using System.Security.Claims;

namespace Store.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        [HttpPost]
        public async Task<ActionResult<OrderResultDto>>CreateOrderAsync(OrderDto input)
        {
            var order=await orderService.CreateOrderAsync(input);
            if (order is null)
                return BadRequest(new Response(400, "Error While Creating Your Order"));
            return Ok(order);

        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderResultDto>>> GetAllOrdersForUserAsync()
        {
            var email=User.FindFirstValue(ClaimTypes.Email);
            var orders = await orderService.GetAllOrdersForUserAsync(email);
            return Ok(orders);
        }
        [HttpGet]
        public async Task<ActionResult<OrderResultDto>> GetOrderByIdAsync(Guid id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var orders = await orderService.GetOrderByIdAsync(id,email);
            return Ok(orders);
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<DelivaryMethod>>> GetAllDelivaryMethodAsync()
        => Ok(await orderService.GetAllDelivaryMethodAsync());
    }
}
