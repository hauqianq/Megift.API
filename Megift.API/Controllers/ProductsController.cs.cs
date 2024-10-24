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
                Image = product.ProductImages.FirstOrDefault(pi=>pi.ProductId == product.Id).ImagePath,
                Name = product.Name,
                Sku = product.Sku,
                Price = product.Price
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

        private async Task<List<ProductImage>> AddListImages(List<IFormFile> request)
        {
            List<ProductImage> images = new List<ProductImage>();
            foreach (var image in request)
            {
                var img = new ProductImage()
                {
                    ImagePath = await UploadProductImage(image),
                };
                images.Add(img);
            }
            return images;
        }


        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Products.Add(new Product()
            {
                Colors = request.Colors,
                Description = request.Description,
                Name = request.Name,
                Price = request.Price,
                Size = request.Size,
                Sku = request.Sku,
                Slug = request.Slug,
                ProductImages = await AddListImages(request.Images)
            });
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}

