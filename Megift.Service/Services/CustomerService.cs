using Megift.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Megift.Service.Services
{
    public class CustomerService
    {
        private readonly MeGiftContext _context;

        public CustomerService(MeGiftContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
        }
    }
}
