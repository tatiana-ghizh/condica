using CVU.CONDICA.Dto.Enums;
using CVU.CONDICA.Persistence.Entities;
using FluentValidation;
using MediatR;

namespace CVU.CONDICA.Application.Blobs.Commands
{
    public class CreateBlobCommand : IRequest<int>
    {
        public CreateBlobCommand(int id, byte[] content, string name, BlobType blobType, BlobComponent blobComponent)
        {
            Id = id;
            Content = content;
            Name = name;
            BlobType = blobType;
            BlobComponent = blobComponent;
        }

        public int Id { get; set; }
        public byte[] Content { get; set; }
        public string Name { get; set; }
        public BlobType BlobType { get; set; }
        public BlobComponent BlobComponent { get; set; }
    }

    public class CreateBlobCommandHandler : RequestHandler<CreateBlobCommand, int>
    {
        public CreateBlobCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override async Task<int> Handle(CreateBlobCommand request, CancellationToken cancellationToken)
        {
            var model = new Blob
            {
                Content = request.Content,
                Name = request.Name,
                BlobType = request.BlobType,
                CreatedAt = DateTime.Now
            };

            switch (request.BlobComponent)
            {
                case BlobComponent.User:
                    model.BlobUsers.Add(new BlobUser { UserId = request.Id });
                    break;
            }

            await AppDbContext.Blobs.AddAsync(model, cancellationToken);
            await AppDbContext.SaveChangesAsync(cancellationToken);

            await AppDbContext.SaveChangesAsync(cancellationToken);

            return model.Id;
        }
    }

    public class CreateBlobValidator : AbstractValidator<CreateBlobCommand>
    {
        public CreateBlobValidator(IServiceProvider services)
        {
            RuleFor(d => d).Custom((obj, context) =>
            {
                if (obj.Id == 0)
                {
                    context.AddFailure("Id can not be empty.");
                    return;
                }

                //using var scope = services.CreateScope();
                //var db = scope.ServiceProvider.GetService<AppDbContext>();
                //var article = db.Articles.FirstOrDefault(d => d.Id == obj.Id);

                //if (article == null)
                //{
                //    context.AddFailure("Invalid Article Id.");
                //}
            });
        }
    }
}
