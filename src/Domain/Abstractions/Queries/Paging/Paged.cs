namespace Domain.Abstractions.Queries.Paging;

public class Paged<T> : PagedBase
{
    public IReadOnlyList<T> Items { get; set; } = Array.Empty<T>();

    public bool Empty => Items is null || !Items.Any();

    public Paged()
    {
        CurrentPage = 1;
        TotalPages = 1;
        ResultsPerPage = 10;
    }

    public Paged
    (
        IReadOnlyList<T> items,
        int currentPage,
        int resultsPerPage,
        int totalPages,
        long totalResults,
        bool hasPreviousPage,
        bool hasNextPage) :
        base(currentPage, resultsPerPage, totalPages, totalResults, hasPreviousPage, hasNextPage)
    {
        Items = items;
    }

    public static Paged<T> Create
   (
        IReadOnlyList<T> items,
        int currentPage,
        int resultsPerPage,
        int totalPages,
        long totalResults,
        bool hasPreviousPage,
        bool hasNextPage
    ) => new Paged<T>(items, currentPage, resultsPerPage, totalPages, totalResults, hasPreviousPage, hasNextPage);

    public static Paged<T> From(PagedBase result, IReadOnlyList<T> items)
        => new Paged<T>(items, result.CurrentPage, result.ResultsPerPage,
            result.TotalPages, result.TotalResults, result.HasPreviousPage, result.HasNextPage);

    public static Paged<T> AsEmpty => new Paged<T>();

    public Paged<TResult> Map<TResult>(Func<T, TResult> map)
        => Paged<TResult>.From(this, Items.Select(map).ToList());
}