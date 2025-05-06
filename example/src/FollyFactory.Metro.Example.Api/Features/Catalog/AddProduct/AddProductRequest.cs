using System.ComponentModel.DataAnnotations;

namespace FollyFactory.Metro.Example.Api.Features.Catalog.AddProduct;

/// <summary>
/// Represents a request to add a new product to the catalog.
/// </summary>
public record AddProductRequest
{
    /// <summary>
    /// SKU (Stock Keeping Unit) of the product.
    /// </summary>
    [Required]
    [StringLength(20, ErrorMessage = "SKU must not exceed 20 characters.")]
    public required string Sku { get; set; }

    /// <summary>
    /// The name of the product.
    /// </summary>
    [Required]
    [StringLength(100, ErrorMessage = "Name must not exceed 100 characters.")]
    public required string Name { get; set; }

    /// <summary>
    /// The description of the product.
    /// </summary>
    [StringLength(500, ErrorMessage = "Description must not exceed 500 characters.")]
    public string? Description { get; set; }
}
