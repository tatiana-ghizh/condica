namespace CVU.CONDICA.Dto.Departments
{
    public class CreateDepartmentDto
    {
        public CreateDepartmentDto() { }
        public CreateDepartmentDto(string name) 
        { 
            Name = name;
        }
        public string Name { get; set; }
    }
}

