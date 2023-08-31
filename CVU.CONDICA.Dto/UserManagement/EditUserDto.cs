using CVU.CONDICA.Dto.Enums;

namespace CVU.CONDICA.Dto.UserManagement
{
    public class EditUserDto
    {
        public EditUserDto()
        {

        }

        public EditUserDto(string firstName, string lastName, int positionId, Role roleId, bool isBlocked)
        {
            FirstName = firstName;
            LastName = lastName;
            PositionId = positionId;
            Role = roleId;
            IsBlocked = isBlocked;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? PositionId { get; set; }
        public Role Role { get; set; }
        public bool? IsBlocked { get; set; }

    }

}

