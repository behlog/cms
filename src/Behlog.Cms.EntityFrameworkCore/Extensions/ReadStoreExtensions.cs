using System.Linq.Expressions;
using System.Reflection;

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
    
    
    public static IQueryable<T> SortBy<T>(
        this IQueryable<T> source, string sortKey, bool desc = false)
    {
        var type = typeof(T);
        var parameter = Expression.Parameter(type, "p");
        PropertyInfo property;
        Expression propertyAccess;
        if (sortKey.Contains('.'))
        {
            // support to be sorted on child fields.
            String[] childProperties = sortKey.Split('.');
            property = type.GetProperty(childProperties[0]);
            propertyAccess = Expression.MakeMemberAccess(parameter, property);
            for (int i = 1; i < childProperties.Length; i++)
            {
                property = property.PropertyType.GetProperty(childProperties[i]);
                propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
            }
        }
        else
        {
            property = typeof(T).GetProperty(sortKey);
            propertyAccess = Expression.MakeMemberAccess(parameter, property);
        }
        var orderByExp = Expression.Lambda(propertyAccess, parameter);
        MethodCallExpression resultExp = Expression.Call(typeof(Queryable),
            desc ? "OrderByDescending" : "OrderBy",
            new[] { type, property.PropertyType }, source.Expression,
            Expression.Quote(orderByExp));
        //return  source.OrderBy(x => orderByExp);
        return source.Provider.CreateQuery<T>(resultExp);
    }
    
}