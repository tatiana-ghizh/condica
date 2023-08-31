using CVU.CONDICA.Application.Services.Mapping;
using CVU.CONDICA.Application.Services.Pagination;
using CVU.CONDICA.Dto.Pagination;
using CVU.CONDICA.Dto.Vacations;
using MediatR;

namespace CVU.CONDICA.Application.Vacations.Queries
{
    public class MyVacationListQuery : IRequest<PaginatedModel<VacationDto>>
    {
        public MyVacationListQuery(VacationListQueryDto queryModel)
        {
            VacationList = queryModel;
        }

        public VacationListQueryDto VacationList { get; set; }
    }

    public class MyVacationListQueryHandler : RequestHandler<MyVacationListQuery, PaginatedModel<VacationDto>>
    {
        private readonly IPaginationService _paginationService;

        public MyVacationListQueryHandler(IServiceProvider serviceProvider, IPaginationService pagination) : base(serviceProvider)
        {
            _paginationService = pagination;
        }

        public async override Task<PaginatedModel<VacationDto>> Handle(MyVacationListQuery request, CancellationToken cancellationToken)
        {
            var query = AppDbContext.Vacations.Where(u => u.UserId == CurrentUser.Id).AsQueryable();

            if (request.VacationList.Statuses != null && request.VacationList.Statuses.Any())
            {
                query = query.Where(d => request.VacationList.Statuses.Contains(d.Status));
            }

            if (request.VacationList.Types != null && request.VacationList.Types.Any())
            {
                query = query.Where(d => request.VacationList.Types.Contains(d.Type));
            }

            if (request.VacationList.FromDate.HasValue)
            {
                query = query.Where(d => d.FromDate >= request.VacationList.FromDate);
            }

            if (request.VacationList.ToDate.HasValue)
            {
                query = query.Where(d => d.ToDate >= request.VacationList.ToDate);
            }

            if (request.VacationList.RequestedAt.HasValue)
            {
                query = query.Where(d => d.RequestedAt >= request.VacationList.RequestedAt);
            }

            if (!string.IsNullOrEmpty(request.VacationList.UserName))
            {
                query = query.Where(d => (d.User.FirstName.Contains(request.VacationList.UserName) || d.User.LastName.Contains(request.VacationList.UserName)));
            }

            var paginatedModel = _paginationService.PaginatedResults(query, request.VacationList, Mapping.VacationProjection);

            return paginatedModel;
        }
    }

}
