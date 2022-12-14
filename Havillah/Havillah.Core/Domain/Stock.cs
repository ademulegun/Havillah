namespace Havillah.Core.Domain;

public class Stock: BaseEntity<Guid>
{
    public Stock() { }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public DateTime Created { get; set; }
    public decimal Price { get; set; }
}