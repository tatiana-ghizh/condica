using CVU.CONDICA.Dto.Enums;
using MediatR;

namespace CVU.CONDICA.Application.Blobs.Commands
{
    public class DeleteBlobCommand : IRequest<Unit>
    {
        public DeleteBlobCommand(int id, int blobId, BlobComponent blobComponent)
        {
            Id = id;
            BlobId = blobId;
            BlobComponent = blobComponent;
        }

        public int Id { get; set; }
        public int BlobId { get; set; }
        public BlobComponent BlobComponent { get; set; }
    }

    public class DeleteBlobCommandHandler : RequestHandler<DeleteBlobCommand, Unit>
    {
        public DeleteBlobCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override async Task<Unit> Handle(DeleteBlobCommand request, CancellationToken cancellationToken)
        {
            var blobToRemove = AppDbContext.Blobs.Where(d => d.Id == request.BlobId).FirstOrDefault();

            AppDbContext.Blobs.Remove(blobToRemove);
            await AppDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
