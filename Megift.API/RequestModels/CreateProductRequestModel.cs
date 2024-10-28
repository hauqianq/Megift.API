using Megift.API.Models;

namespace Megift.API.RequestModels
{
    public class CreateProductRequestModel
    {
        public string ProductName { get; set; }

        public int CategoryId { get; set; }

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public string Description { get; set; }

        public IFormFile ImageUrl { get; set; }
    }
}
