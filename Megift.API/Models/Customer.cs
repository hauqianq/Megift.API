namespace Megift.API.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Image { get; set; }

    public string Email { get; set; }

    public string ShippingAddress { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}