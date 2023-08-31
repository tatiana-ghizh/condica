using CVU.CONDICA.Dto.CompanyProjects;
using CVU.CONDICA.Persistence.Entities;
using MediatR;

namespace CVU.CONDICA.Application.CompanyProjects.Commands
{
    public class CreateCompanyProjectCommand : IRequest<int>
    {
        public CreateCompanyProjectCommand(CreateCompanyProjectDto postModel) 
        { 
            Name = postModel.Name;
            Description = postModel.Description;
            StartDay = postModel.StartDay;
            EndDay = postModel.EndDay;
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDay { get; set; }
        public DateTime? EndDay { get; set; }
    }

    public class CreateCompanyProjectCommandHandler : RequestHandler<CreateCompanyProjectCommand, int>
    {
        public CreateCompanyProjectCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async override Task<int> Handle(CreateCompanyProjectCommand request, CancellationToken cancellationToken)
        {
            var companyProject = new CompanyProject
            {
                Name = request.Name,
                Description = request.Description,
                StartDay = request.StartDay.Value,
                EndDay = request.EndDay.Value,
            };

            AppDbContext.CompanyProjects.Add(companyProject);
            AppDbContext.SaveChanges();

            return companyProject.Id;
        }
    }
}
