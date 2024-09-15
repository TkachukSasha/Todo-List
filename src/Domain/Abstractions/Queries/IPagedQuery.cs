namespace Domain.Abstractions.Queries;

public interface IPagedQuery : IQuery
{
    int Page { get; set; }
    int Results { get; set; }
}

public interface IPagedQuery<TResult> : IPagedQuery, IQuery<TResult>
{
}