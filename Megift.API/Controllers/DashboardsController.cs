using Megift.API.Enumerations;
using Megift.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Megift.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardsController : ControllerBase
    {
        private readonly MeGiftContext _context;

        public DashboardsController(MeGiftContext context)
        {
            _context = context;
        }

        [HttpGet("customer-stats")]
        public IActionResult GetCustomerStats()
        {
            var totalCustomers = _context.Customers.Count();
            var data = _context.Orders
                .GroupBy(o => o.OrderDate.Value.Day)
                .Select(g => new { day = g.Key.ToString("D2"), value = g.Count() })
                .ToList();

            var result = new
            {
                totalCustomers,
                data
            };

            return Ok(result);
        }

        [HttpGet("payment-method-summary")]
        public async Task<IActionResult> GetPaymentMethodSummary()
        {
            try
            {
                // Truy vấn dữ liệu từ cơ sở dữ liệu trước, không sử dụng Enum.GetName trong LINQ query
                var summaryData = await _context.Orders
                    .Where(o => o.OrderType.HasValue)
                    .GroupBy(o => o.OrderType.Value)
                    .Select(g => new
                    {
                        OrderType = g.Key, // Lưu enum dưới dạng số nguyên
                        Count = g.Count()
                    })
                    .ToListAsync();

                // Tính tổng số lượng bán hàng
                var totalSales = await _context.Orders.SumAsync(o => o.TotalAmount) ?? 0;

                // Sau khi dữ liệu đã được truy vấn, chuyển đổi OrderType từ enum sang chuỗi
                var summary = summaryData.Select(item => new
                {
                    Method = Enum.GetName(typeof(OrderType), item.OrderType), // Chuyển enum sang chuỗi sau khi truy vấn
                    Count = item.Count
                }).ToList();

                // Tạo cấu trúc JSON trả về
                var response = new
                {
                    TotalSales = totalSales,
                    Data = summary
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving data: " + ex.Message);
            }
        }
    }
}

