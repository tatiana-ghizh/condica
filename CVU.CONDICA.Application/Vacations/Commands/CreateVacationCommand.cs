using CVU.CONDICA.Dto.Enums;
using CVU.CONDICA.Dto.Vacations;
using CVU.CONDICA.Persistence.Entities;
using MediatR;

namespace CVU.CONDICA.Application.Vacations.Commands
{
    public class CreateVacationCommand : IRequest<int>
    {
        public CreateVacationCommand() { }
        public CreateVacationCommand(CreateVacationDto model)
        {
            Type = model.Type;
            FromDate = model.FromDate;
            ToDate = model.ToDate;
            Mentions = model.Mentions;
        }

        public VacationType Type { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string? Mentions { get; set; }
    }

    public class CreateVacationCommandHandler : RequestHandler<CreateVacationCommand, int>
    {
        public CreateVacationCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async override Task<int> Handle(CreateVacationCommand request, CancellationToken cancellationToken)
        {
            var newVacation = new Vacation
            {
                Type = request.Type,
                Status = VacationStatus.Pending,
                FromDate = request.FromDate,
                ToDate = request.ToDate,
                RequestedAt = DateTime.Now,
                Mentions = request.Mentions,
                UserId = CurrentUser.Id
            };

            AppDbContext.Vacations.Add(newVacation);
            AppDbContext.SaveChanges();

            return newVacation.Id;
        }
    }
}
