namespace CVU.CONDICA.Dto.Positions
{
    public class CreatePositionDto
    {
        public CreatePositionDto() { }
        public CreatePositionDto(string name) 
        { 
            Name = name;
        }
        public string Name { get; set; }
    }
}
