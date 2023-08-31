using CVU.CONDICA.Dto.Enums;
using CVU.CONDICA.Dto.Vacations;
using MediatR;

namespace CVU.CONDICA.Application.VacationRequests.Commands
{
    public class AcceptOrRejectVacationFormCommand : IRequest<Unit>
    {
        public AcceptOrRejectVacationFormCommand(int id, EditVacationRequestStatus model)
        {
            Id = id;
            Status = model.Status;
        }
        public int Id { get; set; }
        public VacationStatus? Status { get; set; }
    }

    public class AcceptOrRejectVacationFormCommandHandler : RequestHandler<AcceptOrRejectVacationFormCommand, Unit>
    {
        public AcceptOrRejectVacationFormCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async override Task<Unit> Handle(AcceptOrRejectVacationFormCommand request, CancellationToken cancellationToken)
        {
            var vacation = AppDbContext.Vacations.Where(x => x.Id == request.Id).FirstOrDefault();

            if (request.Status.HasValue)
            {
                vacation.Status = request.Status.Value;
            }

            AppDbContext.SaveChanges();

            return Unit.Value;
        }
    }
}
