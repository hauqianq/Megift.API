using Megift.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Megift.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly MeGiftContext _context;

        public CategoriesController(MeGiftContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _context.Categories.Select(category => new
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName
            }).ToListAsync();
            return Ok(categories);
        }
    }
}
