using Megift.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Megift.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly MeGiftContext _context;

        public CustomerController(MeGiftContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _context.Customers.Include(c=>c.User).Select(customer => new
            {
                Id = customer.CustomerId,
                //Image = customer.Image,
                Name = customer.User.Username,
                Email = customer.User.Email,
                ShippingAddress = customer.Address
            }).ToListAsync();
            
            return Ok(customers);
        }
    }
}
