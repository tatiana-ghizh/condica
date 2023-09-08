//using CVU.CONDICA.Application.Services.Mapping;
//using CVU.CONDICA.Dto.Positions;
//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CVU.CONDICA.Application.Positions.Queries
//{
//    public class PositionQuery : IRequest<PositionDto>
//    {
//        public PositionQuery(int id) 
//        { 
//            Id = id;
//        }
//        public int Id { get; set; }
//    }

//    public class PositionQueryHandler : RequestHandler<PositionQuery, PositionDto>
//    {
//        public PositionQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider)
//        {
//        }

//        public async override Task<PositionDto> Handle(PositionQuery request, CancellationToken cancellationToken)
//        {
//            return await AppDbContext.Positions.Where(p => p.Id == request.Id).Select(Mapping.PositionProjection).FirstOrDefaultAsync();
//        }
//    }
//}
