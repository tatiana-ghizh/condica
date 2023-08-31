namespace CVU.CONDICA.Dto.Generic
{
    public class DropdownDto
    {
        public DropdownDto(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString() => Name;
    }
}
