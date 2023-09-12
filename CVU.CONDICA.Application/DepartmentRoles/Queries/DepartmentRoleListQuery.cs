using CVU.CONDICA.Application.Services.Mapping;
using CVU.CONDICA.Application.Services.Pagination;
using CVU.CONDICA.Dto.DepartmentRoles;
using CVU.CONDICA.Dto.Pagination;
using MediatR;

namespace CVU.CONDICA.Application.DepartmentRoles.Queries
{
    public class DepartmentRoleListQuery : IRequest<PaginatedModel<DepartmentRoleDto>>
    {
        public DepartmentRoleListQuery(DepartmentRoleListQueryDto queryModel) 
        { 
            DepartmentRoleList = queryModel;
        }

        public DepartmentRoleListQueryDto DepartmentRoleList { get;set; }
    }

    public class DepartmentRoleListQueryHandler : RequestHandler<DepartmentRoleListQuery, PaginatedModel<DepartmentRoleDto>>
    {
        private readonly IPaginationService _paginationService;

        public DepartmentRoleListQueryHandler(IServiceProvider serviceProvider, IPaginationService paginationService) : base(serviceProvider)
        {
            _paginationService = paginationService;
        }

        public async override Task<PaginatedModel<DepartmentRoleDto>> Handle(DepartmentRoleListQuery request, CancellationToken cancellationToken)
        {
            var query = AppDbContext.DepartmentRoles.AsQueryable();

            var paginatedModel = _paginationService.PaginatedResults(query, request.DepartmentRoleList, Mapping.DepartmentRoleProjection);

            return paginatedModel;
        }
    }
}
