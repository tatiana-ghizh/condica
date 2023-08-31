using CVU.CONDICA.Application.Services.Mapping;
using CVU.CONDICA.Application.Services.Pagination;
using CVU.CONDICA.Dto.CompanyProjects;
using CVU.CONDICA.Dto.Pagination;
using MediatR;

namespace CVU.CONDICA.Application.CompanyProjects.Queries
{
    public class CompanyProjectListQuery : IRequest<PaginatedModel<CompanyProjectDto>>
    {
        public CompanyProjectListQuery(CompanyProjectListQueryDto queryModel) 
        {
            CompanyProjectList = queryModel;
        }

        public CompanyProjectListQueryDto CompanyProjectList { get; set; }
    }

    public class CompanyProjectListQueryHandler : RequestHandler<CompanyProjectListQuery, PaginatedModel<CompanyProjectDto>>
    {
        private readonly IPaginationService _paginationService;

        public CompanyProjectListQueryHandler(IServiceProvider serviceProvider, IPaginationService paginationService) : base(serviceProvider)
        {
            _paginationService = paginationService;
        }

        public async override Task<PaginatedModel<CompanyProjectDto>> Handle(CompanyProjectListQuery request, CancellationToken cancellationToken)
        {
            var query = AppDbContext.CompanyProjects.AsQueryable();

            var paginatedModel = _paginationService.PaginatedResults(query, request.CompanyProjectList, Mapping.CompanyProjectProjection);

            return paginatedModel;
        }
    }
}
