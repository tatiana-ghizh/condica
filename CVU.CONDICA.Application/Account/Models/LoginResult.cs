using CVU.CONDICA.Dto.UserManagement;

namespace CVU.CONDICA.Application.Account.Models
{
    public class LoginResult
    {
        public UserDto User { get; set; }
        public TokenDto Jwt { get; set; }
    }
}
