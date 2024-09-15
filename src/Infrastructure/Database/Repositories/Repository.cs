using System.Linq.Expressions;

using Domain.Abstractions;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public abstract class Repository<TEntity>(TodoListContext todoListContext)
    where TEntity : BaseEntity
{
    protected readonly TodoListContext _todoListContext = todoListContext;

    public async Task<TEntity?> GetByPredicateAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _todoListContext
            .Set<TEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _todoListContext
            .Set<TEntity>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public IQueryable<TEntity> GetAll()
    {
        return _todoListContext
            .Set<TEntity>();
    }

    public async Task<List<dynamic>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, dynamic>> selector, CancellationToken cancellationToken = default)
    {
        if(selector is null)
        {
            return await _todoListContext
                .Set<TEntity>()
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync(cancellationToken) as dynamic;
        }

        return await _todoListContext
                .Set<TEntity>()
                .AsNoTracking()
                .Where(predicate)
                .Select(selector)
                .ToListAsync(cancellationToken);
    }

    public void Add(TEntity entity)
    {
        _todoListContext
            .Set<TEntity>()
            .Add(entity);
    }

    public void Remove(TEntity entity)
    {
        _todoListContext
            .Set<TEntity>()
            .Remove(entity);
    }
}
