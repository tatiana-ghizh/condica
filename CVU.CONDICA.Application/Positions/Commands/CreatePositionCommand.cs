//using CVU.CONDICA.Dto.Positions;
//using CVU.CONDICA.Persistence.Entities;
//using MediatR;

//namespace CVU.CONDICA.Application.Positions.Commands
//{
//    public class CreatePositionCommand : IRequest<int>
//    {
//        public CreatePositionCommand() { }
//        public CreatePositionCommand(CreatePositionDto model) 
//        { 
//            Name = model.Name;
//        }

//        public string Name { get; set; }

//    }

//    public class CreatePositionCommandHandler : RequestHandler<CreatePositionCommand, int>
//    {
//        public CreatePositionCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
//        {
//        }

//        public async override Task<int> Handle(CreatePositionCommand request, CancellationToken cancellationToken)
//        {
//            var newPosition = new Position
//            {
//                Name = request.Name,
//            };

//            AppDbContext.Positions.Add(newPosition);
//            AppDbContext.SaveChanges();

//            return newPosition.Id;
//        }
//    }
//}
