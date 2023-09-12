using CVU.CONDICA.Application.DepartmentRoles.Commands;
using CVU.CONDICA.Application.DepartmentRoles.Queries;
using CVU.CONDICA.Dto.DepartmentRoles;
using CVU.CONDICA.Dto.Pagination;
using CVU.CONDICA.Server.Config;
using Microsoft.AspNetCore.Mvc;

namespace CVU.CONDICA.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentRoleController : BaseController
    {
        [HttpPost]
        public async Task<int> CreateDepartmentRoles([FromBody] CreateDepartmentRoleDto postModel)
        {
            var command = new CreateDepartmentRoleCommand(postModel);

            return await Mediator.Send(command);
        }

        [HttpGet]
        public async Task<PaginatedModel<DepartmentRoleDto>> GetDepartmenRoless([FromQuery] DepartmentRoleListQueryDto queryModel)
        {
            var query = new DepartmentRoleListQuery(queryModel);

            return await Mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<DepartmentRoleDto> GetDepartmentRole([FromRoute] int id)
        {
            var query = new DepartmentRoleQuery(id);

            return await Mediator.Send(query);
        }
    }
}
