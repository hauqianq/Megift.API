using Megift.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Megift.Service.Services
{
    public class ProductService
    {
        private readonly MeGiftContext _context;

        public ProductService(MeGiftContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }
    }
}
