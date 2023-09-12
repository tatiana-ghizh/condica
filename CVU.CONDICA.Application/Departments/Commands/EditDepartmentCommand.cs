using CVU.CONDICA.Dto.Departments;
using MediatR;

namespace CVU.CONDICA.Application.Departments.Commands
{
    public class EditDepartmentCommand : IRequest<Unit>
    {
        public EditDepartmentCommand() { }
        public EditDepartmentCommand(int id, EditDepartmentDto model)
        {
            Id = id;
            Name = model.Name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class EditPositionCommandHandler : RequestHandler<EditDepartmentCommand, Unit>
    {
        public EditPositionCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async override Task<Unit> Handle(EditDepartmentCommand request, CancellationToken cancellationToken)
        {
            var departmentToEdit = AppDbContext.Departments.Where(p => p.Id == request.Id).First();

            if (!string.IsNullOrEmpty(departmentToEdit.Name))
            {
                departmentToEdit.Name = request.Name;
            }

            AppDbContext.SaveChanges();

            return Unit.Value;
        }
    }
}
