using CVU.CONDICA.Application.Services.Mapping;
using CVU.CONDICA.Dto.Vacations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVU.CONDICA.Application.Vacations.Queries
{
    public class VacationQuery : IRequest<VacationDto>
    {
        public VacationQuery(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }

    public class VacationQueryHandler : RequestHandler<VacationQuery, VacationDto>
    {
        public VacationQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async override Task<VacationDto> Handle(VacationQuery request, CancellationToken cancellationToken)
        {
            return await AppDbContext.Vacations.Where(v => v.Id == request.Id).Select(Mapping.VacationProjection).FirstOrDefaultAsync();
        }
    }
}
