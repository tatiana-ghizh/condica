using MediatR;

namespace CVU.CONDICA.Application.Departments.Commands
{
    public class RemoveDepartmentCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }

    public class RemovePositionCommandHandler : RequestHandler<RemoveDepartmentCommand, Unit>
    {
        public RemovePositionCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async override Task<Unit> Handle(RemoveDepartmentCommand request, CancellationToken cancellationToken)
        {
            var departmentToRemove = AppDbContext.Departments.First(p => p.Id == request.Id);

            AppDbContext.Departments.Remove(departmentToRemove);
            AppDbContext.SaveChanges();

            return Unit.Value;
        }
    }
}
