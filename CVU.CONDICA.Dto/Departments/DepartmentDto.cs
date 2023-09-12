namespace CVU.CONDICA.Dto.Departments
{
    public class DepartmentDto
    {
        //public DepartmentDto(int id, string name)
        //{
        //    Id = id;
        //    Name = name;
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        //public int EmployeesNumber { get; set; }

        public override string ToString() => Name;
    }
}
