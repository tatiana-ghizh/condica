using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CVU.CONDICA.Dto.Enums;

namespace CVU.CONDICA.Persistence.Entities
{
    public class User
    {
        public User() 
        { 
            UserCompanyProjects = new HashSet<UserCompanyProject>();
            Vacations = new HashSet<Vacation>();
        }
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Password { get; set; }

        public bool IsBlocked { get; set; }
        public bool IsActivated { get; set; }

        public string SecurityCode { get; set; }
        public DateTime? SecurityCodeExpiresAt { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }

        public Role Role { get; set; }
        public int? PositionId { get; set; }
        public virtual Position Position { get; set; }

        public virtual ICollection<UserCompanyProject> UserCompanyProjects { get; set; }
        public virtual ICollection<Vacation> Vacations { get; set; }

    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.HasOne(x => x.Position)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.PositionId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
