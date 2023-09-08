//using CVU.CONDICA.Application.Positions.Commands;
//using CVU.CONDICA.Application.Positions.Queries;
//using CVU.CONDICA.Dto.Generic;
//using CVU.CONDICA.Dto.Pagination;
//using CVU.CONDICA.Dto.Positions;
//using CVU.CONDICA.Persistence.Context;
//using CVU.CONDICA.Server.Config;
//using MediatR;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace CVU.CONDICA.Server.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class PositionController : BaseController
//    {
//        private readonly AppDbContext appDbContext;

//        public PositionController(AppDbContext appDbContext)
//        {
//            this.appDbContext = appDbContext;
//        }

//        [HttpGet]
//        public async Task<PaginatedModel<PositionDto>> GetPositions([FromQuery] PositionListQueryDto queryModel)
//        {
//            var query = new PositionListQuery(queryModel);

//            return await Mediator.Send(query);
//        }

//        [HttpGet("{id}")]
//        public async Task<PositionDto> GetPosition([FromRoute] int id)
//        {
//            var query = new PositionQuery(id);

//            return await Mediator.Send(query);
//        }

//        [HttpPost]
//        public async Task<int> CreatePosition([FromBody] CreatePositionDto postModel)
//        {
//            var command = new CreatePositionCommand(postModel);

//            return await Mediator.Send(command);
//        }

//        [HttpPatch("{id}")]
//        public async Task EditPosition([FromRoute] int id, [FromBody] EditPositionDto postModel)
//        {
//            var command = new EditPositionCommand(id, postModel);

//            await Mediator.Send(command);
//        }

//        [HttpDelete("{id}")]
//        public async Task<Unit> RemovePosition([FromRoute] int id)
//        {
//            return await Mediator.Send(new RemovePositionCommand { Id = id });
//        }

//        [HttpGet("positions-dropdown")]
//        public async Task<IEnumerable<PositionDto>> GetPositions(string query)
//        {
//            var positionsQuery = appDbContext.Positions.AsQueryable();

//            if (!string.IsNullOrEmpty(query))
//            {
//                positionsQuery = positionsQuery.Where(d => d.Name.Contains(query))
//                    .OrderBy(d => d.Name.StartsWith(query))
//                    .ThenBy(d => d.Name.Contains(query));
//            }

//            var positions = await positionsQuery
//            .Select(d => new PositionDto 
//            { 
//                Id = d.Id,
//                Name = d.Name
//            })
//            .ToListAsync();

//            return positions;
//        }
//    }
//}
