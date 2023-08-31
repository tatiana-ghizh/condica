using CVU.CONDICA.Application.Services.Mapping;
using CVU.CONDICA.Dto.UserManagement;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVU.CONDICA.Application.Account.Queries
{
    public class UserQuery : IRequest<UserDto>
    {
        public UserQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }

    public class UserQueryHandler : RequestHandler<UserQuery, UserDto>
    {
        public UserQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override async Task<UserDto> Handle(UserQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
            {
                request.Id = CurrentUser.Id;
            }

            var administrationUser = AppDbContext.User
                .Include(d => d.Position)
                .First(d => d.Id == request.Id);

            var mappedUser = Mapping.UserProjection.Compile().Invoke(administrationUser);

            //return await AppDbContext.Vacations.Where(v => v.Id == request.Id).Select(Mapping.VacationProjection).FirstOrDefaultAsync();

            return mappedUser;
        }
    }
}
