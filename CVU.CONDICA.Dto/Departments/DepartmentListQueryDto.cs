using CVU.CONDICA.Dto.Pagination;

namespace CVU.CONDICA.Dto.Departments
{
    public class DepartmentListQueryDto : PaginatedQueryParameter
    {
        public string Name { get; set; }
    }
}
