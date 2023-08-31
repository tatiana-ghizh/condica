using CVU.CONDICA.Dto.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CVU.CONDICA.Persistence.Entities
{
    public class Vacation
    {
        public int Id { get; set; }
        public VacationType Type { get; set; }
        public VacationStatus Status { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime RequestedAt { get; set; }
        public string Mentions { get; set; }
        public int NumberOfDays { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }

    public class VacationConfiguration : IEntityTypeConfiguration<Vacation>
    {
        public void Configure(EntityTypeBuilder<Vacation> entity)
        {
            entity.HasOne(d => d.User)
                .WithMany(d => d.Vacations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
