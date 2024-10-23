using Megift.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Megift.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly MeGiftContext _context;

        public ReviewController(MeGiftContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {
            var reviews = await _context.Reviews.Include(r=>r.Customer).Select(review => new
            {
                Image = review.Customer.Image,
                Name = review.Customer.Name,
                Review = review.Description,
            }).ToListAsync();
            return Ok(reviews);
        }
    }
}