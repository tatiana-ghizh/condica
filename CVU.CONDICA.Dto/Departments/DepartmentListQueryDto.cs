using CVU.CONDICA.Dto.Pagination;

namespace CVU.CONDICA.Dto.Positions
{
    public class PositionListQueryDto : PaginatedQueryParameter
    {
        public string Name { get; set; }
    }
}
