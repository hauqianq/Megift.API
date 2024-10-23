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
            var customers = await _context.Customers.Select(customer => new
            {
                Id = customer.Id,
                Image = customer.Image,
                Name = customer.Name,
                Email = customer.Email,
                ShippingAddress = customer.ShippingAddress
            }).ToListAsync();
            
            return Ok(customers);
        }
    }
}
