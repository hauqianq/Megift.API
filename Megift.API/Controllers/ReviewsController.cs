using Megift.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Megift.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly MeGiftContext _context;

        public ReviewsController(MeGiftContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetReviews()
        {
            var reviews = await _context.Reviews.Include(r=>r.Customer)
                                                .Include(r => r.Product)        
                                                .Select(review => new
                                                {
                                                    ReviewId = review.ReviewId,
                                                    Product = review.Product.ProductName,
                                                    Rating = review.Rating,
                                                    Comment = review.Comment,
                                                    ReviewDate = review.ReviewDate,
                                                    Customer = review.Customer.FirstName + " " + review.Customer.LastName,
                                                })
                                                .ToListAsync();
            return Ok(reviews);
        }
    }
}
