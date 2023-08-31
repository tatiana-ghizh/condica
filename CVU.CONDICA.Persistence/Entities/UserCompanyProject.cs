using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CVU.CONDICA.Persistence.Entities
{
    public class UserCompanyProject
    {
        public int CompanyProjectId { get; set; }
        public virtual CompanyProject CompanyProject { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }

    public class UserCompanyProjectConfiguration : IEntityTypeConfiguration<UserCompanyProject>
    {
        public void Configure(EntityTypeBuilder<UserCompanyProject> entity)
        {
            entity.HasKey(d => new { d.UserId, d.CompanyProjectId });

            entity.HasOne(d => d.User)
                .WithMany(d => d.UserCompanyProjects)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(d => d.CompanyProject)
                .WithMany(d => d.UserCompanyProjects)
                .HasForeignKey(d => d.CompanyProjectId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
