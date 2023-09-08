using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVU.CONDICA.Persistence.Entities
{
    public class UserDepartmentRole
    {
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int DepartmentRoleId { get; set; }
        public DepartmentRole DepartmentRole { get; set; }
    }

    public class UserDepartmentRoleConfiguration : IEntityTypeConfiguration<UserDepartmentRole>
    {
        public void Configure(EntityTypeBuilder<UserDepartmentRole> entity)
        {
            entity.HasKey(d => new { d.UserId, d.DepartmentRoleId });

            entity.HasOne(d => d.User)
                .WithOne(d => d.UserDepartmentRole)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(d => d.DepartmentRole)
                .WithMany(d => d.UserDepartmentRoles)
                .HasForeignKey(d => d.DepartmentRoleId)
                .OnDelete(DeleteBehavior.NoAction);
        }

    }
}
