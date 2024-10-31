using Megift.API.Enumerations;
using Megift.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Megift.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly MeGiftContext _context;

        public OrdersController(MeGiftContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _context.Orders.Include(order => order.Customer)
                                                .Include(order=> order.OrderDetails).ThenInclude(orderDetail => orderDetail.Product)
                                                .Select(order => new
            {

                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                OrderType = ((OrderType)order.OrderType).ToString(),
                OrderStatus = order.Status,
                Customer = order.Customer.FirstName + " " + order.Customer.LastName,
                TotalAmount = order.TotalAmount,
                OrderDetails = order.OrderDetails.Select(orderDetail => new
                {
                    ProductImage = orderDetail.Product.ImageUrl,
                    ProductName = orderDetail.Product.ProductName,
                    Quantity = orderDetail.Quantity,
                    Price = orderDetail.Product.Price,
                }).ToList()
            }).ToListAsync();
            return Ok(orders);
        }
    }
}
