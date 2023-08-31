using CVU.CONDICA.Dto.Enums;

namespace CVU.CONDICA.Dto.Vacations
{
    public class EditVacationDto
    {
        public EditVacationDto() { }
        public EditVacationDto(VacationType type, DateTime? fromDate, DateTime? toDate, string mentions)
        {
            Type = type;
            FromDate = fromDate;
            ToDate = toDate;
            Mentions = mentions;
        }

        public VacationType Type { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Mentions { get; set; }
    }
}
