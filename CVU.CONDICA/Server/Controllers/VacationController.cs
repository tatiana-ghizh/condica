using CVU.CONDICA.Application.VacationRequests.Commands;
using CVU.CONDICA.Application.Vacations.Commands;
using CVU.CONDICA.Application.Vacations.Queries;
using CVU.CONDICA.Dto.Pagination;
using CVU.CONDICA.Dto.Vacations;
using CVU.CONDICA.Server.Config;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CVU.CONDICA.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VacationController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<VacationDto>> GetVacations([FromQuery] VacationListQueryDto queryModel)
        {
            var query = new VacationListQuery(queryModel);

            return await Mediator.Send(query);
        }

        [HttpGet("my-vacation")]
        public async Task<PaginatedModel<VacationDto>> GetMyVacations([FromQuery] VacationListQueryDto queryModel)
        {
            var query = new MyVacationListQuery(queryModel);

            return await Mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<VacationDto> GetVacation([FromRoute] int id)
        {
            var query = new VacationQuery(id);

            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<int> CreateVacationForm([FromBody] CreateVacationDto postModel)
        {
            var command = new CreateVacationCommand(postModel);

            return await Mediator.Send(command);
        }

        [HttpPatch("{id}")]
        public async Task<Unit> UpdateLodgeTransferRequest([FromRoute] int id, [FromBody] EditVacationRequestStatus model)
        {
            return await Mediator.Send(new AcceptOrRejectVacationFormCommand(id, model));
        }

        [HttpPatch("update/{id}")]
        public async Task<Unit> EditVacation([FromRoute] int id, [FromBody] EditVacationDto model)
        {
            return await Mediator.Send(new EditVacationCommand(id, model));
        }
    }
}
