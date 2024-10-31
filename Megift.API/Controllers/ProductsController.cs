using Firebase.Storage;
using Megift.API.Models;
using Megift.API.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Megift.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly MeGiftContext _context;
        private readonly IConfiguration _configuration;

        public ProductsController(MeGiftContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _context.Products.Include(p => p.Category).Select(product => new
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Category = product.Category.CategoryName,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                Description = product.Description,
                ImageUrl = product.ImageUrl
            }).ToListAsync();
            return Ok(products);
        }

        private async Task<string> UploadProductImage(IFormFile image)
        {
            string firebaseBucket = _configuration["FirebaseConfiguration:StorageBucket"];

            var firebaseStorage = new FirebaseStorage(firebaseBucket);

            var task = firebaseStorage.Child("Products");

            var stream = image.OpenReadStream();
            await task.PutAsync(stream);

            return await task.GetDownloadUrlAsync();
        }


        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = new Product
            {
                ProductName = request.ProductName,
                CategoryId = request.CategoryId,
                Price = request.Price,
                StockQuantity = request.StockQuantity,
                Description = request.Description,
                ImageUrl = await UploadProductImage(request.ImageUrl)
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }
    }
}
