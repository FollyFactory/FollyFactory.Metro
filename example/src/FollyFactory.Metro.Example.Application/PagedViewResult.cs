namespace FollyFactory.Metro.Example.Application;

public record PagedViewResult<TView>(ICollection<TView> Items, int TotalItems, int Page, int PageSize);
