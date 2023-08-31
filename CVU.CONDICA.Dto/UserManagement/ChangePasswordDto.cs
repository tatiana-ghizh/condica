namespace CVU.CONDICA.Dto.UserManagement
{
    public class ChangePasswordDto
    {
        public ChangePasswordDto()
        {

        }

        public ChangePasswordDto(string currentPassword, string newPassword, string confirmPassword)
        {
            CurrentPassword = currentPassword;
            NewPassword = newPassword;
            ConfirmPassword = confirmPassword;
        }

        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
