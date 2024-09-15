namespace Domain.Abstractions.Queries.Paging;

public abstract class PagedBase
{
    public int CurrentPage { get; set; }
    public int ResultsPerPage { get; set; }
    public int TotalPages { get; set; }
    public long TotalResults { get; set; }

    public bool HasPreviousPage { get; set; }

    public bool HasNextPage { get; set; }

    protected PagedBase()
    {
    }

    protected PagedBase
    (
        int currentPage,
        int resultsPerPage,
        int totalPages,
        long totalResults,
        bool hasPreviousPage,
        bool hasNextPage
    )
    {
        CurrentPage = currentPage;
        ResultsPerPage = resultsPerPage;
        TotalPages = totalPages;
        TotalResults = totalResults;
        HasPreviousPage = hasPreviousPage;
        HasNextPage = hasNextPage;
    }
}