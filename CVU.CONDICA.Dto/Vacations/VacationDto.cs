using CVU.CONDICA.Dto.Enums;

namespace CVU.CONDICA.Dto.Vacations
{
    public class VacationDto
    {
        public int Id { get; set; }
        public VacationType Type { get; set; }
        public VacationStatus Status { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime RequestedAt { get; set; }
        public string Mentions { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
