using CVU.CONDICA.Dto.Pagination;

namespace CVU.CONDICA.Dto.RequestModels
{
    public class UserListQueryModel : PaginatedQueryParameter
    {
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
