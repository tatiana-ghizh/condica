namespace CVU.CONDICA.Dto.DepartmentRoles
{
    public class CreateDepartmentRoleDto
    {
        public CreateDepartmentRoleDto() { }
        public CreateDepartmentRoleDto(string name, string departmentRoleCode, int departmentId)
        {
            Name = name;
            DepartmentRoleCode = departmentRoleCode;
            DepartmentId = departmentId;
        }
        public string Name { get; set; }
        public string DepartmentRoleCode { get; set; }
        public int DepartmentId { get; set; }
    }
}
