using CVU.CONDICA.Dto.Enums;
    
namespace CVU.CONDICA.Dto.UserManagement
{
    public class UserShortDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public int? PositionId { get; set; }
        public string PositionName { get; set; }
        public string SecurityCode { get; set; }

    }

}
