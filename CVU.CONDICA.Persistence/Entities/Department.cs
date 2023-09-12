using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVU.CONDICA.Persistence.Entities
{
    public class Department
    {
        public Department() 
        {
            DepartmentRoles = new HashSet<DepartmentRole>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? UserCount { get; set; }

        public virtual ICollection<DepartmentRole> DepartmentRoles { get; set; }

    }
}
