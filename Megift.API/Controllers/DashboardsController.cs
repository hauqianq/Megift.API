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

        [HttpGet("customer")]
        public async Task<IActionResult> GetCustomerSummary()
        {
            try
            {
                int totalUsers = await _context.Users.CountAsync();

                DateTime today = DateTime.UtcNow.Date;
                DateTime lastWeek = today.AddDays(-7);

                int lastWeekLogins = await _context.Users
                    .Where(u => u.LastLogin.HasValue && u.LastLogin >= lastWeek && u.LastLogin < today)
                    .CountAsync();

                int currentDayLogins = await _context.Users
                    .Where(u => u.LastLogin.HasValue && u.LastLogin >= today)
                    .CountAsync();

                var response = new
                {
                    TotalUsers = totalUsers,
                    LastWeekLogins = lastWeekLogins,
                    CurrentDayLogins = currentDayLogins
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("best-selling")]
        public async Task<IActionResult> GetBestSellingProducts()
        {
            try
            {
                var bestSellingProducts = await _context.OrderDetails
                    .Include(od => od.Product)
                    .Where(od => od.ProductId != null && od.Quantity != null)
                    .GroupBy(od => od.Product)
                    .Select(group => new
                    {
                        name = group.Key.ProductName,
                        sales = group.Sum(od => od.Quantity) ?? 0
                    })
                    .OrderByDescending(product => product.sales)
                    .ToListAsync();

                return Ok(bestSellingProducts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("payment-methods")]
        public async Task<IActionResult> GetPaymentMethodsSummary()
        {
            try
            {
                var paymentMethodsData = await _context.Orders
                    .GroupBy(o => o.OrderType)
                    .Select(group => new
                    {
                        method = ((OrderType)group.Key).ToString(),
                        count = group.Count()
                    })
                    .ToListAsync();

                return Ok(paymentMethodsData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("total-sales-last-week")]
        public async Task<IActionResult> GetTotalSalesLastWeek()
        {
            try
            {
                DateTime today = DateTime.UtcNow.Date;
                DateTime oneWeekAgo = today.AddDays(-6);

                var salesData = await _context.Orders
                    .Where(o => o.Status == "Completed" && o.OrderDate >= oneWeekAgo && o.OrderDate <= today)
                    .GroupBy(o => o.OrderDate.Value.Date)
                    .Select(group => new
                    {
                        name = group.Key.ToString("dd"),
                        value = group.Sum(o => o.TotalAmount)
                    })
                    .OrderBy(result => result.name)
                    .ToListAsync();

                return Ok(salesData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("monthly-orders")]
        public async Task<IActionResult> GetMonthlyOrderGoal()
        {
            try
            {
                DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime today = DateTime.UtcNow;

                int totalOrders = await _context.Orders.CountAsync();

                int monthlyGoal = 50; // Ví dụ mục tiêu cố định cho số lượng đơn hàng hàng tháng

                return Ok(new
                {
                    totalOrders = totalOrders,
                    monthlyGoal = monthlyGoal
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("recent-orders")]
        public async Task<IActionResult> GetRecentOrders()
        {
            try
            {
                var recentOrders = await _context.Orders.Include(order => order.OrderDetails).ThenInclude(od => od.Product)
                    .OrderByDescending(o => o.OrderDate)
                    .Take(5)
                    .Select(o => new
                    {
                        item = o.OrderDetails.FirstOrDefault().Product.ProductName,
                        date = o.OrderDate.Value.ToString("dd MMM, yyyy"),
                        total = o.TotalAmount,
                        status = o.Status
                    })
                    .ToListAsync();

                return Ok(recentOrders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

