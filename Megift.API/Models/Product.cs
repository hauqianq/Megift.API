namespace Megift.API.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Image { get; set; }

    public string Name { get; set; }

    public string Sku { get; set; }

    public decimal Price { get; set; }

    public string Stock { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}