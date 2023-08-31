namespace CVU.CONDICA.Dto.UserManagement
{
    public class CreateUserDto
    {
        public CreateUserDto() { }
        public CreateUserDto(string email, string firstName, string lastName, int positionId, int roleId, bool isBlocked)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            PositionId = positionId;
            RoleId = roleId;
            IsBlocked = isBlocked;
        }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PositionId { get; set; }
        public int RoleId { get; set; }
        public bool IsBlocked { get; set; }
    }
}
