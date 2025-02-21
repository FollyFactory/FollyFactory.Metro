namespace FollyFactory.Metro.Example.Application.Features.Catalog.Views;

public record ProductSearchView
{
    public Guid ProductId { get; set; }

    public string Sku { get; set; }

    public string Name { get; set; }
}