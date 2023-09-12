using CVU.CONDICA.Dto.Pagination;

namespace CVU.CONDICA.Dto.DepartmentRoles
{
    public class DepartmentRoleListQueryDto : PaginatedQueryParameter
    {
        public string Name { get; set; }
        public string DepartmentRoleCode { get; set; }
    }
}
