using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVU.CONDICA.Persistence.Entities
{
    public class DepartmentRole
    {
        public DepartmentRole()
        {
            UserDepartmentRoles = new HashSet<UserDepartmentRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string DepartmentRoleCode { get; set; }
        public int UserCount { get; set; }
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }

        public virtual ICollection<UserDepartmentRole> UserDepartmentRoles { get; set; }

    }

    public class DepartmentRoleConfiguration : IEntityTypeConfiguration<DepartmentRole>
    {
        public void Configure(EntityTypeBuilder<DepartmentRole> entity)
        {
            entity.HasOne(d => d.Department)
                .WithMany(d => d.DepartmentRoles)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
