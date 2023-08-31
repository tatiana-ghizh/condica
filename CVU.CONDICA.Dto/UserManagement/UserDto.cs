using CVU.CONDICA.Dto.Enums;
using System.Text.Json.Serialization;

namespace CVU.CONDICA.Dto.UserManagement
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public int RoleId { get; set; }
        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public bool IsBlocked { get; set; }
        [JsonIgnore]
        public bool IsAuthenticated { get; set; }
        public string SecurityCode { get; set; }
    }
}


