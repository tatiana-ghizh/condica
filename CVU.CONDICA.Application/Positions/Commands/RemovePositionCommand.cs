using MediatR;

namespace CVU.CONDICA.Application.Positions.Commands
{
    public class RemovePositionCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }

    public class RemovePositionCommandHandler : RequestHandler<RemovePositionCommand, Unit>
    {
        public RemovePositionCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async override Task<Unit> Handle(RemovePositionCommand request, CancellationToken cancellationToken)
        {
            var positionToRemove = AppDbContext.Positions.First(p => p.Id == request.Id); 

            AppDbContext.Positions.Remove(positionToRemove);
            AppDbContext.SaveChanges();

            return Unit.Value;
        }
    }
}
