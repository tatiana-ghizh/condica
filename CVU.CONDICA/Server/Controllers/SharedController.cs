using CVU.CONDICA.Dto.Generic;
using CVU.CONDICA.Persistence.Context;
using CVU.CONDICA.Server.Config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CVU.CONDICA.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public class SharedController : BaseController
    {
        private readonly AppDbContext appDbContext;

        public SharedController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        [HttpGet("users")]
        public async Task<IEnumerable<DropdownDto>> GetUsers(string query)
        {
            var users = appDbContext.User.AsQueryable();

            if (!string.IsNullOrEmpty(query))
            {
                users = users.Where(d => d.Email.Contains(query) || d.FirstName.Contains(query) || d.LastName.Contains(query) || (d.LastName + " " + d.FirstName).Contains(query))
                    .OrderBy(d => d.FirstName.StartsWith(query))
                    .ThenBy(d => d.LastName.StartsWith(query))
                    .ThenBy(d => (d.LastName + " " + d.FirstName).StartsWith(query))
                    .ThenBy(d => d.Email.StartsWith(query));
            }

            var list = await users
                .Select(d => new DropdownDto(d.Id, $"{d.FirstName} {d.LastName} - {d.Email}"))
                .ToListAsync();

            return list;
        }

        [HttpGet("users/{id}")]
        public async Task<DropdownDto> GetUser([FromRoute] int id)
        {
            var user = await appDbContext.User
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();

            return new DropdownDto(user.Id, $"{user.FirstName} {user.LastName} - {user.Email}");
        }

        //[HttpGet("positions")]
        //public async Task<IEnumerable<DropdownDto>> GetPositions(string query)
        //{
        //    var positionsQuery = appDbContext.Positions.AsQueryable();

        //    if (!string.IsNullOrEmpty(query))
        //    {
        //        positionsQuery = positionsQuery.Where(d => d.Name.Contains(query))
        //            .OrderBy(d => d.Name.StartsWith(query))
        //            .ThenBy(d => d.Name.Contains(query));
        //    }

        //    var positions = await positionsQuery
        //    .Select(d => new DropdownDto(d.Id, d.Name))
        //    .ToListAsync();

        //    return positions;
        //}
    }
}
