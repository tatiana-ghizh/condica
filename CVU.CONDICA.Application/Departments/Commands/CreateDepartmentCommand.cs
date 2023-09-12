using CVU.CONDICA.Dto.Departments;
using CVU.CONDICA.Persistence.Entities;
using MediatR;

namespace CVU.CONDICA.Application.Departments.Commands
{
    public class CreateDepartmentCommand : IRequest<int>
    {
        public CreateDepartmentCommand() { }
        public CreateDepartmentCommand(CreateDepartmentDto model)
        {
            Name = model.Name;
        }

        public string Name { get; set; }

    }

    public class CreatePositionCommandHandler : RequestHandler<CreateDepartmentCommand, int>
    {
        public CreatePositionCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async override Task<int> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var newDepartment = new Department
            {
                Name = request.Name,
            };

            AppDbContext.Departments.Add(newDepartment);
            AppDbContext.SaveChanges();

            return newDepartment.Id;
        }
    }
}
