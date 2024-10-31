using Megift.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Megift.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly MeGiftContext _context;

        public CustomersController(MeGiftContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _context.Customers.Include(c=>c.User)
                                                .Select(c=> new
                                                {
                                                    CustomerId = c.CustomerId,
                                                    Name = c.FirstName + " " + c.LastName,
                                                    Address = c.Address,
                                                    PhoneNumber = c.PhoneNumber,
                                                    Username = c.User.Username,
                                                    Email = c.User.Email,
                                                    CreatedAt = c.User.CreatedAt,
                                                    LastLogin = c.User.LastLogin
                                                })
                                                .ToListAsync();
            return Ok(customers); 
        }
    }
}
