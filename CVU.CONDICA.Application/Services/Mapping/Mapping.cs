using CVU.CONDICA.Dto.CompanyProjects;
using CVU.CONDICA.Dto.Positions;
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

        //public static Expression<Func<Position, PositionDto>> PositionProjection
        //{
        //    get
        //    {
        //        return d => new PositionDto
        //        {
        //            Id = d.Id,
        //            Name = d.Name,
        //            EmployeesNumber = d.Users.Count()
        //        };
        //    }
        //}

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
    }
}
