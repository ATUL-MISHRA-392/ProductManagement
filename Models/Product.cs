using System.ComponentModel.DataAnnotations;

namespace ProductManagementSystem.Models;

public class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Product name is required")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Range(0.01, 10000000)]
    public decimal Price { get; set; }

    [Required]
    [StringLength(50)]
    public string Category { get; set; } = string.Empty;

    [Range(0, 1000000)]
    public int Stock { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    public string? ImagePath { get; set; }
}