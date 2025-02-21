namespace FollyFactory.Metro.Example.Application.Features.Catalog.Views;

public record ProductDetailsView
{
    public Guid ProductId { get; set; }

    public string Sku { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
}