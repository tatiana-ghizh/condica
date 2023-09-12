using CVU.CONDICA.Dto.Files;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CVU.CONDICA.Application.Blobs.Queries
{
    public class DownloadBlobQuery : IRequest<FileDataDto>
    {
        public DownloadBlobQuery(int id, int blobId)
        {
            Id = id;
            BlobId = blobId;
        }

        public int Id { get; set; }
        public int BlobId { get; set; }

    }

    public class DownloadBlobQueryHandler : RequestHandler<DownloadBlobQuery, FileDataDto>
    {
        public DownloadBlobQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async override Task<FileDataDto> Handle(DownloadBlobQuery request, CancellationToken cancellationToken)
        {
            var file = AppDbContext.Blobs.Where(d => d.Id == request.BlobId).Include(x => x.BlobType).FirstOrDefault();

            return new FileDataDto
            {
                Content = file.Content,
                ContentType = file.BlobType.ToString(),
                Name = file.Name
            };
        }
    }
}
