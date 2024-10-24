namespace Megift.API.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public string Slug { get; set; }

    public string Sku { get; set; }

    public string Description { get; set; }

    public string Colors { get; set; }

    public string Size { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
}