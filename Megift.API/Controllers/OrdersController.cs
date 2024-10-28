using Megift.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Megift.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly MeGiftContext _context;

        public OrderController(MeGiftContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _context.Orders.Include(order => order.Customer).Include(order=>order.OrderDetails)
                .Select(order => new
            {
                ProductName = order.OrderDetails.FirstOrDefault().Product.ProductName,
                Date = order.OrderDate,
                Total = order.TotalAmount,
                Status = order.Status,
            }).ToListAsync();
            return Ok(orders);
        }
    }
}
