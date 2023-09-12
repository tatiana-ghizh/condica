using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVU.CONDICA.Dto.Departments
{
    public class EditDepartmentDto
    {
        public EditDepartmentDto() { }
        public EditDepartmentDto(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}
