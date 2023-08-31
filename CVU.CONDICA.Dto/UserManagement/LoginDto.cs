namespace CVU.CONDICA.Dto.UserManagement
{
    public class LoginDto
    {
        public LoginDto()
        {
            BearerAuth = true;
        }

        public LoginDto(string email, string password, bool bearerAuth = true)
        {
            Email = email;
            Password = password;
            BearerAuth = bearerAuth;
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public bool BearerAuth { get; set; }
    }

}
