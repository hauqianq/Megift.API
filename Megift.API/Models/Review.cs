namespace Megift.API.Models;

public partial class Review
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string Description { get; set; }

    public virtual Customer Customer { get; set; }
}