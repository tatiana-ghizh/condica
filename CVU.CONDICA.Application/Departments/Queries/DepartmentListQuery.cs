using CVU.CONDICA.Application.Services.Mapping;
using CVU.CONDICA.Application.Services.Pagination;
using CVU.CONDICA.Dto.Pagination;
using CVU.CONDICA.Dto.Departments;
using MediatR;

namespace CVU.CONDICA.Application.Departments.Queries
{
    public class DepartmentListQuery : IRequest<PaginatedModel<DepartmentDto>>
    {
        public DepartmentListQuery(DepartmentListQueryDto queryModel)
        {
            DepartmentList = queryModel;
        }

        public DepartmentListQueryDto DepartmentList { get; set; }
    }

    public class DepartmentListQueryHandler : RequestHandler<DepartmentListQuery, PaginatedModel<DepartmentDto>>
    {
        private readonly IPaginationService _paginationService;

        public DepartmentListQueryHandler(IServiceProvider serviceProvider, IPaginationService paginationService) : base(serviceProvider)
        {
            _paginationService = paginationService;
        }

        public async override Task<PaginatedModel<DepartmentDto>> Handle(DepartmentListQuery request, CancellationToken cancellationToken)
        {
            var query = AppDbContext.Departments.AsQueryable();

            var paginatedModel = _paginationService.PaginatedResults(query, request.DepartmentList, Mapping.DepartmentProjection);

            return paginatedModel;
        }
    }
}
