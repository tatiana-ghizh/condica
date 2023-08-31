using CVU.CONDICA.Application.CompanyProjects.Commands;
using CVU.CONDICA.Application.CompanyProjects.Queries;
using CVU.CONDICA.Dto.CompanyProjects;
using CVU.CONDICA.Dto.Pagination;
using CVU.CONDICA.Server.Config;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CVU.CONDICA.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyProjectController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<CompanyProjectDto>> GetCompanyProjects([FromQuery] CompanyProjectListQueryDto queryModel)
        {
            var query = new CompanyProjectListQuery(queryModel);

            return await Mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<CompanyProjectDto> GetCompanyProject([FromRoute] int id)
        {
            var query = new CompanyProjectQuery(id);

            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<int> CreateCompanyProject([FromBody] CreateCompanyProjectDto postModel)
        {
            var command = new CreateCompanyProjectCommand(postModel);

            return await Mediator.Send(command);
        }

        [HttpPatch("{id}")]
        public async Task EditCompanyProject([FromRoute] int id, [FromBody] EditCompanyProjectDto postModel)
        {
            var command = new EditCompanyProjectCommand(id, postModel);

            await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<Unit> RemoveCompanyProject([FromRoute] int id)
        {
            return await Mediator.Send(new RemoveCompanyProjectCommand { Id = id });
        }
    }
}
