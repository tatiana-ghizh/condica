using CVU.CONDICA.Dto.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CVU.CONDICA.Persistence.Entities
{
    public class Blob
    {
        public Blob()
        {
            BlobUsers = new HashSet<BlobUser>();
        }
        public int Id { get; set; }
        public byte[] Content { get; set; }
        public string Name { get; set; }

        public virtual BlobType BlobType { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual ICollection<BlobUser> BlobUsers { get; set; }
    }

    public class BlobsConfiguration : IEntityTypeConfiguration<Blob>
    {
        public void Configure(EntityTypeBuilder<Blob> builder)
        {
        }
    }
}
