namespace CVU.CONDICA.Dto.DepartmentRoles
{
    public class DepartmentRoleDto
    {
        public DepartmentRoleDto() 
        {
        
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string DepartmentRoleCode { get; set; }
        public int UserCount { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public override string ToString() => Name;
    }
}
