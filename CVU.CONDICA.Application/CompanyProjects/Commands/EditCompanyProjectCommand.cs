using CVU.CONDICA.Dto.CompanyProjects;
using MediatR;

namespace CVU.CONDICA.Application.CompanyProjects.Commands
{
    public class EditCompanyProjectCommand : IRequest<Unit>
    {
        public EditCompanyProjectCommand(int id, EditCompanyProjectDto model) 
        { 
            Id = id;
            Name= model.Name;
            Description= model.Description;
            StartDate = model.StartDay;
            EndDate = model.EndDay;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class EditCompanyProjectCommandHandler : RequestHandler<EditCompanyProjectCommand, Unit>
    {
        public EditCompanyProjectCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async override Task<Unit> Handle(EditCompanyProjectCommand request, CancellationToken cancellationToken)
        {
            var projectToEdit = AppDbContext.CompanyProjects.Where(x => x.Id == request.Id).First();

            if (!string.IsNullOrEmpty(request.Name))
            {
                projectToEdit.Name = request.Name;
            }

            if (!string.IsNullOrEmpty(request.Description))
            {
                projectToEdit.Description = request.Description;
            }

            if (request.StartDate.HasValue)
            {
                projectToEdit.StartDay = request.StartDate.Value;
            }

            if (request.EndDate.HasValue)
            {
                projectToEdit.EndDay = request.EndDate.Value;
            }

            AppDbContext.SaveChanges();

            return Unit.Value;
        }
    }
}
