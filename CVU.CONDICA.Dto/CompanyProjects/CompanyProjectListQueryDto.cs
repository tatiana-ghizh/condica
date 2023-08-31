using CVU.CONDICA.Dto.Pagination;

namespace CVU.CONDICA.Dto.CompanyProjects
{
    public class CompanyProjectListQueryDto : PaginatedQueryParameter
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
