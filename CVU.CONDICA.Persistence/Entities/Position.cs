using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CVU.CONDICA.Persistence.Entities
{
    public class Position
    {
        public Position() 
        {
            Users = new HashSet<User>();

        }
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }

    }

    public class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> entity)
        {

        }
    }
}
