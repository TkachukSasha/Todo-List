namespace Domain.Abstractions.Queries.Paging;

public abstract class PagedQuery : IPagedQuery
{
    public int Page { get; set; }
    public int Results { get; set; }
}

public abstract class PagedQuery<T> : PagedQuery, IPagedQuery<Paged<T>>
{
}