using CVU.CONDICA.Application.Services.Mapping;
using CVU.CONDICA.Dto.Departments;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVU.CONDICA.Application.Departments.Queries
{
    public class DepartmentQuery : IRequest<DepartmentDto>
    {
        public DepartmentQuery(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }

    public class PositionQueryHandler : RequestHandler<DepartmentQuery, DepartmentDto>
    {
        public PositionQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async override Task<DepartmentDto> Handle(DepartmentQuery request, CancellationToken cancellationToken)
        {
            return await AppDbContext.Departments.Where(p => p.Id == request.Id).Select(Mapping.DepartmentProjection).FirstOrDefaultAsync();
        }
    }
}
