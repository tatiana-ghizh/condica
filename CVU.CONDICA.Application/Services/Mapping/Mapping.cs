using CVU.CONDICA.Dto.Blob;
using CVU.CONDICA.Dto.CompanyProjects;
using CVU.CONDICA.Dto.DepartmentRoles;
using CVU.CONDICA.Dto.Departments;
using CVU.CONDICA.Dto.UserManagement;
using CVU.CONDICA.Dto.Vacations;
using CVU.CONDICA.Persistence.Entities;
using System.Linq.Expressions;

namespace CVU.CONDICA.Application.Services.Mapping
{
    public static class Mapping
    {
        public static Expression<Func<User, UserDto>> UserProjection
        {
            get
            {
                return d => new UserDto
                {
                    Id = d.Id,
                    Email = d.Email,
                    FullName = d.FirstName + " " + d.LastName,
                    Role = d.Role,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    //PositionId = (int)d.UserPositionId,
                    //PositionName = d.Position.Name,
                    IsBlocked = d.IsBlocked
                };

            }
        }

        public static Expression<Func<User, UserShortDto>> UserShortProjection
        {
            get
            {
                return d => new UserShortDto
                {
                    Id = d.Id,
                    Email = d.Email,
                    FullName = d.FirstName + " " + d.LastName,
                    Role = d.Role,
                    //PositionId = d.UserPositionId,
                    //PositionName = d.Position.Name,
                    SecurityCode = d.SecurityCode,
                };
            }
        }

        public static Expression<Func<Department, DepartmentDto>> DepartmentProjection
        {
            get
            {
                return d => new DepartmentDto
                {
                    Id = d.Id,
                    Name = d.Name,
                };
            }
        }

        public static Expression<Func<DepartmentRole, DepartmentRoleDto>> DepartmentRoleProjection
        {
            get
            {
                return d => new DepartmentRoleDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    DepartmentRoleCode= d.DepartmentRoleCode,
                    UserCount = d.UserDepartmentRoles.Count(),
                    DepartmentId = d.DepartmentId.Value,
                    DepartmentName = d.Department.Name,
                };
            }
        }

        public static Expression<Func<CompanyProject, CompanyProjectDto>> CompanyProjectProjection
        {
            get
            {
                return d => new CompanyProjectDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    StartDay = d.StartDay,
                    EndDay = d.EndDay,
                };
            }
        }

        public static Expression<Func<Vacation, VacationDto>> VacationProjection
        {
            get
            {
                return d => new VacationDto
                {
                    Id = d.Id,
                    Type = d.Type,
                    Status = d.Status,
                    FromDate= d.FromDate,
                    ToDate= d.ToDate,
                    RequestedAt= d.RequestedAt,
                    Mentions = d.Mentions,
                    UserId = d.UserId,
                    UserName = d.User.FirstName + " " + d.User.LastName,
                };
            }
        }

        public static Expression<Func<Blob, BlobDto>> BlobProjection
        {
            get
            {
                return d => new BlobDto
                {
                    Id = d.Id,
                    BlobType = d.BlobType,
                    Content = d.Content,
                    Name = d.Name
                };
            }
        }
    }
}
