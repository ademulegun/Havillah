using System.ComponentModel.DataAnnotations;

namespace Havillah.Shared.Category;

public class ProductCategory
{
    [Required(ErrorMessage = "Category name must be provided")]
    public string Name { get; set; }
}