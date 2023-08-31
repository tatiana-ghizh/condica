using CVU.CONDICA.Dto.Pagination;
using System.Linq.Expressions;

namespace CVU.CONDICA.Application.Services.Pagination
{
    public class PaginationService : IPaginationService
    {
        public PaginatedModel<T> PaginatedResults<T>(IQueryable<T> query, PaginatedQueryParameter pageMetadata)
        {
            var count = query.Count();
            var items = query.Skip((pageMetadata.Page - 1) * pageMetadata.ItemsPerPage).Take(pageMetadata.ItemsPerPage).ToList();

            var pageSummary = new PaginatedHeaderParameter
            {
                TotalCount = count,
                PageSize = pageMetadata.ItemsPerPage,
                CurrentPage = pageMetadata.Page
            };

            return new PaginatedModel<T>(items, pageSummary);

        }

        public PaginatedModel<TDestination> PaginatedResults<TSource, TDestination>(IQueryable<TSource> query, PaginatedQueryParameter pagedQuery, Expression<Func<TSource, TDestination>> MappingRule)
        {
            var count = query.Count();

            var items = query.Skip((pagedQuery.Page - 1) * pagedQuery.ItemsPerPage).Take(pagedQuery.ItemsPerPage).Select(MappingRule).ToList();

            var pageSummary = new PaginatedHeaderParameter
            {
                TotalCount = count,
                PageSize = pagedQuery.ItemsPerPage,
                CurrentPage = pagedQuery.Page
            };

            return new PaginatedModel<TDestination>(items, pageSummary);
        }
    }
}
