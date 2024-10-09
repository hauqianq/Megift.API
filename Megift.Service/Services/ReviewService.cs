using Megift.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Megift.Service.Services
{
    public class ReviewService
    {
        private readonly MeGiftContext _context;

        public ReviewService(MeGiftContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Review>> GetAllReviewsAsync()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<Review> GetReviewByIdAsync(int id)
        {
            return await _context.Reviews.FindAsync(id);
        }
    }
}
