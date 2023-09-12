using CVU.CONDICA.Application.Services.Mapping;
using CVU.CONDICA.Dto.DepartmentRoles;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVU.CONDICA.Application.DepartmentRoles.Queries
{
    public class DepartmentRoleQuery : IRequest<DepartmentRoleDto>
    {
        public DepartmentRoleQuery(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }

    public class DepartmentRoleQueryHandler : RequestHandler<DepartmentRoleQuery, DepartmentRoleDto>
    {
        public DepartmentRoleQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async override Task<DepartmentRoleDto> Handle(DepartmentRoleQuery request, CancellationToken cancellationToken)
        {
            return await AppDbContext.DepartmentRoles.Where(p => p.Id == request.Id).Select(Mapping.DepartmentRoleProjection).FirstOrDefaultAsync();

        }
    }
}
