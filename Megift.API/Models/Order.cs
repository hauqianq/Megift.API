namespace Megift.API.Models;

public partial class Order
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int CustomerId { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal Total { get; set; }

    public string Status { get; set; }

    public virtual Customer Customer { get; set; }

    public virtual Product Product { get; set; }
}