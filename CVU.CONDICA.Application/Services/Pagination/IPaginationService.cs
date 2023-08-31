using CVU.CONDICA.Dto.Pagination;
using System.Linq.Expressions;

namespace CVU.CONDICA.Application.Services.Pagination
{
    public interface IPaginationService
    {
        PaginatedModel<T> PaginatedResults<T>(IQueryable<T> list, PaginatedQueryParameter query);
        PaginatedModel<TDestination> PaginatedResults<TSource, TDestination>(IQueryable<TSource> list, PaginatedQueryParameter query, Expression<Func<TSource, TDestination>> MappingRule);
    }
}
