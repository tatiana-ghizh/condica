using CVU.CONDICA.Dto.Enums;

namespace CVU.CONDICA.Dto.UserManagement
{
    public class CurrentUser
    {
        public CurrentUser()
        {
        }

        public CurrentUser(Dictionary<string,string> claims)
        {
            IsAuthenticated = true;
            FullName = claims[Claims.FullName];
            Role = (Role)int.Parse(claims[Claims.RoleId]);
            Id = int.Parse(claims[Claims.UserId]);
            Email = claims[Claims.EmailAddress];
            PositionId = int.Parse(claims[Claims.PositionId]);
            PositionName = claims[Claims.PositionName];
            //FirstName = claims[Claims.FirstName];
            //LastName = claims[Claims.LastName];
        }

        public bool IsAuthenticated { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public int PositionId { get; set; }
        public string PositionName { get; set; }
    }

}

