using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Security.Claims;
using Tala.Core.Entities.Order_Aggregate;
using Tala.Core.Services;
using Talab.Api.Dtos;

namespace Talab.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService,
            IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Tala.Core.Entities.Order_Aggregate.Order>> CreateOrder(OrderDto orderDto)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var shippingAddress = _mapper.Map<AddressDto,Address>(orderDto.ShippingAddress);

           var order = await _orderService.CreateOrderAsync(buyerEmail, orderDto.BasketId, orderDto.DeliverMethodId,shippingAddress);
            if (order is null)
                return BadRequest();
            return Ok(order);
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Tala.Core.Entities.Order_Aggregate.Order>>> GetOrdersForUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderService.GetOrdersForUserAsync(buyerEmail);
            return Ok(orders);
        }
        
    }

}
