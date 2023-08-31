using CVU.CONDICA.Application.VacationRequests.Commands;
using CVU.CONDICA.Server.Config;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CVU.CONDICA.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VacationRequestController : BaseController
    {
        [HttpPatch("{id}")]
        public async Task<Unit> UpdateLodgeTransferRequest([FromBody] AcceptOrRejectVacationFormCommand model)
        {
            return await Mediator.Send(model);
        }
    }
}
