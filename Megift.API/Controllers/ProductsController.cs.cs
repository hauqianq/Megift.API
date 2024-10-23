using Firebase.Storage;
using Megift.API.Models;
using Megift.API.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Megift.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly MeGiftContext _context;
        private readonly IConfiguration _configuration;

        public ProductController(MeGiftContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _context.Products.Select(product => new
            {
                Id = product.Id,
                Image = product.Image,
                Name = product.Name,
                Sku = product.Sku,
                Price = product.Price,
                Stock = product.Stock,
            }).ToListAsync();
            return Ok(products);
        }

        private async Task<string> UploadProductImage(IFormFile image)
        {
            string firebaseBucket = _configuration["Firebase:StorageBucket"];

            var firebaseStorage = new FirebaseStorage(firebaseBucket);

            var task = firebaseStorage.Child("Products");

            var stream = image.OpenReadStream();
            await task.PutAsync(stream);

            return await task.GetDownloadUrlAsync();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = new Product
            {
                Name = model.Name,
                Image = await UploadProductImage(model.ImageUrl),
                Sku = model.Sku,
                Price = model.Price,
                Stock = model.Stock
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }
    }
}

