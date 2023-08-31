using MediatR;

namespace CVU.CONDICA.Application.CompanyProjects.Commands
{
    public class RemoveCompanyProjectCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }

    public class RemoveCompanyProjectCommandHandler : RequestHandler<RemoveCompanyProjectCommand, Unit>
    {
        public RemoveCompanyProjectCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async override Task<Unit> Handle(RemoveCompanyProjectCommand request, CancellationToken cancellationToken)
        {
            var projectToRemove = AppDbContext.CompanyProjects.First(x => x.Id == request.Id);
            AppDbContext.CompanyProjects.Remove(projectToRemove);  
            AppDbContext.SaveChanges();

            return Unit.Value;
        }
    }
}
