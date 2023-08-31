namespace CVU.CONDICA.Dto.UserManagement
{
    public class ResetPasswordDto
    {
        public string SecurityCode { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

}
