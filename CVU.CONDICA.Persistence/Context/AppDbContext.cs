using CVU.CONDICA.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace CVU.CONDICA.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<CompanyProject> CompanyProjects { get; set; }
        public virtual DbSet<UserCompanyProject> UserCompanyProjects { get; set; }
        public virtual DbSet<Vacation> Vacations { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<DepartmentRole> DepartmentRoles { get; set; }
        public virtual DbSet<UserDepartmentRole> UserDepartmentRoles { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
