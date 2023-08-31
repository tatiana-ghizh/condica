using CVU.CONDICA.Dto.Enums;
using CVU.CONDICA.Dto.Pagination;

namespace CVU.CONDICA.Dto.Vacations
{
    public class VacationListQueryDto : PaginatedQueryParameter
    {
        public VacationListQueryDto()
        {
            Statuses = new List<VacationStatus>();
        }

        public List<VacationType> Types { get; set; }
        public List<VacationStatus> Statuses { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? RequestedAt { get; set; }
        public string UserName { get; set; }
    }
}
