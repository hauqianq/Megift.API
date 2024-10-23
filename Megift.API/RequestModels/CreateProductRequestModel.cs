namespace Megift.API.RequestModels
{
    public class CreateProductRequestModel
    {
        public string Name { get; set; }
        
        public IFormFile ImageUrl { get; set; }

        public string Sku { get; set; }

        public decimal Price { get; set; }

        public string Stock { get; set; }   
    }
}
