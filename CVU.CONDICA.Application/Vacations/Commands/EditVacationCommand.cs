using CVU.CONDICA.Dto.Enums;
using CVU.CONDICA.Dto.Vacations;
using MediatR;

namespace CVU.CONDICA.Application.Vacations.Commands
{
    public class EditVacationCommand : IRequest<Unit>
    {
        public EditVacationCommand() { }
        public EditVacationCommand(int id, EditVacationDto model) 
        {
            Id= id;
            Type = model.Type;
            FromDate = model.FromDate;
            ToDate = model.ToDate;
            Mentions= model.Mentions;
        }

        public int Id { get; set; }
        public VacationType? Type { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Mentions { get; set; }
    }

    public class EditVacationCommandHandler : RequestHandler<EditVacationCommand, Unit>
    {
        public EditVacationCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async override Task<Unit> Handle(EditVacationCommand request, CancellationToken cancellationToken)
        {
            var vacationsToEdit = AppDbContext.Vacations.Where(x => x.Id == request.Id).First();

            if (request.Type.HasValue) 
            {
                vacationsToEdit.Type = request.Type.Value;
            }

            if (!string.IsNullOrEmpty(request.Mentions))
            {
                vacationsToEdit.Mentions = request.Mentions;
            }

            if (request.FromDate.HasValue)
            {
                vacationsToEdit.FromDate = request.FromDate.Value;
            }

            if (request.ToDate.HasValue)
            {
                vacationsToEdit.ToDate = request.ToDate.Value;
            }

            AppDbContext.SaveChanges();

            return Unit.Value;
        }
    }
}
