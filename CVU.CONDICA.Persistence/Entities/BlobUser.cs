using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVU.CONDICA.Persistence.Entities
{
    public class BlobUser
    {
        public int BlobId { get; set; }
        public int UserId { get; set; }

        public virtual Blob Blob { get; set; }
        public virtual User User { get; set; }
    }
    public class BlobEventConfiguration : IEntityTypeConfiguration<BlobUser>
    {
        public void Configure(EntityTypeBuilder<BlobUser> builder)
        {
            builder.HasKey(d => new { d.BlobId, d.UserId });

            builder.HasOne(d => d.Blob)
                       .WithMany(d => d.BlobUsers)
                       .HasForeignKey(d => d.BlobId)
                       .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.User)
                    .WithMany(d => d.BlobUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
