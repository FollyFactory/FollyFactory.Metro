using System.ComponentModel.DataAnnotations;

namespace FollyFactory.Metro.Example.Api.Features.Catalog.AddProduct;

public record AddProductRequest
{
    [Required]
    [StringLength(20)]
    public string Sku { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(500)]
    public string Description { get; set; }
}