using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CVU.CONDICA.Persistence.Entities
{
    public class CompanyProject
    {
        public CompanyProject() 
        {
            UserCompanyProjects = new HashSet<UserCompanyProject>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime EndDay { get; set; }

        public virtual ICollection<UserCompanyProject> UserCompanyProjects { get; set; }

    }
    public class CompanyProjectConfiguration : IEntityTypeConfiguration<CompanyProject>
    {
        public void Configure(EntityTypeBuilder<CompanyProject> entity)
        {

        }
    }
}
