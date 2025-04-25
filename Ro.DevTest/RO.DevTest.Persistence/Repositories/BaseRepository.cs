using Microsoft.EntityFrameworkCore;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using System.Linq.Expressions;
using RO.DevTest.Application.Common;

namespace RO.DevTest.Persistence.Repositories;

public class BaseRepository<T>(DefaultContext defaultContext) : IBaseRepository<T> where T : class {
    private readonly DefaultContext _defaultContext = defaultContext;

    protected DefaultContext Context { get => _defaultContext; }

    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default) {
        await Context.Set<T>().AddAsync(entity, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default) {
        Context.Set<T>().Update(entity);
        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default) {
        Context.Set<T>().Remove(entity);
        await Context.SaveChangesAsync(cancellationToken);
    }

    protected async Task<PagedResult<T>> GetPagedAsync(
        Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    ) {
        IQueryable<T> baseQuery = GetWhereQuery(predicate);

        if(orderBy is not null) {
            baseQuery = orderBy(baseQuery);
        }

        int totalCount = await baseQuery.CountAsync(cancellationToken);
        List<T> items = await baseQuery
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<T>(totalCount, pageSize, pageNumber, items);
    }

    public T? Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
    => GetQueryWithIncludes(predicate, includes).FirstOrDefault();

    /// <summary>
    /// Generates a filtered <see cref="IQueryable{T}"/>, based on its
    /// <paramref name="predicate"/> and <paramref name="includes"/>, including
    /// the data requested
    /// </summary>
    /// <param name="predicate">
    /// The <see cref="Expression"/> to use as filter
    /// </param>
    /// <param name="includes">
    /// The <see cref="Expression"/> to use as include
    /// </param>
    /// <returns>
    /// The generated <see cref="IQueryable{T}"/>
    /// </returns>
    private IQueryable<T> GetQueryWithIncludes(
        Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[] includes
    ) {
        IQueryable<T> baseQuery = GetWhereQuery(predicate);

        foreach(Expression<Func<T, object>> include in includes) {
            baseQuery = baseQuery.Include(include);
        }

        return baseQuery;
    }

    /// <summary>
    /// Generates an <see cref="IQueryable"/> based on
    /// the <paramref name="predicate"/>
    /// </summary>
    /// <param name="predicate">
    /// An <see cref="Expression"/> representing a filter
    /// of it
    /// </param>
    /// <returns>S
    /// The <see cref="IQueryable{T}"/>
    /// </returns>
    private IQueryable<T> GetWhereQuery(Expression<Func<T, bool>> predicate) {
        IQueryable<T> baseQuery = Context.Set<T>();

        if(predicate is not null) {
            baseQuery = baseQuery.Where(predicate);
        }

        return baseQuery;
    }

}
