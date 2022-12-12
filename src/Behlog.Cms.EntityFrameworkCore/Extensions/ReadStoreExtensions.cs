using System.Linq.Expressions;

namespace Behlog.Cms.EntityFrameworkCore.Extensions;

public static class ReadStoreExtensions
{

    public static IQueryable<TSource> AddConditionIfNotNull<TSource>(
        this IQueryable<TSource> query, object? obj, Expression<Func<TSource, bool>> predicate)
    {
        if (obj is null) return query;

        return query.Where(predicate);
    }
    
    
    public static IQueryable<TSource> AddConditionIfNotNull<TSource>(
        this IQueryable<TSource> query, string? str, Expression<Func<TSource, bool>> predicate)
    {
        if (string.IsNullOrWhiteSpace(str)) return query;

        return query.Where(predicate);
    }
    
}