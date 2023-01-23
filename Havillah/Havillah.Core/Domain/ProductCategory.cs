namespace Havillah.Core.Domain;

public class ProductCategory: BaseEntity<int>
{
    public string Name { get; set; }

    public ProductCategory(string name)
    {
        this.Name = name;
        this.DateAdded = DateTime.UtcNow;
    }
}