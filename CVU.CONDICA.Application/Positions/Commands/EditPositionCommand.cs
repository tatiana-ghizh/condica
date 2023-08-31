using CVU.CONDICA.Dto.Positions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVU.CONDICA.Application.Positions.Commands
{
    public class EditPositionCommand : IRequest<Unit>
    {
        public EditPositionCommand() { }
        public EditPositionCommand(int id, EditPositionDto model)
        {
            Id = id;
            Name = model.Name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class EditPositionCommandHandler : RequestHandler<EditPositionCommand, Unit>
    {
        public EditPositionCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async override Task<Unit> Handle(EditPositionCommand request, CancellationToken cancellationToken)
        {
            var positionToEdit = AppDbContext.Positions.Where(p => p.Id == request.Id).First();

            if(!string.IsNullOrEmpty(positionToEdit.Name))
            {
                positionToEdit.Name = request.Name;
            }

            AppDbContext.SaveChanges();

            return Unit.Value;
        }
    }
}
