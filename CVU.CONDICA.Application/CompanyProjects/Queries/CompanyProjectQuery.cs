using CVU.CONDICA.Application.Services.Mapping;
using CVU.CONDICA.Dto.CompanyProjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVU.CONDICA.Application.CompanyProjects.Queries
{
    public class CompanyProjectQuery : IRequest<CompanyProjectDto>
    {
        public CompanyProjectQuery(int id) 
        { 
            Id = id;
        }
        public int Id { get; set; }
    }

    public class CompanyProjectQueryHandler : RequestHandler<CompanyProjectQuery, CompanyProjectDto>
    {
        public CompanyProjectQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async override Task<CompanyProjectDto> Handle(CompanyProjectQuery request, CancellationToken cancellationToken)
        {
            return await AppDbContext.CompanyProjects.Where(x => x.Id == request.Id).Select(Mapping.CompanyProjectProjection).FirstOrDefaultAsync();
        }
    }
}
