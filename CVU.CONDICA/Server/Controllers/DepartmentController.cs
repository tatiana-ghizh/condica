using CVU.CONDICA.Application.Departments.Commands;
using CVU.CONDICA.Application.Departments.Queries;
using CVU.CONDICA.Dto.Pagination;
using CVU.CONDICA.Dto.Departments;
using CVU.CONDICA.Persistence.Context;
using CVU.CONDICA.Server.Config;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CVU.CONDICA.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : BaseController
    {
        private readonly AppDbContext appDbContext;

        public DepartmentController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<PaginatedModel<DepartmentDto>> GetDepartments([FromQuery] DepartmentListQueryDto queryModel)
        {
            var query = new DepartmentListQuery(queryModel);

            return await Mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<DepartmentDto> GetDepartment([FromRoute] int id)
        {
            var query = new DepartmentQuery(id);

            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<int> CreateDepartment([FromBody] CreateDepartmentDto postModel)
        {
            var command = new CreateDepartmentCommand(postModel);

            return await Mediator.Send(command);
        }

        [HttpPatch("{id}")]
        public async Task EditDepartment([FromRoute] int id, [FromBody] EditDepartmentDto postModel)
        {
            var command = new EditDepartmentCommand(id, postModel);

            await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<Unit> RemoveDepartment([FromRoute] int id)
        {
            return await Mediator.Send(new RemoveDepartmentCommand { Id = id });
        }

        [HttpGet("positions-dropdown")]
        public async Task<IEnumerable<DepartmentDto>> GetPositions(string query)
        {
            var departmentsQuery = appDbContext.Departments.AsQueryable();

            if (!string.IsNullOrEmpty(query))
            {
                departmentsQuery = departmentsQuery.Where(d => d.Name.Contains(query))
                    .OrderBy(d => d.Name.StartsWith(query))
                    .ThenBy(d => d.Name.Contains(query));
            }

            var deparments = await departmentsQuery
            .Select(d => new DepartmentDto
            {
                Id = d.Id,
                Name = d.Name
            })
            .ToListAsync();

            return deparments;
        }
    }
}
