using Megift.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Megift.Service.Services
{
    public class OrderService
    {
        private readonly MeGiftContext _context;

        public OrderService(MeGiftContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }
    }
}
