using CVU.CONDICA.Application.Services.Mapping;
using CVU.CONDICA.Application.Services.Pagination;
using CVU.CONDICA.Dto.Pagination;
using CVU.CONDICA.Dto.RequestModels;
using CVU.CONDICA.Dto.UserManagement;
using MediatR;

namespace CVU.CONDICA.Application.Account.Queries
{
    public class UserListQuery : IRequest<PaginatedModel<UserShortDto>>
    {
        public UserListQuery(UserListQueryModel model)
        {
            QueryModel = model;
        }

        public UserListQueryModel QueryModel { get; set; }
    }

    public class UserListQueryHandler : RequestHandler<UserListQuery, PaginatedModel<UserShortDto>>
    {
        private readonly IPaginationService PaginationService;
        public UserListQueryHandler(IServiceProvider serviceProvider, IPaginationService paginationService) : base(serviceProvider)
        {
            PaginationService = paginationService;
        }

        public override async Task<PaginatedModel<UserShortDto>> Handle(UserListQuery request, CancellationToken cancellationToken)
        {
            var queryModel = request.QueryModel;

            var users = AppDbContext.User.AsQueryable();

            if (!string.IsNullOrEmpty(queryModel.EmailAddress))
            {
                users = users.Where(d => d.Email.Contains(queryModel.EmailAddress));
            }

            if (!string.IsNullOrEmpty(queryModel.FirstName))
            {
                users = users.Where(d => d.FirstName.Contains(queryModel.FirstName));
            }

            if (!string.IsNullOrEmpty(queryModel.LastName))
            {
                users = users.Where(d => d.LastName.Contains(queryModel.LastName));
            }

            users = users.OrderByDescending(d => d.Id);

            var paginatedResult = PaginationService.PaginatedResults(users, queryModel, Mapping.UserShortProjection);

            return paginatedResult;
        }
    }
}
