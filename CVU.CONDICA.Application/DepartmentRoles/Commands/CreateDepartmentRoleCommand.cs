using CVU.CONDICA.Dto.DepartmentRoles;
using CVU.CONDICA.Persistence.Entities;
using MediatR;

namespace CVU.CONDICA.Application.DepartmentRoles.Commands
{
    public class CreateDepartmentRoleCommand : IRequest<int>
    {
        public CreateDepartmentRoleCommand() { }
        public CreateDepartmentRoleCommand(CreateDepartmentRoleDto model) 
        { 
            Name = model.Name;
            DepartmentRoleCode = model.DepartmentRoleCode;
            DepartmentId = model.DepartmentId;
        }
        public string Name { get; set; }
        public string DepartmentRoleCode { get; set; }
        public int DepartmentId { get; set; }
    }

    public class CreateDepartmentRoleCommandHandler : RequestHandler<CreateDepartmentRoleCommand, int>
    {
        public CreateDepartmentRoleCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async override Task<int> Handle(CreateDepartmentRoleCommand request, CancellationToken cancellationToken)
        {
            var newDepartmentRole = new DepartmentRole
            {
                Name = request.Name,
                DepartmentRoleCode = request.DepartmentRoleCode,
                DepartmentId = request.DepartmentId,
            };

            AppDbContext.DepartmentRoles.Add(newDepartmentRole);
            AppDbContext.SaveChanges();

            return newDepartmentRole.Id;
        }
    }
}
