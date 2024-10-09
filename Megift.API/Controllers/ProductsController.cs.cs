using Megift.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Megift.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly MeGiftContext _context;

        public ProductController(MeGiftContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _context.Products.Include(product => product.Category).Select(product => new
            {
                Name = product.Name,
                SKU = product.Sku,
                Price = product.Price,
                Stock = product.Stock,
                Category = product.Category.Name
            }).ToListAsync();
            return Ok(products);
        }
    }
}
