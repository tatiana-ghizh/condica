using CVU.CONDICA.Dto.Enums;
using CVU.CONDICA.Dto.UserManagement;
using Microsoft.AspNetCore.Components.Authorization;

namespace CVU.CONDICA.Client.Services
{
    public class CurrentUserService
    {
        private readonly AuthenticationStateProvider authStateProvider;

        public CurrentUserService(AuthenticationStateProvider authStateProvider)
        {
            this.authStateProvider = authStateProvider;
        }


        public async Task<CurrentUser> GetAsync()
        {
            var authState = await authStateProvider.GetAuthenticationStateAsync();

            var claims = authState.User?.Claims.ToDictionary(d => d.Type, t => t.Value);

            if (claims != null && claims.Any())
            {
                return new CurrentUser
                {
                    FullName = claims[Claims.FullName],
                    Role = (Role)int.Parse(claims[Claims.RoleId]),
                    Id = int.Parse(claims[Claims.UserId]),
                    Email = claims[Claims.EmailAddress],
                    //FirstName = claims[Claims.FirstName],
                    //LastName = claims[Claims.LastName],
                    //PositionId = int.Parse(claims[Claims.PositionId]),
                    //PositionName = claims[Claims.PositionName],
                };
            }
            else
            {
                return new CurrentUser();
            }
        }

        public CurrentUser CurrentUser { get; set; }
    }
}
