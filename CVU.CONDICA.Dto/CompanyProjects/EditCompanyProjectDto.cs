namespace CVU.CONDICA.Dto.CompanyProjects
{
    public class EditCompanyProjectDto
    {
        public EditCompanyProjectDto() { }
        public EditCompanyProjectDto(string name, string description, DateTime? startDay, DateTime? endDay)
        {
            Name = name;
            Description = description;
            StartDay = startDay;
            EndDay = endDay;
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDay { get; set; }
        public DateTime? EndDay { get; set; }
    }
}
