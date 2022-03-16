namespace Infra.Data.Queryables;

public static class PaginatedQuery
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> query,
        int page, int countPerPage) where T : class
    {        
        countPerPage = Math.Min(countPerPage, 100);
        return query.Skip((page -1) * countPerPage).Take(countPerPage);
    }
}