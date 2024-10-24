namespace Megift.API.RequestModels
{
    public class CreateProductRequestModel
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Slug { get; set; }

        public string Sku { get; set; }

        public string Description { get; set; }

        public string Colors { get; set; }

        public string Size { get; set; }

        public List<IFormFile> Images { get; set; }
    }
}
