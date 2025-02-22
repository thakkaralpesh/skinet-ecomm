using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class SpecificationEvaluator<T> where T : BaseEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
    {
        if(spec.Criteria != null)
        {
            inputQuery = inputQuery.Where(spec.Criteria); //where query x=>brand == brand
        }

        if(spec.OrderBy != null)
        {
            inputQuery = inputQuery.OrderBy(spec.OrderBy);
        }

        if(spec.OrderByDescending != null)
        {
            inputQuery = inputQuery.OrderByDescending(spec.OrderByDescending);
        }

        if(spec.IsDistinct)
        {
            inputQuery = inputQuery.Distinct();
        }

        if(spec.IsPagingEnabled)
        {
            inputQuery = inputQuery.Skip(spec.Skip).Take(spec.Take);
        }

        inputQuery = spec.Includes.Aggregate(inputQuery,(current,include)=>current.Include(include));
        inputQuery = spec.IncludeStrings.Aggregate(inputQuery,(current,include)=>current.Include(include));

        return inputQuery;
    }

     public static IQueryable<TResult> GetQuery<TSpec,TResult>(IQueryable<T> inputQuery, 
                    ISpecification<T,TResult> spec)
    {
        if(spec.Criteria != null)
        {
            inputQuery = inputQuery.Where(spec.Criteria); //where query x=>brand == brand
        }

        if(spec.OrderBy != null)
        {
            inputQuery = inputQuery.OrderBy(spec.OrderBy);
        }

        if(spec.OrderByDescending != null)
        {
            inputQuery = inputQuery.OrderByDescending(spec.OrderByDescending);
        }

        var selectQuery = inputQuery as IQueryable<TResult>;

        if(spec.Selector != null)
        {
            selectQuery = inputQuery.Select(spec.Selector);
        }

        if(spec.IsDistinct)
        {
            selectQuery = selectQuery?.Distinct();
        }

        if(spec.IsPagingEnabled)
        {
            selectQuery = selectQuery?.Skip(spec.Skip).Take(spec.Take);
        }

        return selectQuery ?? inputQuery.Cast<TResult>();
    }
}
